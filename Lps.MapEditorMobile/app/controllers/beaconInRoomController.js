'use strict';
app.controller('beaconInRoomController', ['$scope', 'roomModelService','$location','adminService',
    function ($scope, roomModelService, $location,adminService) {
        $scope.selection = [];

        $scope.actorInRoom = false;

        $scope.toggleSelection = function toggleSelection(id) {
            var idx = $scope.selection.indexOf(id);

            // is currently selected
            if (idx > -1) {
                $scope.selection.splice(idx, 1);
            }
            // is newly selected
            else {
                $scope.selection.push(id);
            }
        };


        $scope.toggleactorInRoom = function toggleactorInRoom() {
            $scope.actorInRoom = !$scope.actorInRoom;
            var position = {
                deviceId: '000', roomId: $scope.selectedRoom , x:0, y:0 };
            if ($scope.actorInRoom) {
                adminService.joinRoom(position);
            } else {
                adminService.leaveRoom(position);
            }
            
        };

        function init() {

            roomModelService.getBackgroundBeacons().then(function(result) {
                $scope.backgroundBeacons = result;
            }, function(error) {
                console.log("error: " + JSON.stringify(error));
            });

            roomModelService.getLocales().then(function(result) {
                $scope.rooms = result;
            }, function(error) {
                console.log("error: " + JSON.stringify(error));
            });

            adminService
        }

        init();

        $scope.$watch("selectedRoom",
            function(selectedRoom, e) {
                roomModelService.getBeaconsInRoom(selectedRoom).then(function(result) {
                    $scope.beaconsInRoom = result;
                }, function(error) {
                    console.log("error: " + JSON.stringify(error));
                });
            }
        );

        $scope.save = function () {
            var model = { roomId: $scope.selectedRoom, beacons: [] };
                $scope.selection.map(function(id) {
                    $scope.backgroundBeacons.map(function(beacon) {
                        if (beacon.id === id) {
                            model.beacons.push({ id: id, id1: beacon.identifier1, id2: beacon.identifier2, id3: beacon.identifier3 });
                        }
                    });
                });

            roomModelService.updateBeaconIdentifier(model).then(function (result) {
                    $location.path('/admin/beaconInRoom');
                }, function (error) {
                    console.log("error: " + JSON.stringify(error));
                });
            };

    }
]);