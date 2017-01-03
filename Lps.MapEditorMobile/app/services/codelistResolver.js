'use strict';
app.service('codelistResolver', ['$localStorage', '$http', '$q', 'ngAuthSettings', function ($localStorage, $http, $q, ngAuthSettings) {
    
    var that = this;

    this.model = $localStorage;
    this.model.codelists = $localStorage.codelists || {};
 
    this.getCodelist = function (nameToSave, callback) {
        var deferred = $q.defer();
        var existingCodelist = this.model.codelists[nameToSave];
        if (existingCodelist) {
            deferred.resolve(existingCodelist);
        } else {
            $http.get(ngAuthSettings.apiServiceBaseUri + 'api/' + nameToSave).success(function (data) {
                that.model.codelists[nameToSave] = data;               
                deferred.resolve(data);
            }).error(function (error) {
                deferred.reject(error);
            });
        }
        return deferred.promise;
    };
    this.save = function (key, list) {
        this.model.codelists[key] = list;
    };

    this.reset = function () {            
        this.model.codelists = {};
    };
}]);
