"use strict";
app.directive('ngGallery', ngGallery);

ngGallery.$inject = ['$document', '$timeout', '$q', '$templateCache', 'filesUploadService', '$rootScope', '$filter'];

function ngGallery($document, $timeout, $q, $templateCache, filesUploadService, $rootScope, $filter) {

    var defaults = {
        baseClass: 'ng-gallery',
        thumbClass: 'ng-thumb',
        templateUrl: 'ng-gallery.html'
    };

    var keys_codes = {
        enter: 13,
        esc: 27,
        left: 37,
        right: 39
    };

    function setScopeValues(scope, attrs) {
        scope.baseClass = scope.class || defaults.baseClass;
        scope.thumbClass = scope.thumbClass || defaults.thumbClass;
        scope.thumbsNum = scope.thumbsNum || 3; 
    }

    var template_url = defaults.templateUrl;
    // Set the default template
    $templateCache.put(template_url,
        '<div class="{{ baseClass }}">' +
        '  <div ng-repeat="i in images" class="thumbdiv">' +
        '       <img ng-src="{{ i.thumb }}" class="{{ thumbClass }}" ng-click="openGallery($index)" alt="Image {{ $index + 1 }}" />' +        
        '       <div ng-show="isadmin" class="btn-group btn-toggle">' +
        '            <button ng-class="i.isMain ? \'btn-primary\':\'btn-default\'" class="btn btn-sm" ng-click="setMainPhoto(i, true)">ON</button>' +
        '            <button ng-class="i.isMain ? \'btn-default\':\'btn-primary\'" class="btn btn-sm" ng-click="setMainPhoto(i, false)">OFF</button>' +
        '        </div>' +
        '       <input ng-show="isadmin" type="button" style="margin-top:2px;margin-left:10px;" class="btn btn-default" value="Delete" ng-click="removeItem(i.id, $index)" />' +
        '  </div>' +
        '  <div ng-show="!images.length">' +
        '       <img src="http://dummyimage.com/200x150&text=image" class="{{ thumbClass }}" />' +
        '  </div>' +
        '</div>' +
        '<div class="ng-overlay" ng-show="opened">' +
        '</div>' +
        '<div class="ng-gallery-content" unselectable="on" ng-show="opened" ng-swipe-left="nextImage()" ng-swipe-right="prevImage()">' +
        '  <div class="uil-ring-css" ng-show="loading"><div></div></div>' +
        '<a href="{{getImageDownloadSrc()}}" target="_blank" ng-show="showImageDownloadButton()" class="download-image"><i class="fa fa-download"></i></a>' +
        '  <a class="close-popup" ng-click="closeGallery()"><i class="fa fa-close"></i></a>' +
        '  <a class="nav-left" ng-click="prevImage()"><i class="fa fa-angle-left"></i></a>' +
        '  <img ondragstart="return false;" draggable="false" ng-src="{{ img }}" ng-click="nextImage()" ng-show="!loading" class="effect" />' +
        '  <a class="nav-right" ng-click="nextImage()"><i class="fa fa-angle-right"></i></a>' +
        '  <span class="info-text">{{ index + 1 }}/{{ images.length }} - {{ description }}</span>' +
        '  <div class="ng-thumbnails-wrapper">' +
        '    <div class="ng-thumbnails slide-left">' +
        '      <div ng-repeat="i in images">' +
        '        <img ng-src="{{ i.thumb }}" ng-class="{\'active\': index === $index}" ng-click="changeImage($index)" />' +
        '      </div>' +
        '    </div>' +
        '  </div>' +
        '</div>'
    );

    return {
        restrict: 'EA',
        scope: {
            imageList: '=',
            isadmin: '=',
            roomid: '=',
            thumbsNum: '@'
        },
        templateUrl: function (element, attrs) {
            return attrs.templateUrl || defaults.templateUrl;
        },
        link: function (scope, element, attrs) {
            setScopeValues(scope, attrs);

            if (scope.thumbsNum >= 11) {
                scope.thumbsNum = 11;
            }

            var $body = $document.find('body');
            var $thumbwrapper = angular.element(document.querySelectorAll('.ng-thumbnails-wrapper'));
            var $thumbnails = angular.element(document.querySelectorAll('.ng-thumbnails'));

            scope.images = [];
            scope.index = 0;
            scope.opened = false;

            scope.thumb_wrapper_width = 0;
            scope.thumbs_width = 0;

            var loadImage = function (i) {
                var deferred = $q.defer();
                var image = new Image();

                image.onload = function () {
                    scope.loading = false;
                    if (typeof this.complete === false || this.naturalWidth === 0) {
                        deferred.reject();
                    }
                    deferred.resolve(image);
                };

                image.onerror = function () {
                    deferred.reject();
                };

                image.src = scope.images[i].img;
                scope.loading = true;

                return deferred.promise;
            };

            var showImage = function (i) {
                loadImage(scope.index).then(function (resp) {
                    scope.img = resp.src;
                    smartScroll(scope.index);
                });
                scope.description = scope.images[i].description || '';
            };

            scope.showImageDownloadButton = function () {
                if (scope.images[scope.index] == null || scope.images[scope.index].downloadSrc == null) return
                var image = scope.images[scope.index];
                return angular.isDefined(image.downloadSrc) && 0 < image.downloadSrc.length;
            };

            scope.getImageDownloadSrc = function () {
                if (scope.images[scope.index] == null || scope.images[scope.index].downloadSrc == null) return
                return scope.images[scope.index].downloadSrc;
            };

            scope.changeImage = function (i) {
                scope.index = i;
                showImage(i);
            };

            scope.nextImage = function () {
                scope.index += 1;
                if (scope.index === scope.images.length) {
                    scope.index = 0;
                }
                showImage(scope.index);
            };

            scope.prevImage = function () {
                scope.index -= 1;
                if (scope.index < 0) {
                    scope.index = scope.images.length - 1;
                }
                showImage(scope.index);
            };

            var setRoomMainPhoto = function (photo, value) {
                if (value) {
                    for (var i = 0; i < scope.images.length; i++) {
                        if (scope.images[i] === photo) {
                            var photoSetting = {
                                PhotoId: photo.id,
                                RoomId: scope.roomid,
                                IsMain: value
                            };
                            filesUploadService.setMainPhoto(photoSetting).then(function (result) {
                                photo.isMain = value;
                            }, function (error) {
                                alert("error: " + error);
                            });
                        } else if (scope.images[i].isMain) {
                            scope.images[i].isMain = false;
                            var photoSetting = {
                                PhotoId: scope.images[i].id,
                                RoomId: scope.roomid,
                                IsMain: scope.images[i].isMain
                            };
                            filesUploadService.setMainPhoto(photoSetting).then(function (result) {
                            }, function (error) {
                                alert("error: " + error);
                            });
                        }
                    }
                } else {
                    var photoSetting = {
                        PhotoId: photo.id,
                        RoomId: scope.roomid,
                        IsMain: value
                    };
                    filesUploadService.setMainPhoto(photoSetting).then(function (result) {
                        photo.isMain = value;
                    }, function (error) {
                        alert("error: " + error);
                    });
                }
            };

            var setUserMainPhoto = function (photo, value) {
                if (value) {
                    for (var i = 0; i < scope.images.length; i++) {
                        if (scope.images[i] === photo) {
                            var photoSetting = {
                                PhotoId: photo.id,                                
                                IsMain: value
                            };
                            filesUploadService.setUserMainPhoto(photoSetting).then(function (result) {                                
                                var found = $filter('filter')($rootScope.profile.photos, { id: photo.id }, true);
                                if (found.length) {
                                    found[0].isMain = value;
                                    photo.isMain = value;
                                }
                            }, function (error) {
                                alert("error: " + error);
                            });
                        } else if (scope.images[i].isMain) {
                            scope.images[i].isMain = false;
                            var photoSetting = {
                                PhotoId: scope.images[i].id,                                
                                IsMain: scope.images[i].isMain
                            };
                            filesUploadService.setUserMainPhoto(photoSetting).then(function (result) {
                            }, function (error) {
                                alert("error: " + error);
                            });
                        }
                    }
                } else {
                    var photoSetting = {                        
                        RoomId: scope.roomid,
                        IsMain: value
                    };
                    filesUploadService.setUserMainPhoto(photoSetting).then(function (result) {
                        var found = $filter('filter')($rootScope.profile.photos, { id: photo.id }, true);
                        if (found.length) {
                            found[0].isMain = value;
                            photo.isMain = value;
                        }                        
                    }, function (error) {
                        alert("error: " + error);
                    });
                }
            };

            scope.setMainPhoto = function (photo, value) {
                if (scope.roomid) {
                    setRoomMainPhoto(photo, value);
                } else {
                    setUserMainPhoto(photo, value);
                }
            };
            
            var deletePhotoFromRoom = function (photoId, index) {
                var photoSetting = {
                    PhotoId: photoId,
                    RoomId: scope.roomid
                };
                filesUploadService.deletePhotoFromRoom(photoSetting).then(function (result) {
                    if (result.success) {
                        scope.images.splice(index, 1);
                    }
                }, function (error) {
                    alert("error: " + error);
                });
            };

            var deletePhotoFromUser = function (photoId, index) {
                var photoSetting = {
                    PhotoId: photoId,                    
                };
                filesUploadService.deletePhotoFromUser(photoSetting).then(function (result) {
                    if (result.success) {                        
                        //scope.images.splice(index, 1);
                        $rootScope.profile.photos.splice(index, 1);
                    }
                }, function (error) {
                    alert("error: " + error);
                });
            };

            scope.removeItem = function (photoId, index) {                
                if (scope.roomid) {
                    deletePhotoFromRoom(photoId, index);
                } else {
                    deletePhotoFromUser(photoId, index);
                }                
            };            

            scope.openGallery = function (i) {
                if (typeof i !== undefined) {
                    scope.index = i;
                    showImage(scope.index);
                }
                scope.opened = true;

                $timeout(function () {
                    var calculatedWidth = calculateThumbsWidth();
                    scope.thumbs_width = calculatedWidth.width;
                    $thumbnails.css({ width: calculatedWidth.width + 'px' });
                    $thumbwrapper.css({ width: calculatedWidth.visible_width + 'px' });
                    smartScroll(scope.index);
                });
            };

            scope.closeGallery = function () {
                scope.opened = false;
            };

            $body.bind('keydown', function (event) {
                if (!scope.opened) {
                    return;
                }
                var which = event.which;
                if (which === keys_codes.esc) {
                    scope.closeGallery();
                } else if (which === keys_codes.right || which === keys_codes.enter) {
                    scope.nextImage();
                } else if (which === keys_codes.left) {
                    scope.prevImage();
                }

                scope.$apply();
            });

            var calculateThumbsWidth = function () {
                var width = 0,
                    visible_width = 0;
                angular.forEach($thumbnails.find('img'), function (thumb) {
                    width += thumb.clientWidth;
                    width += 10; // margin-right
                    visible_width = thumb.clientWidth + 10;
                });
                return {
                    width: width,
                    visible_width: visible_width * scope.images.length
                };
            };

            var smartScroll = function (index) {
                $timeout(function () {
                    var len = scope.images.length,
                        width = scope.thumbs_width,
                        current_scroll = $thumbwrapper[0].scrollLeft,
                        item_scroll = parseInt(width / len, 10),
                        i = index + 1,
                        s = Math.ceil(len / i);

                    $thumbwrapper[0].scrollLeft = 0;
                    $thumbwrapper[0].scrollLeft = i * item_scroll - (s * item_scroll);
                }, 100);
            };

            scope.convertArrayToImageGalery = function () {
                if (!scope.imageList) {
                    return;
                }
                scope.images = [];
                angular.forEach(scope.imageList, function (value, key) {
                    scope.images.push({
                        thumb: $rootScope.imagePath(value.image, $rootScope.IMAGE_SIZE.thumb),
                        img: $rootScope.imagePath(value.image, $rootScope.IMAGE_SIZE.original),
                        id: value.id,
                        description: 'image description',
                        isMain: value.isMain
                    });
                });
            };           

            scope.$watchCollection("imageList", function (newVal, oldVal) {               
                scope.convertArrayToImageGalery();               
            });           
        }
    };
};
