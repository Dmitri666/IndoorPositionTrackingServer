'use strict';
app.controller('bookatableController', ['$rootScope', 'hubService', '$scope', '$routeParams', '$location', 'bookingService', 'roomModelService', 'config',
    function ($rootScope, hubService, $scope, $routeParams, $location, bookingService, roomModelService, config) {
                     
        $scope.editorModel = {
            roomModel: null,
            svgModel: null,
            selectedTables: [],
            tableReservationModel: null
        };
        $scope.reservationWidgetTime = new Date();
        
        $scope.setBooking = function (time, date, capacity) {                                    
            var bookingRequest = {                
                Time: toLocalIsoString(date, time.date),
                PeopleCount: capacity,
                Tables: $scope.editorModel.selectedTables.map(function (a) { return a.id; })
            };
            if ($rootScope.authentication.isAuth) {
                bookingService.saveBooking(bookingRequest).then(function(result) {
                    config.monitorBookingRequest();
                    $location.path('/bookinghistory');
                }, function (error) {
                    alert("error: " + error);
                });
            } else {
                config.saveBooking(bookingRequest);
                $location.path('/home');
            }
        };

        $scope.changeParameters = function (time, date, capacity) {
            if (date && time && time.date) {
                $scope.editorModel.selectedTables.length = 0;
                $scope.reservationWidgetTime = toLocalIsoString(date, time.date);
                var tableReservationModelRequest = {
                    roomId: $routeParams.id,
                    time: $scope.reservationWidgetTime
                };                 
                bookingService.getTableReservationModel(tableReservationModelRequest).then(function (result) {
                    if (result.length > 0) {
                        $scope.editorModel.tableReservationModel = result;
                    }
                }, function (error) {
                    alert("error: " + error);
                });
            }
        }
       
        function refreshTableReservationModel() {
            var tableReservationModelRequest = {
                roomId: $routeParams.id,
                time: $scope.reservationWidgetTime
            };
            bookingService.getTableReservationModel(tableReservationModelRequest).then(function (result) {
                if (result.length > 0) {
                    $scope.editorModel.tableReservationModel = result;
                }
            }, function (error) {
                alert("error: " + error);
            });
        };

        function initHub() {
            if (!$rootScope.authentication.isAuth) {
                return;
            }
            var hub = hubService.getHub();
            hub.client.changeTableReservationModel = function (booking) {
                refreshTableReservationModel();
            };
            hubService.start().then(function () {
                hub.server.joinGroup("ReservationModel:" + $routeParams.id);
            }, function (error) {
                alert("error: " + error);
            });
        };
      
        function toLocalIsoString(date, time) {
            function pad(n) { return n < 10 ? '0' + n : n }
            var localIsoString = date.getFullYear() + '-'
                + pad(date.getMonth() + 1) + '-'
                + pad(date.getDate()) + 'T'
                + pad(time.getHours()) + ':'
                + pad(time.getMinutes()) + ':'
                + pad(time.getSeconds());
            if (date.getTimezoneOffset() == 0) localIsoString += 'Z';
            return localIsoString;
        };       

        function init() {
            if ($routeParams.id === undefined) {
                return;
            }            
            roomModelService.getSvgModel($routeParams.id).then(function (model) {
                if (model === undefined) {
                    return;
                }
                $scope.editorModel.svgModel = model;
                initHub();
            }, function (error) {
                alert("error:" + error);
            });
        };

        init();

        $scope.$on("$destroy", function handler() {
            if (!$rootScope.authentication.isAuth) {
                return;
            }
            var hub = hubService.getHub();
            hub.server.leaveGroup("ReservationModel:" + $routeParams.id);
            hubService.stop();
        });

}]);