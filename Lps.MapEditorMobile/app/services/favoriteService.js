
app.factory('favoriteService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var favoriteService = {};

    var _getFavorits = function () {
        var deferred = $q.defer();
        $http.get(serviceBase + 'api/Favorits').success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    };
   
    var _addFavorite = function (id) {
        var deferred = $q.defer();
        $http.get(serviceBase + 'api/Favorits/insert/' + id).success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    };

    var _deleteFavorite = function (id) {
        var deferred = $q.defer();
        $http.get(serviceBase + 'api/Favorits/delete/' + id).success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    };
   
    favoriteService.getFavorits = _getFavorits;
    favoriteService.addFavorite = _addFavorite;
    favoriteService.deleteFavorite = _deleteFavorite;

    return favoriteService;
}]);

