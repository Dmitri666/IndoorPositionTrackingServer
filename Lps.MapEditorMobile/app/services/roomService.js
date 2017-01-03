
app.factory('roomService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var roomService = {};

    var _getLocale = function (id) {
        var deferred = $q.defer();
        $http.get(serviceBase + 'api/room/GetRoomById/' + id).success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    };
 
    var _saveRoom = function (requestRoomData) {
        var deferred = $q.defer();
        $http.post(serviceBase + 'api/room/SaveRoom', requestRoomData).success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    };

    var _saveRoomModel = function (model) {
        var deferred = $q.defer();
        $http.post(serviceBase + 'api/room/SaveRoomModel', model).success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    };

    var _getRoomModel = function (id) {
        var deferred = $q.defer();
        $http.get(serviceBase + 'api/room/GetRoomModel/' + id).success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    };

    var _getRoomListByUser = function () {
        var deferred = $q.defer();
        $http.get(serviceBase + 'api/room/getRoomListByUser').success(function (data) {
            deferred.resolve(data);            
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    };

    var _getLocations = function (searchSettings) {
        var deferred = $q.defer();
        $http.post(serviceBase + 'api/room/GetAllLocations', searchSettings).success(function (data) {
            if (data) {
                deferred.resolve(data);
            } else {
                deferred.reject(data);
            }
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    };

    var _getKitchenTypes = function () {
        var deferred = $q.defer();
        $http.get(serviceBase + 'api/room/GetKitchenTypes').success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });

        return deferred.promise;
    };

    var _getKitchenMenuTypes = function () {
        var deferred = $q.defer();
        $http.get(serviceBase + 'api/room/GetKitchenMenuTypes').success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });

        return deferred.promise;
    };

    var _getKitchenInternationalTypes = function () {
        var deferred = $q.defer();
        $http.get(serviceBase + 'api/room/GetKitchenInternationalTypes').success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });

        return deferred.promise;
    };

    var _getSpecializationTypes = function () {
        var deferred = $q.defer();
        $http.get(serviceBase + 'api/room/GetSpecializationTypes').success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });

        return deferred.promise;
    };


    roomService.getLocale = _getLocale;
    roomService.getRoomListByUser = _getRoomListByUser;
    roomService.saveRoom = _saveRoom;
    roomService.getLocations = _getLocations;
    roomService.getKitchenTypes = _getKitchenTypes;
    roomService.getKitchenMenuTypes = _getKitchenMenuTypes;
    roomService.getKitchenInternationalTypes = _getKitchenInternationalTypes;
    roomService.getRoomModel = _getRoomModel;
    roomService.saveRoomModel = _saveRoomModel;
    roomService.getSpecializationTypes = _getSpecializationTypes;

    return roomService;
}]);

