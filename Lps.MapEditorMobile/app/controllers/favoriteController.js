'use strict';
app.controller('favoriteController', ['$scope', '$location', 'favoriteService', function ($scope, $location, favoriteService) { 

    $scope.deleteFavorite = function (room) {
        favoriteService.deleteFavorite(room.id).then(function (result) {
            var idx = $scope.favorits.indexOf(room);
            if (idx > -1) {
                $scope.favorits.splice(idx, 1);
            }
        }, function (error) {
            alert("error: " + error);
        });
    };

    function init() {
        favoriteService.getFavorits().then(function (result) {
            $scope.favorits = result;
        }, function (error) {
            alert("error: " + error);
        });
    };

    init();

}]);