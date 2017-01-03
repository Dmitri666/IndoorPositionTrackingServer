'use strict';
app.factory('filesUploadService', ['$http', 'ngAuthSettings', '$q', function ($http, ngAuthSettings, $q) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var filesUploadService = {};

    //function progress(e) {
    //    if (e.lengthComputable) {
    //        var max = e.total;
    //        var current = e.loaded;

    //        var Percentage = (current * 100) / max;
    //        console.log(Percentage);
    //        //$scope.countFrom = Percentage;

    //        if (Percentage >= 100) {
    //            // process completed  
    //            console.log('DONE!');
    //        }
    //    }
    //}

    //var _uploadPhoto = function () {
    //    var deferred = $q.defer();

    //    //$scope.countTo = 100;

    //    // create file input without appending to DOM
    //    var fileInput = document.createElement('input');
    //    fileInput.setAttribute('type', 'file');
    //    fileInput.setAttribute('id', Math.random());

    //    fileInput.onchange = function () {
    //        var fileURI = fileInput.files[0];
    //        var data = new FormData();
    //        data.append("file", fileURI);
    //        data.append("myParameter", "test");

    //        $.ajax({
    //            url: serviceBase + 'api/files/upload',
    //            data: data,
    //            xhr: function () {
    //                var myXhr = $.ajaxSettings.xhr();
    //                if (myXhr.upload) {
    //                    myXhr.upload.addEventListener('progress', progress, false);
    //                }
    //                return myXhr;
    //            },
    //            cache: false,
    //            contentType: false,
    //            processData: false,
    //            type: 'POST',
    //            success: function (data) {
    //                deferred.resolve(data);
    //            },
    //            error: function (error) {
    //                deferred.reject(error);
    //            }
    //        });
    //    };

    //    fileInput.click();

    //    // return a promise
    //    return deferred.promise;
    //};

    function dataURItoBlob(dataURI) {
        // convert base64/URLEncoded data component to raw binary data held in a string
        var byteString;
        if (dataURI.split(',')[0].indexOf('base64') >= 0)
            byteString = atob(dataURI.split(',')[1]);
        else
            byteString = unescape(dataURI.split(',')[1]);

        // separate out the mime component
        var mimeString = dataURI.split(',')[0].split(':')[1].split(';')[0];

        // write the bytes of the string to a typed array
        var ia = new Uint8Array(byteString.length);
        for (var i = 0; i < byteString.length; i++) {
            ia[i] = byteString.charCodeAt(i);
        }

        return new Blob([ia], { type: mimeString });
    }

    var _uploadRoomCanvas = function (dataURL) {
        var deferred = $q.defer();

        var blob = dataURItoBlob(dataURL);
        var data = new FormData();
        data.append("canvasImage", blob);        

        $.ajax({
            url: serviceBase + 'api/files/upload',
            data: data,
            cache: false,
            contentType: false,
            processData: false,
            type: 'POST',
            success: function (data) {
                deferred.resolve(data);
            },
            error: function (error) {
                deferred.reject(error);
            }
        });
        return deferred.promise;
    };

    var _addPhotoToUser = function (photoSetting) {
        var deferred = $q.defer();
        $http.post(serviceBase + 'api/files/AddPhotoToUser', photoSetting).success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    };

    var _deletePhotoFromUser = function (photoSetting) {
        var deferred = $q.defer();
        $http.post(serviceBase + 'api/files/deletePhotoFromUser ', photoSetting).success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    };


    var _addPhototoToRoom = function (photoSetting) {
        var deferred = $q.defer();
        $http.post(serviceBase + 'api/files/AddPhotoToRoom ', photoSetting).success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    };

    var _deletePhotoFromRoom = function (photoSetting) {
        var deferred = $q.defer();
        $http.post(serviceBase + 'api/files/deletePhotoFromRoom ', photoSetting).success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    };

    var _setMainPhoto = function (photoSetting) {
        var deferred = $q.defer();
        $http.post(serviceBase + 'api/files/setmainphoto ', photoSetting).success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    };

    var _setUserMainPhoto = function (photoSetting) {
        var deferred = $q.defer();
        $http.post(serviceBase + 'api/files/setusermainphoto ', photoSetting).success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    };

    filesUploadService.setMainPhoto = _setMainPhoto;
    //filesUploadService.uploadPhoto = _uploadPhoto;
    filesUploadService.uploadRoomCanvas = _uploadRoomCanvas;
    filesUploadService.addPhotoToUser = _addPhotoToUser;
    filesUploadService.addPhotoToRoom = _addPhototoToRoom;
    filesUploadService.deletePhotoFromRoom = _deletePhotoFromRoom;
    filesUploadService.deletePhotoFromUser = _deletePhotoFromUser;
    filesUploadService.setUserMainPhoto = _setUserMainPhoto;

    return filesUploadService;

}]);