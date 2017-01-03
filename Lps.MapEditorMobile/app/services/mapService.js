'use strict';
app.factory('mapService', ['$http', '$q', '$window', function ($http, $q, $window) {

    var mapService = {};
    var currentPosition = null;
    var result = function () {
        return {
            success: false,
            message: '',
            location: ''
        }
    };

    var _getLocation = function (address) {
        var deferred = $q.defer();
        var googleMap = new google.maps.Geocoder();
        googleMap.geocode({ 'address': address }, function (results, status) {
            if (status === google.maps.GeocoderStatus.OK) {
                var ok = new result();
                ok.success = true;
                ok.location = results[0].geometry.location;
                deferred.resolve(ok);
            } else {
                var error = new result();
                error.message = 'The geocode was not successful for the these reasons: ' + status;
                deferred.reject(error);
            }
        });

        return deferred.promise;
    };

    //https://gist.github.com/danasilver/6024009
    var _getCurrentLocationCity = function () {
        var deferred = $q.defer();

        _getCurrentPosition().then(function (latLng) {
            var geocoder = new google.maps.Geocoder();
            geocoder.geocode({ 'latLng': latLng }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    if (results[1]) {
                        for (var i = 0; i < results.length; i++) {
                            if (results[i].types[0] === "locality") {
                                var city = results[i].address_components[0].short_name;
                                var state = results[i].address_components[2].short_name;
                                deferred.resolve(city + ", " + state);
                            }
                        }
                    }
                    else { deferred.reject("No reverse geocode results.") }
                }
                else { deferred.reject("Geocoder failed: " + status) }
            });
        });
        
        return deferred.promise;
    };

    var _getCurrentPosition = function () {
        var deferred = $q.defer();
        if (currentPosition != null) {
            deferred.resolve(currentPosition);
        } else {
            if ($window.navigator.geolocation) {
                $window.navigator.geolocation.getCurrentPosition(function (position) {
                    currentPosition = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);;
                    deferred.resolve(currentPosition);
                },
                function (error) {
                    deferred.reject("Geolocation not available.");
                });
            } else {
                deferred.reject("Geolocation not available.");
            }
        }
        return deferred.promise;
    }

    


    mapService.getCurrentLocationCity = _getCurrentLocationCity;
    mapService.getLocation = _getLocation;
    mapService.getCurrentPosition = _getCurrentPosition;

    return mapService;

}]);