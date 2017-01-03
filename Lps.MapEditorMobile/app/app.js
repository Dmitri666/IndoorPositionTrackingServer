
var app = angular.module('AngularAuthApp', [
    'ngRoute',
    'LocalStorageModule',
    //'angular-loading-bar',    
    'SignalR',
    'ngStorage',
    'angular.filter',
    'ui.bootstrap',
    'ngMap',
    'toaster',
    //'ngAnimate',
    "mobile-angular-ui",
    //"mobile-angular-ui.core.sharedState",
    "rzModule",
    "wt.responsive",
    "angularFileUpload"
]);

//var serviceUrl = 'http://192.168.19.102/LpsServer';
var serviceUrl = 'http://localhost/LpsServer';
//var serviceUrl = 'http://gloobar.de/api';

app.constant('ngAuthSettings', {
    signalRServiceUrl: serviceUrl + '/signalR',
    signalRHubUrl: serviceUrl + '/signalR/hubs',
    apiServiceBaseUri: serviceUrl + '/',
    clientId: 'ngAuthApp',
    debugEnabled: true
});

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "app/views/home.html"
        , reloadOnSearch: false
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "app/views/login.html"
        , reloadOnSearch: false
    });

    $routeProvider.when("/help", {
        controller: "helpController",
        templateUrl: "app/views/help.html"
        , reloadOnSearch: false
    });    

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "app/views/signup.html"
        , reloadOnSearch: false
    });

    $routeProvider.when("/search", {
        controller: "searchController",
        templateUrl: "app/views/search.html"
        , reloadOnSearch: false
    });
    
    $routeProvider.when("/tryroom", {
        controller: "tryRoomController",
        templateUrl: "app/views/tryroom.html"
        , reloadOnSearch: false
    });

    $routeProvider.when("/search/:showmap", {
        controller: "searchController",
        templateUrl: "app/views/search.html"
        , reloadOnSearch: false
    });

    $routeProvider.when("/location/:id", {
        controller: "locationController",
        templateUrl: "app/views/location.html"
        , reloadOnSearch: false
    });

    $routeProvider.when("/associate", {
        controller: "associateController",
        templateUrl: "app/views/associate.html"
        , reloadOnSearch: false
    });

    $routeProvider.when("/roomnew/:id", {
        controller: "roomEditorController",
        templateUrl: "app/views/roomnew.html",
        reloadOnSearch: false,
        resolve: {
            permission: function (authService, USER_ROLES) {
                return authService.isAuthorized([USER_ROLES.Administrator, USER_ROLES.BarOwner]);
            }
        }
    });

    $routeProvider.when("/tokens", {
        controller: "tokensManagerController",
        templateUrl: "app/views/tokens.html"
        , reloadOnSearch: false
    });

    $routeProvider.when("/profile", {
        controller: "profileController",
        templateUrl: "app/views/profile.html",
        reloadOnSearch: false,
        resolve: {
            permission: function (authService, USER_ROLES) {
                return authService.isAuthorized([USER_ROLES.Administrator, USER_ROLES.BarOwner, USER_ROLES.User]);
            }
        }
    });

    $routeProvider.when("/registration", {
        controller: "registrationController",
        templateUrl: "app/views/registration.html",
        reloadOnSearch: false,
        resolve: {
            permission: function (authService, USER_ROLES) {
                return authService.isAuthorized([USER_ROLES.Administrator, USER_ROLES.BarOwner]);
            }
        }
    });

    $routeProvider.when("/registration/:id", {
        controller: "registrationController",
        templateUrl: "app/views/registration.html",
        reloadOnSearch: false,
        resolve: {
            permission: function (authService, USER_ROLES) {
                return authService.isAuthorized([USER_ROLES.Administrator, USER_ROLES.BarOwner]);
            }
        }
    });

    //$routeProvider.when("/businesshours/:id", {
    //    controller: "businessHoursController",
    //    templateUrl: "app/views/businessHours.html",
    //    reloadOnSearch: false,
    //    resolve: {
    //        permission: function (authService, USER_ROLES) {
    //            return authService.isAuthorized([USER_ROLES.Administrator, USER_ROLES.BarOwner]);
    //        }
    //    }
    //});

    $routeProvider.when("/bookatable/:id", {
        controller: "bookatableController",
        templateUrl: "app/views/bookatable.html"
        , reloadOnSearch: false
    });

    $routeProvider.when("/bookinghistory", {
        controller: "bookinghistoryController",
        templateUrl: "app/views/bookinghistory.html",
        reloadOnSearch: false,
        resolve: {
            permission: function (authService, USER_ROLES) {
                return authService.isAuthorized([USER_ROLES.Administrator, USER_ROLES.BarOwner, USER_ROLES.User]);
            }
        }
    });

    $routeProvider.when("/kassa/:id", {
        controller: "kassaController",
        templateUrl: "app/views/kassa.html",
        reloadOnSearch: false,
        reloadOnSearch: false,
        resolve: {
            permission: function (authService, USER_ROLES) {
                return authService.isAuthorized([USER_ROLES.Administrator, USER_ROLES.BarOwner]);
            }
        }
    });

    $routeProvider.when("/roomslist", {
        controller: "roomslistController",
        templateUrl: "app/views/roomslist.html",
        reloadOnSearch: false,
        resolve: {
            permission: function (authService, USER_ROLES) {
                return authService.isAuthorized([USER_ROLES.Administrator, USER_ROLES.BarOwner]);
            }
        }
    });

    $routeProvider.when("/kassalist", {
        controller: "roomslistController",
        templateUrl: "app/views/kassaList.html",
        reloadOnSearch: false,
        resolve: {
            permission: function (authService, USER_ROLES) {
                return authService.isAuthorized([USER_ROLES.Administrator, USER_ROLES.BarOwner]);
            }
        }
    });

    $routeProvider.when("/statistics", {
        controller: "statisticsController",
        templateUrl: "app/views/statistics.html",
        reloadOnSearch: false,
        resolve: {
            permission: function (authService, USER_ROLES) {
                return authService.isAuthorized([USER_ROLES.Administrator, USER_ROLES.BarOwner]);
            }
        }
    });

    $routeProvider.when("/rating/:id", {
        controller: "ratingController",
        templateUrl: "app/views/rating.html",
        reloadOnSearch: false,
        resolve: {
            permission: function (authService, USER_ROLES) {
                return authService.isAuthorized([USER_ROLES.Administrator, USER_ROLES.BarOwner, USER_ROLES.User]);
            }
        }
    });

    $routeProvider.when("/favorite", {
        controller: "favoriteController",
        templateUrl: "app/views/favorite.html",
        reloadOnSearch: false,
        resolve: {
            permission: function (authService, USER_ROLES) {
                return authService.isAuthorized([USER_ROLES.Administrator, USER_ROLES.BarOwner, USER_ROLES.User]);
            }
        }
    });

    $routeProvider.when("/admin/beaconInRoom", {
        controller: "beaconInRoomController",
        templateUrl: "app/views/beaconInRoom.html",
        reloadOnSearch: false
    });

    $routeProvider.when("/unauthorizedAccess", {
        templateUrl: 'app/views/unauthorizedAccess.html'
        , reloadOnSearch: false
    });

    $routeProvider.otherwise({ redirectTo: "/search" });

});

app.constant('USER_ROLES', {
    Administrator: 'Administrator',
    BarOwner: 'BarOwner',
    User: 'User'
});

app.constant('IMAGE_SIZE', {
    original: 'original',
    medium: 'medium',
    thumb: 'thumb'
});

app.constant('BOOKING_STATE', {
    Waiting: 0,
    Accepted: 1,
    Rejected: 2,
    Canceled: 3,
    BarOwnerAccepted: 4
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);

app.run(function ($rootScope, $interval) {
    $interval(function () {
        $rootScope.AssignedDate = Date;
    }, 1000)
});


