
app.factory('bookingService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var bookingService = {};

    var _getBookinMap = function (reservation) {
        var deferred = $q.defer();
        $http.post(serviceBase + 'api/booking/GetBookinMap', reservation).success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    };

    var _saveBooking = function (bookingRequest) {
        var deferred = $q.defer();
        $http.post(serviceBase + 'api/booking/Request', bookingRequest).success(function (data) {
            deferred.resolve(data);          
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    };

    var _updateBooking = function (bookingResponse) {
        var deferred = $q.defer();
        $http.post(serviceBase + 'api/Booking/Response', bookingResponse).success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    };

    var _getBookingHistoryByUser = function () {
        var deferred = $q.defer();
        $http.get(serviceBase + 'api/Booking/GetBookingHistoryByUser').success(function (data) {
            if (data) {
                deferred.resolve(data);
            } else {
                deferred.reject(data);
            }
        }).error(function (error) {
            deferred.reject(error);
        });

        return deferred.promise;
    };

    var _getKassaBookingHistoryByRoom = function (roomId) {
        var deferred = $q.defer();
        $http.get(serviceBase + 'api/Booking/GetBookingHistoryForKassaByRoom/' + roomId).success(function (data) {
            if (data) {
                deferred.resolve(data);
            } else {
                deferred.reject(data);
            }
        }).error(function (error) {
            deferred.reject(error);
        });

        return deferred.promise;
    };

    var _getTableReservationModel = function (tableReservationModelRequest) {
        var deferred = $q.defer();
        $http.post(serviceBase + 'api/Booking/TableReservationModel', tableReservationModelRequest).success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    };

    bookingService.updateBooking = _updateBooking;
    bookingService.saveBooking = _saveBooking;
    bookingService.getBookingHistoryByUser = _getBookingHistoryByUser;
    bookingService.getKassaBookingHistoryByRoom = _getKassaBookingHistoryByRoom;
    bookingService.getTableReservationModel = _getTableReservationModel;
    bookingService.getBookinMap = _getBookinMap;

    return bookingService;
}]);

