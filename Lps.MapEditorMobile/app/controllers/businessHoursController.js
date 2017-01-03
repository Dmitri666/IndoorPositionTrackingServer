//'use strict';
//app.controller('businessHoursController', ['$rootScope', '$scope', '$location', '$routeParams', '$filter', 'roomService', 'filesUploadService',
//    function ($rootScope, $scope, $location, $routeParams, $filter, roomService, filesUploadService) {

//    $scope.roomId = $routeParams.id;
//    $scope.registration = {
//        name: "",
//        street: "",
//        streetNumber: "",
//        city: "",
//        plz: "",
//        email: "",
//        phone: "",
//        homepage: "",
//        latitude: "",
//        longitude: "",
//        isVisibleBusinessHours: false, 
//        kitchenTypes: [],
//        KitchenInternationalTypes: [],
//        businessHours: [],
//        photos: []
//    };

//    $scope.imageGaleryArray = [];
    
//    $scope.businessHours = [
//    {
//        openTime: '9:00 am',
//        closeTime: '10:00 pm',
//        close: false,
//        day: 1,        
//        openTimeSelected: '9:00 am',
//        closeTimeSelected: '10:00 pm'
//    },
//    {
//        openTime: '9:00 am',
//        closeTime: '10:00 pm',
//        close: false,
//        day: 2,        
//        openTimeSelected: '9:00 am',
//        closeTimeSelected: '10:00 pm'
//    },
//    {
//        openTime: '9:00 am',
//        closeTime: '10:00 pm',
//        close: false,
//        day: 3,        
//        openTimeSelected: '9:00 am',
//        closeTimeSelected: '10:00 pm'
//    },
//    {
//        openTime: '9:00 am',
//        closeTime: '10:00 pm',
//        close: false,
//        day: 4,        
//        openTimeSelected: '9:00 am',
//        closeTimeSelected: '10:00 pm'
//    },
//    {
//        openTime: '9:00 am',
//        closeTime: '10:00 pm',
//        close: false,
//        day: 5,        
//        openTimeSelected: '9:00 am',
//        closeTimeSelected: '10:00 pm'
//    },
//    {
//        openTime: '9:00 am',
//        closeTime: '10:00 pm',
//        close: true,
//        day: 6,        
//        openTimeSelected: '9:00 am',
//        closeTimeSelected: '10:00 pm'
//    },
//    {
//        openTime: '9:00 am',
//        closeTime: '10:00 pm',
//        close: true,
//        day: 7,        
//        openTimeSelected: '9:00 am',
//        closeTimeSelected: '10:00 pm'
//    }
//    ];
//    $scope.dayOfWeak = ['Montag', 'Dinstag', 'Mittwoch', 'Donnerstag', 'Freitag', 'Samstag', 'Sonntag'];


//    $scope.kitchenMenuTypeList = [];
//    $scope.item = {};
//    $scope.items = [{
//			            kitchenMenuTypeId: "",
//			            name: "Mezeteller",
//			            price: 56,
//			            description: "mit Würfelschafskäse, Peperoni, Oliven, Gurken, Tomaten, Honigmelone mit Rinderschinken dazu Brot"
//			        },
//			        {
//			            kitchenMenuTypeId: "",
//			            name: "Satalatar",
//			            price: 45,
//			            description: "Rucolasalat mit Parmesankäse und Tomaten angemacht mit Granatapfel-Viniagrette"
//			        },
//			        {
//			            kitchenMenuTypeId: "",
//			            name: "Lammfrikadelle",
//			            price: 18,
//			            description: "Hackfleischfrikadelle aus der Pfanne auf Wunsch mit Tomatensauce"
//			        }];
//    $scope.editing = false;

//    $scope.removeItem = function (index) {
//        $scope.items.splice(index, 1);
//    };
//    $scope.editItem = function (index) {
//        $scope.editing = $scope.items.indexOf(index);
//    };
//    $scope.saveField = function (index) {
//        if ($scope.editing !== false) {
//            $scope.editing = false;
//        }
//    };
//    $scope.addItem = function (item) {
//        $scope.items.push(item);
//        $scope.item = {};
//    };
//    $scope.getKitchenMenuTypeName = function (item) {
//        if ($scope.kitchenMenuTypeList.length === 0) {
//            return 'Not found';
//        }
//        if (!item.kitchenMenuTypeId || 0 === item.kitchenMenuTypeId.length) {
//            item.kitchenMenuTypeId = $scope.kitchenMenuTypeList[0].id;
//            return $scope.kitchenMenuTypeList[0].name;
//        }        
//        var found = $filter('filter')($scope.kitchenMenuTypeList, { id: item.kitchenMenuTypeId }, true);
//        if (found.length) {
//            return found[0].name;
//        } else {
//            return 'Not found';
//        }
//    };

//    $scope.next = function () {
//        $scope.registration.kitchenMenus = $scope.items;
//        $scope.registration.businessHours = $scope.businessHours;
//        roomService.saveRoom($scope.registration).then(function (result) {            
//            if (result.success) {
//                $location.path('/roomnew/' + $routeParams.id);
//            }
//        }, function (error) {
//            alert("error:" + error);            
//        });        
//    };

//    $scope.back = function () {        
//        $location.path('/registration/' + $routeParams.id);
//    };    

//    $scope.uploadPhoto = function () {
//        filesUploadService.uploadPhoto().then(function onCapturePhoto(result) {
//            var photoSetting = {
//                Image: result,
//                RoomId: $routeParams.id
//            };
//            filesUploadService.addPhotoToRoom(photoSetting).then(function (result) {
//                if (result.success) {                    
//                    $scope.imageGaleryArray.push({
//                        thumb: $rootScope.imagePath(result.photo.image, $rootScope.IMAGE_SIZE.thumb),
//                        img: $rootScope.imagePath(result.photo.image, $rootScope.IMAGE_SIZE.original),
//                        description: 'image description',
//                        id: result.photo.id,
//                        isMain: result.photo.isMain
//                    });
//                }
//            }, function (error) {
//                alert("error: " + error);
//            });
//        }).catch(function (error) {
//            alert("error: " + error);
//        });
//    };

//    $scope.convertArrayToImageGalery = function () {
//        if (!$scope.registration.photos) {
//            return;
//        }
//        angular.forEach($scope.registration.photos, function (value, key) {
//            $scope.imageGaleryArray.push({
//                thumb: $rootScope.imagePath(value.image, $rootScope.IMAGE_SIZE.thumb),
//                img: $rootScope.imagePath(value.image, $rootScope.IMAGE_SIZE.original),
//                id: value.id,
//                description: 'image description',
//                isMain: value.isMain
//            });
//        });
//    };

//    function init() {
//        roomService.getKitchenMenuTypes().then(function (result) {
//            $scope.kitchenMenuTypeList = result;
//        }, function (error) {
//            alert("error: " + JSON.stringify(error));
//        });

//        if ($routeParams.id !== undefined) {
//            roomService.getLocale($routeParams.id).then(function (result) {
//                $scope.registration = result;
//                $scope.convertArrayToImageGalery();
//                if ($scope.registration.businessHours.length > 0) {
//                    $scope.businessHours = $scope.registration.businessHours;
//                }
//                if ($scope.registration.kitchenMenus.length > 0) {
//                    $scope.items = $scope.registration.kitchenMenus;
//                }
//            }, function (error) {
//                alert("error: " + error);
//            });
//        }        
//    };

//    init();

//}]);