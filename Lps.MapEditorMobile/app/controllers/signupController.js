'use strict';
app.controller('signupController', ['$scope', '$location', '$timeout', 'authService', function ($scope, $location, $timeout, authService) {

    $scope.savedSuccessfully = false;
    $scope.message = "";
    $scope.messageRestaurants = "";

    $scope.registration = {
        userName: "",
        password: "",
        email: "",
        confirmPassword: ""
    };

    $scope.registrationBarOwner = {
        userName: "",
        password: "",
        company: "",
        email: "",
        confirmPassword: ""
    };

    $scope.signUp = function () {
        authService.saveRegistration($scope.registration).then(function (response) {                        
            if (response.success) {
                $scope.savedSuccessfully = true;
                $location.path('/login');
            }
        },function (response) {
             var errors = [];
             for (var key in response.data.modelState) {
                 for (var i = 0; i < response.data.modelState[key].length; i++) {
                     errors.push(response.data.modelState[key][i]);
                 }
             }
             $scope.message = "Failed to register user due to:" + errors.join(' ');
         });
    };

    $scope.signUpRestaurants = function () {
        authService.saveRegistrationBarOwner($scope.registrationBarOwner).then(function (response) {            
            if (response.success) {
                $scope.savedSuccessfully = true;
                $location.path('/login');
            }
        }, function (response) {
            var errors = [];
            for (var key in response.data.modelState) {
                for (var i = 0; i < response.data.modelState[key].length; i++) {
                    errors.push(response.data.modelState[key][i]);
                }
            }
            $scope.messageRestaurants = "Failed to register user due to:" + errors.join(' ');
        });
    };

}]);