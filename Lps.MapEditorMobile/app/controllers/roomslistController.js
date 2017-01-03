'use strict';
app.controller('roomslistController', ['$scope', '$location', 'roomService',
function ($scope, $location, roomService) {    
    
    function init() {
        roomService.getRoomListByUser().then(function (result) {
            $scope.roomsList = result;
        }, function (error) {
            alert("error: " + error);
        });        
    };

    $scope.newRoom = function () {
        $location.path("/registration");
    };

    init();

}]);