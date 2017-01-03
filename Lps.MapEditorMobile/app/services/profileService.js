'use strict';
app.factory('profileService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var profileService = {};

    var _getUserProfile = function () {
        var deferred = $q.defer();
        $http.post(serviceBase + 'api/Account/GetUserProfile').success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;       
    };

    var _saveUserProfile = function (profile) {
        var deferred = $q.defer();
        $http.post(serviceBase + 'api/Account/saveUserProfile', profile).success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    };

    profileService.getUserProfile = _getUserProfile;
    profileService.saveUserProfile = _saveUserProfile;

    return profileService;

}]);