//http://bootsnipp.com/snippets/featured/form-wizard-using-tabs
"use strict";
app.directive("processSteps", ['$location', function ($location) {
    return {
        restrict: "E",
        scope: {},
        link: function (scope, element, attrs) {            

            scope.tabList = [{
                                url: '/registration',
                                icon: 'glyphicon-folder-open',
                                title: 'Step1'
                            },
                            {
                                url: '/businesshours',
                                icon: 'glyphicon-pencil',
                                title: 'Step2'
                            },
                            {
                                url: '/roomnew',
                                icon: 'glyphicon glyphicon-ok',
                                title: 'Step3'
                            }];

            scope.isActive = function (url) {                
                return $location.path().substr(0, url.length) === url;                
            };           
          
        },
        template: '<div class="wizard">\
	                <div class="wizard-inner">\
		                <div class="connecting-line"></div>\
		                <ul class="nav nav-tabs" role="tablist">\
			                <li ng-repeat="tab in tabList" ng-class="{ \'active\' : isActive(tab.url) }">\
                                <a role="tab">\
                                    <span class="round-tab"><i class="glyphicon" ng-class="tab.icon"></i>{{tab.title}}</span>\
				                </a>\
			                </li>\
                        </ul>\
	                </div>\
                </div>'
                                    
    };
}]);
