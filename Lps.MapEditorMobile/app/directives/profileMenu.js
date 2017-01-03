"use strict";
app.directive("profileMenu", ['$rootScope', '$location',  function ($rootScope, $location) {
    return {
        restrict: "E",
        scope: {},
        link: function (scope, element, attrs) {

            scope.profile = $rootScope.profile;
            scope.userRoles = $rootScope.userRoles;
            scope.isAuthorized = $rootScope.isAuthorized;
            scope.imagePath = $rootScope.imagePath;
            scope.IMAGE_SIZE = $rootScope.IMAGE_SIZE;
            scope.logOut = $rootScope.logOut;
            scope.uploadPhoto = $rootScope.uploadPhoto;
                        
            scope.isActive = function (viewLocation) {
                return viewLocation === $location.path();
            };           
          
        },
        template: '<div class="col-md-3">\
                    <div class="element-invisible">\
                        <div class="text-center">\
                            <br />\
                            <img ng-src="{{imagePath(profile.photo, IMAGE_SIZE.medium)}}" class="avatar img-circle" style="height:160px;" alt="avatar"><br/><br/><a href="" ng-click="uploadPhoto()">Upload a photo</a>\
                        </div>\
                        <hr />\
                    </div>\
                    <div class="list-group">\
                        <a class="list-group-item" ng-class="{ \'list-group-item-info\' : isActive(\'/profile\') }" href="#/profile">\
                            <i class="fa fa-user fa-fw"></i>&nbsp; Mein Profil</a>\
                        <a class="list-group-item" ng-class="{ \'list-group-item-info\' : isActive(\'/bookinghistory\') }" href="#/bookinghistory">\
                            <i class="fa fa-book fa-fw"></i>&nbsp; Meine Reservierungen</a>\
                        <a class="list-group-item" ng-class="{ \'list-group-item-info\' : isActive(\'/favorite\') }" href="#/favorite">\
                            <i class="fa fa-book fa-fw"></i>&nbsp; Meine Favoriten</a>\
                        <a class="list-group-item" ng-class="{ \'list-group-item-info\' : isActive(\'/roomslist\') }" href="#/roomslist" ng-if="isAuthorized([userRoles.Administrator, userRoles.BarOwner])">\
                            <i class="fa fa-pencil fa-fw"></i>&nbsp; Mein Restaurant\
                        </a>\
                        <a class="list-group-item" ng-class="{ \'list-group-item-info\' : isActive(\'/kassalist\') }" href="#/kassalist" ng-if="isAuthorized([userRoles.Administrator, userRoles.BarOwner])">\
                            <i class="fa fa-cog fa-fw"></i>&nbsp; Meine Kasse\
                        </a>\
                        <a class="list-group-item" href="" data-ng-click="logOut()"><i class="fa fa-lock fa-fw"></i>&nbsp; Abmelden</a>\
                    </div>\
                </div>'
    };
}]);
