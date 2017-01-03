'use strict';
app.controller('searchController', function ($rootScope, $scope, $filter, $timeout, $location, $routeParams, $route, roomService, $window, mapService, ngAuthSettings, $templateCache, $compile, NgMap) {   

    $scope.ids = [];
    $scope.kitchenInternationalTypeIds = [];
    $scope.selectedFile = [];
    //$scope.searchLocationModel = "";
    //$scope.isChatExist = false;

    $scope.slider = {
        value: 6,
        options: {
            floor: 1,
            ceil: 30,
            showTicks: true,            
            translate: function(value) {
                return value + ' km';
            }
        }
    };

    $scope.activeTab = 1;
    $scope.setTab = function (tab) {
        $scope.activeTab = tab;        
    };
    
    $scope.locations = [];    

    var map;
    $scope.onMapInitialized = function () {
        map = $scope.map;        
        if ($scope.currentPosition != null) {
            $scope.map.setCenter($scope.currentPosition);
        }        
        $scope.$watch('activeTab', function () {
            $timeout(function () {                
                google.maps.event.trigger($scope.map, 'resize');
            }, 1);
        });
        $scope.search();
    };    
    
    $scope.showInfoBox = function (event, data) {
        map.customMarkers.template.setVisible(true);        
        map.customMarkers.template.setPosition(this.getPosition());
        map.setCenter(this.getPosition());
        $scope.infoBox = data;
    };
    $scope.clickCustomMarker = function (evt) {
        evt.stopPropagation();
    };
    $scope.closeCustomMarker = function () {
        map.customMarkers.template.setVisible(false);
    };
    $scope.mapclick = function (evt) {        
        this.customMarkers.template.setVisible(false);
    };
    //$scope.logMouseEnter = function (location) {
    //    if (map === null || map.customMarkers === null) {
    //        return;
    //    }        
    //    $scope.infoBox = location;
    //    map.customMarkers.template.setVisible(true);
    //    map.customMarkers.template.setPosition(new google.maps.LatLng(location.latitude, location.longitude));
    //};
    //$scope.logMouseLeave = function () {
    //    if (map === null || map.customMarkers === null) {
    //        return;
    //    }
    //    map.customMarkers.template.setVisible(false);
    //};
    
    $scope.typeaheadSubmit = function () {
        if ($scope.selectedNumberNonEditableMobile2 && $scope.selectedNumberNonEditableMobile2.id) {
            $location.path('/location/' + $scope.selectedNumberNonEditableMobile2.id);
            $scope.selectedNumberNonEditableMobile2 = null;            
        }
    };
    $scope.$watch('selectedNumberNonEditableMobile2', function (value) {
        if (value && value.id) {
            $location.path('/location/' + value.id);            
        }
    });

    $scope.$watch('selectedNumberNonEditableMobile1', function (value) {
        if (value && value.id) {
            $scope.search();
        }
    });   

    $scope.search = function () {                
        $rootScope.searchCity = $scope.selectedNumberNonEditableMobile1 ? $scope.selectedNumberNonEditableMobile1.value : $rootScope.searchCity;        
        mapService.getLocation($rootScope.searchCity).then(function (result) {
            if (result.success) {

                $scope.circle = result.location;
                $scope.circle.radius = $scope.slider.value * 1000;// 1609.34;
                $timeout(function () {
                    $scope.map.setZoom(13);
                    $scope.map.setCenter(result.location);                                        
                }, 100);
                $scope.activeTab = 1;

                var searchSettings = {
                    radius: $scope.slider.value,
                    latitude: result.location.lat(),
                    longitude: result.location.lng(),

                    kitchenTypes: $scope.ids,
                    kitchenInternationalTypes: $scope.kitchenInternationalTypeIds
                    //locationName: $scope.searchLocationModel,
                    //isChatExist: $scope.isChatExist,                    
                    //selectedFile: selectedFile
                };
                roomService.getLocations(searchSettings).then(function (result) {
                    $scope.locations = result;                                                           
                }, function (error) {
                    alert("error:" + JSON.stringify(error));
                });
            }
        });
    };

    //$scope.toggleSelection = function toggleSelection(id, groupByValue, first) {
    //    var idx = $scope.kitchenInternationalTypeIds.indexOf(id);
    //    if (first) {
    //        if (idx > -1) {
    //            var found = $filter('filter')($scope.kitchenInternationalTypesList, { parentId: groupByValue }, true);
    //            if (found.length) {
    //                angular.forEach(found, function (value, key) {
    //                    var idxTest = $scope.kitchenInternationalTypeIds.indexOf(value.id);
    //                    if (idxTest > -1) {
    //                        $scope.kitchenInternationalTypeIds.splice(idxTest, 1);
    //                    }
    //                });
    //            }
    //        }
    //        else {
    //            var found = $filter('filter')($scope.kitchenInternationalTypesList, { parentId: groupByValue }, true);
    //            if (found.length) {
    //                angular.forEach(found, function (value, key) {
    //                    $scope.kitchenInternationalTypeIds.push(value.id);
    //                });
    //            }
    //        }
    //    } else {
    //        if (idx > -1) {
    //            $scope.kitchenInternationalTypeIds.splice(idx, 1);
    //        }
    //        else {
    //            $scope.kitchenInternationalTypeIds.push(id);
    //        }
    //    }
    //};

    function init() {
        mapService.getCurrentPosition().then(function (result) {
            $scope.currentPosition = result;
        });
        //roomService.getKitchenTypes().then(function (result) {
        //    $scope.kitchenTypesList = result;
        //}, function (error) {
        //    alert("error: " + JSON.stringify(error));
        //});
        //roomService.getKitchenInternationalTypes().then(function (result) {
        //    $scope.kitchenInternationalTypesList = result;
        //}, function (error) {
        //    alert("error: " + JSON.stringify(error));
        //});
        roomService.getSpecializationTypes().then(function (result) {
            $scope.specializationTypeList = result;
        }, function (error) {
            alert("error: " + JSON.stringify(error));
        });
    };

    init();    

});