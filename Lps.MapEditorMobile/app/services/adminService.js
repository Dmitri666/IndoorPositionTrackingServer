
app.factory('adminService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var adminService = {};

    var _joinRoom = function (position) {
        var deferred = $q.defer();
        $http.post(serviceBase + 'api/Chat/SetPosition', position).success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    };

    var _leaveRoom = function (position) {
        var deferred = $q.defer();
        $http.post(serviceBase + 'api/Chat/RemovePosition', position).success(function (data) {
            deferred.resolve(data);          
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    };

    
    

    adminService.joinRoom = _joinRoom;
    adminService.leaveRoom = _leaveRoom;
    
    

    return adminService;
}]);

