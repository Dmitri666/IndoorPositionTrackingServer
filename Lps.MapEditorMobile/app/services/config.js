'use strict';
app.service('config', ['$localStorage', '$rootScope', '$location', 'bookingService', 'hubService', 'toaster', '$timeout', 'USER_ROLES', 'authService', 'BOOKING_STATE',
    function ($localStorage, $rootScope, $location, bookingService, hubService, toaster, $timeout, USER_ROLES, authService, BOOKING_STATE) {

        var that = this;
        
        this.model = $localStorage;

        this.model.isInitialized = $localStorage.isInitialized || false;
        this.model.identity = $localStorage.identity ||
            {
                id: "",
                name: "",
                phone: "",
                image: "",
                email: "",
                photo: ""
            };
        this.model.booking = {};
        //this.model.isHubCreated = false;        

        var restoreBookingRequest = function () {
            var isUser = authService.permissionCheck([USER_ROLES.User]);
            if (that.model.booking && isUser) {
                bookingService.saveBooking(that.model.booking).then(function (result) {                    
                    that.model.booking = {};
                    that.monitorBookingRequest();
                    $location.path('/bookinghistory');
                }, function (error) {
                    alert("error: " + error);
                });
            } else {
                $location.path('/profile');
            }
        };

        var createHub = function () {
            var hub = hubService.getHub();
            hub.client.bookingStateChanged = function (booking) {
                angular.forEach($rootScope.bookingHistoryList, function (item, i) {
                    if (item.bookingId === booking.bookingId) {
                        item.state = booking.state;
                    }
                });
                $rootScope.$apply();
                if (booking.state === BOOKING_STATE.Accepted) {
                    $timeout(function () {
                        toaster.pop('success', "Buchungsbestätigung", "Hiermit bestätigen wir Ihre vom " + booking.time + ", datierte Reservierung in '" + booking.name + "' Haus.", 10000);
                    }, 100);
                } else if (booking.state === BOOKING_STATE.Rejected) {
                    toaster.pop('error', "Buchungen ablehnen", "Ihre Buchung abgelehnt: vom " + booking.time + ", datierte Reservierung in '" + booking.name + "' Haus.", 10000);
                }              
            };
            hubService.start().then(function () {               
                //hub.server.joinGroup(authService.authentication.userName);
                //that.model.isHubCreated = true;
            }, function (error) {
                alert("error: " + error);
            });
        };

        this.monitorBookingRequest = function () {
            var isUser = authService.permissionCheck([USER_ROLES.User]);
            if (!isUser ) { // || that.model.isHubCreated) {
                return;
            }
            $timeout(function () {
                createHub();
            }, 1000);           
        };

        this.destroyHub = function () {
            var isUser = authService.permissionCheck([USER_ROLES.User]);
            if (!isUser ) { // || !that.model.isHubCreated) {
                return;
            }
            var hub = hubService.getHub();            
            //hub.server.leaveGroup(authService.authentication.userName);
            hubService.stop();
            that.model.isHubCreated = false;
        };

        this.saveBooking = function (booking) {
            this.model.booking = booking;
        };


        this.saveProfile = function (profile) {
            this.model.isInitialized = true;
            this.model.identity = profile;
            $rootScope.profile = this.model.identity;
            restoreBookingRequest();            
        };
        this.deleteProfile = function () {            
            this.model.isInitialized = false;
            this.model.identity = {};
            $rootScope.profile = {};
            $localStorage.$reset();            
        };

        function init() {
            $rootScope.profile = that.model.identity;            
            that.monitorBookingRequest();
        };

        init();
}]);