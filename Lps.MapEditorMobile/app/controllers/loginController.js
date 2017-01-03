'use strict';
app.controller('loginController', ['$scope', '$rootScope', '$location', 'authService', 'ngAuthSettings', 'profileService', 'config', 'bookingService',
    function ($scope, $rootScope, $location, authService, ngAuthSettings, profileService, config, bookingService) {       

        $scope.authExternalProvider = function (provider) {

            var redirectUri = location.protocol + '//' + location.host + '/editor/authcomplete.html';

            var externalProviderUrl = ngAuthSettings.apiServiceBaseUri + "api/Account/ExternalLogin?provider=" + provider
                                                                        + "&response_type=token&client_id=" + ngAuthSettings.clientId
                                                                        + "&redirect_uri=" + redirectUri;
            window.$windowScope = $scope;

            var oauthWindow = window.open(externalProviderUrl, "Authenticate Account", "location=0,status=0,width=600,height=750");
        };

        $scope.authCompletedCB = function (fragment) {

            $scope.$apply(function () {

                if (fragment.haslocalaccount == 'False') {

                    authService.logOut();

                    authService.externalAuthData = {
                        provider: fragment.provider,
                        userName: fragment.external_user_name,
                        externalAccessToken: fragment.external_access_token
                    };

                    $location.path('/associate');

                }
                else {
                    //Obtain access token and redirect to orders
                    var externalData = { provider: fragment.provider, externalAccessToken: fragment.external_access_token };
                    authService.obtainAccessToken(externalData).then(function (response) {

                        profileService.getUserProfile().then(function (result) {
                            config.saveProfile(result);
                            $location.path('/profile');
                        }, function (error) {
                            alert("error: " + error);
                        });

                    },
                 function (err) {
                     $scope.message = err.error_description;
                 });
                }

            });
        }


        $scope.login = function () {
            authService.login($rootScope.loginData).then(function (response) {
                profileService.getUserProfile().then(function (result) {
                    config.saveProfile(result);
                    $location.path('/profile');
                }, function (error) {
                    alert("error: " + error);
                });
            }, function (err) {
                $rootScope.message = err.error_description;
            });
        };

    }]);
