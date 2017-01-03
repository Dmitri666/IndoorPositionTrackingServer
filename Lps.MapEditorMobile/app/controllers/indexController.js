'use strict';
app.controller('indexController', ['$rootScope', '$scope', '$q', '$location', 'authService', 'roomService', 'ngAuthSettings', 'USER_ROLES', 'mapService', 'profileService', 'config', 'IMAGE_SIZE', '$window', 'bookingService', '$sce',
    function ($rootScope, $scope, $q, $location, authService, roomService, ngAuthSettings, USER_ROLES, mapService, profileService, config, IMAGE_SIZE, $window, bookingService, $sce) {

        (function (proxied) {
            window.alert = function () {
                if (ngAuthSettings.debugEnabled) {
                    console.log(new Error().stack);
                }
                //return proxied.apply(this, arguments);            
            };
        })(window.alert);

        // Needed for the loading screen
        $rootScope.$on('$routeChangeStart', function () {
            $rootScope.loading = true;
        });
        $rootScope.$on('$routeChangeSuccess', function () {
            $rootScope.loading = false;
        });
        
        $rootScope.serviceBase = ngAuthSettings.apiServiceBaseUri;
        $scope.signalrHubUrl = $sce.trustAsResourceUrl(ngAuthSettings.signalRHubUrl);

        if ('ontouchstart' in window) {
            $scope.fabricScript = "scripts/fabric_all.js";
        } else {
            $scope.fabricScript = "bower_components/fabric/dist/fabric.js";
        }

        $rootScope.prefixUrl = function (url) {
            return $rootScope.serviceBase + url;
        };

        $rootScope.dayOfWeak = ['Montag', 'Dinstag', 'Mittwoch', 'Donnerstag', 'Freitag', 'Samstag', 'Sonntag'];

        $rootScope.IMAGE_SIZE = IMAGE_SIZE;
        $rootScope.authentication = authService.authentication;
        $rootScope.isAuthorized = authService.permissionCheck;
        $rootScope.userRoles = USER_ROLES;        

        $rootScope.searchLocation = "";
        $rootScope.searchCity = "Düsseldorf, NRW";

        $rootScope.logOut = function () {
            config.destroyHub();
            authService.logOut();
            config.deleteProfile();
            $location.path('/home');
        };

        $rootScope.loginData = {
            userName: "",
            password: "",
            useRefreshTokens: false
        };
        $rootScope.message = "";

        $scope.selectedNumberNonEditable = null;

        var numbers = new Bloodhound({
            datumTokenizer: function (datum) {
                return Bloodhound.tokenizers.whitespace(datum.value);
            },
            queryTokenizer: Bloodhound.tokenizers.whitespace,
            limit: 10,
            remote: {
                url: $rootScope.serviceBase + 'api/room/GetTypeaheadCity?query=%QUERY',
                wildcard: '%QUERY',
                transform: function (response) {
                    return $.map(response, function (item) {
                        return {
                            value: item.description,
                            id: item.id
                        };
                    });
                }
            }
        });
        var numbers2 = new Bloodhound({
            datumTokenizer: function (datum) {
                return Bloodhound.tokenizers.whitespace(datum.value);
            },
            queryTokenizer: Bloodhound.tokenizers.whitespace,
            limit: 10,
            remote: {
                url: $rootScope.serviceBase + 'api/room/GetTypeaheadLocation?query=%QUERY',
                wildcard: '%QUERY',
                transform: function (response) {
                    return $.map(response, function (item) {
                        return {
                            value: item.description,
                            id: item.id
                        };
                    });
                }
            }
        });

        // initialize the bloodhound suggestion engine
        numbers.initialize();
        numbers2.initialize();
        $scope.numbersDataset1 =
        {
            name: 'nba-teams',
            displayKey: 'value',
            source: numbers.ttAdapter(),
            templates: {
                header: '<h3 class="league-name">Stadt:</h3>'
            }
        };
        $scope.numbersDataset2 =
        {
            name: 'nhl-teams',
            displayKey: 'value',
            source: numbers2.ttAdapter(),
            templates: {
                header: '<h3 class="league-name">Restaurant:</h3>'
            }
        };

        $scope.exampleOptionsNonEditable = {
            highlight: true,
            minLength: 0,
            editable: false
        };

        $rootScope.imagePath = function (image, imageSize) {
            if (image) {
                var returnImage;
                switch (imageSize) {
                    case IMAGE_SIZE.original:
                        returnImage = $rootScope.serviceBase + "FileUploads/" + image;
                        break;
                    case IMAGE_SIZE.medium:
                        returnImage = $rootScope.serviceBase + "FileUploads/medium/" + image;
                        break;
                    case IMAGE_SIZE.thumb:
                        returnImage = $rootScope.serviceBase + "FileUploads/thumb/" + image;
                        break;
                    default:
                        returnImage = $rootScope.serviceBase + "FileUploads/" + image;
                        break;
                }
                return returnImage;
            }
            var dummyImage;
            switch (imageSize) {
                case IMAGE_SIZE.original:
                    dummyImage = "http://dummyimage.com/600x400&text=image";
                    break;
                case IMAGE_SIZE.medium:
                    dummyImage = "http://dummyimage.com/600x400&text=image";
                    break;
                case IMAGE_SIZE.thumb:
                    dummyImage = "http://dummyimage.com/150x100&text=image";
                    break;
                default:
                    dummyImage = "http://dummyimage.com/600x400&text=image";
                    break;
            }
            return dummyImage;
        };

        function init() {
            mapService.getCurrentLocationCity().then(function (result) {
                $rootScope.searchCity = result;
            }, function (error) {
                alert("error: " + error);
            });
        };

        init();
}]);