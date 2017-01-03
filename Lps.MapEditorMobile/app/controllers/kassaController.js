'use strict';
app.controller('kassaController', ['$rootScope', 'hubService', '$scope', 'bookingService', 'roomService', '$routeParams', 'roomModelService', 'BOOKING_STATE', 'toaster', '$timeout', 'modalService',
                            function ($rootScope, hubService, $scope, bookingService, roomService, $routeParams, roomModelService, BOOKING_STATE, toaster, $timeout, modalService) {
                               
    $scope.toggleAccepted = true;
 
    $scope.editorModel = {
        roomModel: null,
        svgModel: null,
        selectedTables: [],
        zoom: 1,
        tableReservationModel: null
    };
      
    $scope.filters = {
        username: ""
    };

    $scope.bookingEndTime = function (date) {
        var timeArr = date.split('T');
        var test = timeArr[1].split(':');        
        return new Date(1970, 0, 1, parseInt(test[0], 10) +2 , test[1], test[2]);         
    };

    $scope.filterBooking = function (booking) {        
        return booking.state === BOOKING_STATE.Waiting || booking.state === BOOKING_STATE.Accepted;
    };  

    $scope.bookingKassa = function (timeStamp, table) {        
        if (timeStamp.subColumns.length) {
            $scope.showBooking(timeStamp.bookingData);
        } else {
            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Buchung bestätigen',
                headerText: 'Buchung bestätigen?',
                bodyText: 'Are you sure you want to booking?'
            };
            modalService.showModal({}, modalOptions).then(function (result) {
                _updateBooking(timeStamp, table);
            });            
        }
    };

    var _updateBooking = function (timeStamp, table) {
        var array = [];
        array.push(table.id);
        var bookingRequest = {
            Time: timeStamp.date,
            PeopleCount: 0,
            Tables: array
        };
        bookingService.saveBooking(bookingRequest).then(function (result) {
            toaster.pop('success', "Gebucht!", "Gebucht", 3000);
            $scope.getBookingMap($scope.buttonStatus);
            $scope.updateTableReservation();
        }, function (error) {
            alert("error: " + error);
        });
    };
    
    $scope.buttonStatus = 0;
    $scope.getBookingMap = function (status) {        
        //$rootScope.loading = true;
        $scope.buttonStatus = status;
        var reservation = {
            time: $scope.buttonStatus === 0 ? dateTodayToIsoString() : dateTomorrowToIsoString(),
            roomId: $routeParams.id,
        };
        bookingService.getBookinMap(reservation).then(function (result) {
            $scope.bookingMapList = result;            
            //$rootScope.loading = false;
        }, function (error) {
            //$rootScope.loading = false;
            alert("error: " + error);
        });
    };
   
    function dateTomorrowToIsoString() {
        var date = new Date();
        function pad(n) { return n < 10 ? '0' + n : n }
        var localIsoString = date.getFullYear() + '-'
            + pad(date.getMonth() + 1) + '-'
            + pad(date.getDate() + 1) + 'T'
            + pad(date.getHours()) + ':'
            + pad(date.getMinutes()) + ':'
            + pad(date.getSeconds());
        if (date.getTimezoneOffset() == 0) localIsoString += 'Z';
        return localIsoString;
    };

    function dateTodayToIsoString() {
        var date = new Date();
        function pad(n) { return n < 10 ? '0' + n : n }
        var localIsoString = date.getFullYear() + '-'
            + pad(date.getMonth() + 1) + '-'
            + pad(date.getDate()) + 'T'
            + pad(date.getHours()) + ':'
            + pad(date.getMinutes()) + ':'
            + pad(date.getSeconds());
        if (date.getTimezoneOffset() == 0) localIsoString += 'Z';
        return localIsoString;
    };

    $scope.showBooking = function (booking) {
        if ($scope.selectedBooking === booking) {
            $scope.selectedBooking = null;
            $scope.updateTableReservation();
        } else {
            $scope.selectedBooking = booking;            
            $timeout(function() {                                    
                $scope.editorModel.tableReservationModel = [];
                angular.forEach(booking.roomTableDataList, function (item) {
                    $scope.editorModel.tableReservationModel.push({
                        bookingId: booking.bookingId,
                        state: 4,
                        tableId: item.id
                    });
                });                
            }, 1);
        }
    };       

    $scope.updateBooking = function (booking, status) {
        if (!booking.bookingId) {
            return;
        }
        var bookingResponse = {
            Accepted: status,
            BookingId: booking.bookingId
        };
        bookingService.updateBooking(bookingResponse).then(function (result) {                     
            booking.state = status ? BOOKING_STATE.Accepted : BOOKING_STATE.Rejected;
            $scope.getBookingMap($scope.buttonStatus);
            $scope.updateTableReservation();
        }, function (error) {
            alert("error: " + error);
        });
    };
    
    $scope.setBooking = function (time, date, capacity) {
        var bookingRequest = {
            Time: toLocalIsoString(date, time.date),
            PeopleCount: capacity,
            Tables: $scope.editorModel.selectedTables.map(function (a) { return a.id; })
        };
        bookingService.saveBooking(bookingRequest).then(function (result) {                        
            toaster.pop('success', "Gebucht!", "Gebucht", 3000);
            $scope.getBookingMap($scope.buttonStatus);
            $scope.updateTableReservation();            
        }, function (error) {
            alert("error: " + error);
        });
    };    

    function dateNowToIsoString(date) {
        function pad(n) { return n < 10 ? '0' + n : n }
        var localIsoString = date.getFullYear() + '-'
            + pad(date.getMonth() + 1) + '-'
            + pad(date.getDate()) + 'T'
            + pad(date.getHours()) + ':'
            + pad(date.getMinutes()) + ':'
            + pad(date.getSeconds());
        if (date.getTimezoneOffset() == 0) localIsoString += 'Z';
        return localIsoString;
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

    function initHub() {
        var hub = hubService.getHub();
        hub.client.NewBooking = function (booking) {            
            $scope.$apply(function () {
                $scope.filters.username = "";
                $scope.requestedBookingList.push(booking);
            });
            toaster.pop('success', "eingehende Buchungsanfragen!", "Incoming booking requests", 3000);
            $scope.getBookingMap($scope.buttonStatus);
            $scope.updateTableReservation();
        };
        hubService.start().then(function () {
            hub.server.joinGroup("Kassa:" + $routeParams.id);
        }, function (error) {
            alert("error: " + error);
        });
    };

    $scope.changeParameters = function (time, date, capacity) {
        if (date && time && time.date) {
            $scope.editorModel.selectedTables.length = 0;
            var tableReservationModelRequest = {
                roomId: $routeParams.id,
                time: toLocalIsoString(date, time.date)
            };
            bookingService.getTableReservationModel(tableReservationModelRequest).then(function (result) {                
                $scope.editorModel.tableReservationModel = result;                
            }, function (error) {
                alert("error: " + error);
            });
        }
    };

    $scope.updateTableReservation = function () {
        var tableReservationModelRequest = {
            roomId: $routeParams.id,
            time: dateNowToIsoString(new Date())
        };
        bookingService.getTableReservationModel(tableReservationModelRequest).then(function (result) {            
            $scope.editorModel.tableReservationModel = result;            
        }, function (error) {
            alert("error: " + error);
        });
    };

    function init() {        
        if (!$routeParams.id) {
            return;
        }
        $scope.getBookingMap($scope.buttonStatus);
        roomService.getLocale($routeParams.id).then(function (result) {
            $scope.location = result;            
        }, function (error) {
            alert("error: " + error);
        });
        bookingService.getKassaBookingHistoryByRoom($routeParams.id).then(function (result) {
            $scope.requestedBookingList = result;
        }, function (error) {
            alert("error: " + error);
        });                              
        roomModelService.getSvgModel($routeParams.id).then(function (model) {
            if (model === undefined) {
                return;
            }
            $scope.editorModel.svgModel = model;
            $scope.changeParameters();
            initHub();            
        }, function (error) {
            alert("error:" + error);
        });     
    };

    $scope.$on("$destroy", function handler() {
        var hub = hubService.getHub();
        hub.server.leaveGroup("Kassa:" + $routeParams.id);
        hubService.stop();
    });

    init();
    
}]);