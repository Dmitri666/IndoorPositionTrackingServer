'use strict';
app.controller('locationController', function ($rootScope, $scope, $routeParams, $timeout, roomService, NgMap, favoriteService, codelistResolver, $sce) {
                    
    $scope.id = $routeParams.id;    

    // Set up the default filters.
    $scope.filters = {
        name: "",
        type: ""
    };

    $scope.concatFullStreet = function (location) {
        if (location === undefined) {
            return;
        }        
        return location.street + " " + location.streetNumber + ", " + location.plz + ", " + location.city;        
    };        
    
    $scope.$on('mapInitialized', function (evt, evtMap) {
        $scope.map.customMarkers.template.setVisible(false);
        google.maps.event.trigger($scope.map, 'resize');
        if ($scope.location !== undefined) {
            var myLatLng = new google.maps.LatLng($scope.location.latitude, $scope.location.longitude);
            $scope.map.setCenter(myLatLng);
            $scope.map.markers.markertemplate.setPosition(myLatLng);
            $scope.map.customMarkers.template.setPosition(myLatLng);
        }        
    });
    $scope.showInfoBox = function (event) {
        $scope.map.customMarkers.template.setVisible(true);
        $scope.map.customMarkers.template.setPosition(this.getPosition());
    };
    $scope.clickCustomMarker = function (evt) {
        evt.stopPropagation();
    };
    $scope.closeCustomMarker = function () {
        $scope.map.customMarkers.template.setVisible(false);
    };
    $scope.mapclick = function (evt) {
        this.customMarkers.template.setVisible(false);
    };

    $scope.getFullOpenTime = function (picker) {
        var returnText;
        if (picker.close) {
            returnText = "Close";
        } else if (picker.pauseEnd && picker.pauseStart) {
            returnText = picker.openTime + " - " + picker.pauseStart + ", <br/>" + picker.pauseEnd + " - " + picker.closeTime;
        } else {
            returnText = picker.openTime + " - " + picker.closeTime;
        }
        return $sce.trustAsHtml(returnText);
    };

    $scope.addFavorite = function () {                   
        favoriteService.addFavorite($routeParams.id).then(function (result) {            
            $scope.location.isFavorite = !$scope.location.isFavorite;            
        }, function (error) {
            alert("error: " + error);
        });                    
    };

    $scope.deleteFavorite = function (isFavorite) {       
        favoriteService.deleteFavorite($routeParams.id).then(function (result) {            
            $scope.location.isFavorite = !$scope.location.isFavorite;
        }, function (error) {
            alert("error: " + error);
        });       
    };

    $scope.getKitchenTypeName = function (photos, id) {
        for (var pos in photos) {
            if (photos[pos].hasOwnProperty("id")) {
                if (photos[pos].id == id) {
                    return photos[pos].name;
                }
            }
        }
        return undefined;
    };
    
    function init() {
        if (!$routeParams.id) {
            return;
        }
        roomService.getKitchenMenuTypes().then(function (result) {
            $scope.kitchenMenuTypeList = result;
            $scope.kitchenMenuTypeList.push({});
        }, function (error) {
            alert("error: " + JSON.stringify(error));
        });
        roomService.getKitchenTypes().then(function (result) {
            $scope.kitchenTypesList = result;
        }, function (error) {
            alert("error: " + JSON.stringify(error));
        });
        roomService.getKitchenInternationalTypes().then(function (result) {
            $scope.kitchenInternationalTypesList = result;
        }, function (error) {
            alert("error: " + JSON.stringify(error));
        });
        roomService.getSpecializationTypes().then(function (result) {
            $scope.specializationTypesList = result;
        }, function (error) {
            alert("error: " + JSON.stringify(error));
        });
        roomService.getLocale($routeParams.id).then(function (result) {
            $scope.location = result;
            //$scope.convertArrayToImageGalery();            
            //if (!$scope.$$phase)
            //    $scope.$apply();
        }, function (error) {
            alert("error: " + error);
        });
    };

    init();
});