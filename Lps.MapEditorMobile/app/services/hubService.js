app.factory('hubService', ['localStorageService','$q',
                        function (localStorageService, $q) {

    

    function init() {
        var authData = localStorageService.get('authorizationData');
        if (authData) {
            $.connection.hub.qs = { 'access_token':authData.token };
        };
    };

    var _getHub = function () {
        return $.connection.lpsHub;
    };

    var _stop = function () {
        $.connection.hub.stop(true, true);
    };

    var _start = function () {
        init();
        var deferred = $q.defer();
        $.connection.hub.start().done(function () {
            deferred.resolve();
        }, function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    };

    return {
        start: _start,
        stop: _stop,
        getHub: _getHub
    };
}]);