'use strict';
app.controller('bookinghistoryController', ['$rootScope', 'hubService','$filter','$location','$scope', 'bookingService', 'modalService',
    function ($rootScope, hubService, $filter, $location, $scope, bookingService, modalService) {

    //var locales = [];    

    //$scope.$on("$destroy", function handler() {
    //    var hub = hubService.getHub();
    //    angular.forEach(locales, function (id) {
    //        hub.server.leaveGroup("ReservationModel:" + id);
    //    });
    //    hubService.stop();
    //});

    var _updateBooking = function (booking) {        
        if (!booking || !booking.bookingId) {
            return;
        }
        var bookingResponse = {
            Accepted: false,
            BookingId: booking.bookingId
        };
        bookingService.updateBooking(bookingResponse).then(function (result) {
             bookingService.getBookingHistoryByUser().then(function (result) {
                 $rootScope.bookingHistoryList = result;
             }, function (error) {
                 alert("error: " + error);
             });
        }, function (error) {
            alert("error: " + error);
        });
    };

    $scope.stornoBooking = function (booking) {
        var modalOptions = {
            closeButtonText: 'Cancel',
            actionButtonText: 'Delete Booking',
            headerText: 'Delete Booking?',
            bodyText: 'Are you sure you want to delete this Booking?'
        };
        modalService.showModal({}, modalOptions).then(function (result) {
            _updateBooking(booking);
        });
    };
    
    //function initHub() {
    //    var hub = hubService.getHub();
    //    hub.client.bookingStateChanged = function (booking) {                        
    //        angular.forEach($scope.bookingHistoryList, function (item, i) {
    //            if (item.bookingId === booking.bookingId) {
    //                item.state = booking.state;
    //            }
    //        });
    //        $scope.$apply();
    //        toaster.pop('success', "Buchungsbestätigung", "Hiermit bestätigen wir Ihre vom " + booking.time + ", datierte Reservierung in '" + booking.name +"' Haus.", 10000);
    //    };
    //    hubService.start().then(function () {
    //        angular.forEach(locales, function (id) {
    //            hub.server.joinGroup("ReservationModel:" + id);
    //        });
    //    }, function (error) {
    //        alert("error: " + error);
    //    });
    //};

    function init() {        
        bookingService.getBookingHistoryByUser().then(function (result) {
            $rootScope.bookingHistoryList = result;
            //angular.forEach(result, function (booking) {
            //    if (locales.indexOf(booking.roomId) === -1) {
            //        locales.push(booking.roomId);
            //    }
            //});            
            //initHub();
        }, function (error) {
            alert("error: " + error);
        });        
    };

    init();

}]);