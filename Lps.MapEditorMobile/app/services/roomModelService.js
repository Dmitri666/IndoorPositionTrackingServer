
app.factory('roomModelService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var roomModelService = {};
    
    roomModelService.getJsonModel = function (id) {
        var deferred = $q.defer();
        $http.get(serviceBase + 'api/room/GetJsonRoomModel/' + id).success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    };

    roomModelService.getSvgModel = function (id) {
        var deferred = $q.defer();
        $http.get(serviceBase + 'api/room/GetSvgRoomModel/' + id).success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    };

    roomModelService.saveJsonModel = function (model) {
        var deferred = $q.defer();
        $http.post(serviceBase + 'api/room/SaveJsonRoomModel', model).success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    };
    
    roomModelService.getBackgroundBeacons = function () {
        var deferred = $q.defer();
        $http.get(serviceBase + 'api/AltBeaconsData/GetBackgroundBeacons').success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    };

    roomModelService.getLocales = function () {
        var deferred = $q.defer();
        $http.get(serviceBase + 'api/RoomMobile').success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    };

    roomModelService.getBeaconsInRoom = function (id) {
        var deferred = $q.defer();
        $http.get(serviceBase + 'api/room/GetBeaconsInRoom/' + id).success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    };


    roomModelService.updateBeaconIdentifier = function (model) {
        var deferred = $q.defer();
        $http.post(serviceBase + 'api/room/UpdateBeaconIdentifier', model).success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    };
    
    return roomModelService;
}]);

