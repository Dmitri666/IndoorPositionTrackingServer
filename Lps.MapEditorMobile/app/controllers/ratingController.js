'use strict';
app.controller('ratingController', ['$scope', '$routeParams', '$location', 'roomService', 'ratingService',
    function ($scope, $routeParams, $location, roomService, ratingService) {

    $scope.id = $routeParams.id;
    $scope.ratings = [{ max: 6, rating: 1, name: "Bitte bewerten" }];    

    $scope.rate = function () {
        var review = {
            description: $scope.reviewDescription,
            roomId: $routeParams.id,
            state: $scope.ratings[0].rating
        };                
        ratingService.saveRating(review).then(function (result) {            
            if (result.success) {
                $location.path('/bookinghistory');
            }            
        }, function (error) {
            alert("error: " + error);
        });
    };

    function init() {
        if ($scope.id === undefined) {
            return;
        }
        roomService.getLocale($scope.id).then(function (result) {
            $scope.location = result;            
        }, function (error) {
            alert("error: " + error);
        });
    };

    init();
}]);