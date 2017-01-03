app.factory('ratingService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var ratingService = {};

    var _saveRating = function (review) {
        var deferred = $q.defer();
        $http.post(serviceBase + 'api/room/SaveRating', review).success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    };
   

    ratingService.saveRating = _saveRating;

    return ratingService;
}]);

