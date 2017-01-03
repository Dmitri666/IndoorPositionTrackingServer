'use strict';
app.controller('associateController', ['$scope', '$location', '$timeout', 'authService', 'profileService', 'config',
    function ($scope, $location, $timeout, authService, profileService, config) {

        $scope.savedSuccessfully = false;
        $scope.message = "";

        $scope.registerData = {
            userName: authService.externalAuthData.userName,
            provider: authService.externalAuthData.provider,
            externalAccessToken: authService.externalAuthData.externalAccessToken
        };

        $scope.registerExternal = function () {

            authService.registerExternal($scope.registerData).then(function (response) {

                $scope.savedSuccessfully = true;
                $scope.message = "User has been registered successfully, you will be redicted to orders page in 2 seconds.";

                profileService.getUserProfile().then(function (result) {
                    config.saveProfile(result);

                    $location.path('/profile');
                    //startTimer();

                }, function (error) {
                    alert("error: " + error);
                });

            }, function (response) {
                var errors = [];
                for (var key in response.modelState) {
                    errors.push(response.modelState[key]);
                }
                $scope.message = "Failed to register user due to:" + errors.join(' ');
            });
        };

        //var startTimer = function () {
        //    var timer = $timeout(function () {
        //        $timeout.cancel(timer);
        //        $location.path('/profile');
        //    }, 2000);
        //}

}]);