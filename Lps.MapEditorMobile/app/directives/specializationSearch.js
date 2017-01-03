"use strict";
app.directive("specializationSearch", ['$filter', function ($filter) {

    var tpl = "<div class='row'> \
                    <div class='col-lg-4 col-md-4 col-sm-4 col-xs-12'> \
                        <div class='panel panel-default'> \
                            <div class='panel-heading'> \
                                <h4 class='panel-title'>Cпециализация</h4> \
                            </div> \
                            <div class='panel-body'> \
                                <div class='borderTree tree'> \
                                    <a class='tree-item' \
                                        ng-repeat='specializationType in specializationTypeList | filter: { hierarchie: 1 } | orderBy: \"order\" ' \
                                        ng-click='checkSpecializationType(specializationType)' \
                                        ng-class='{ selected: isSelected(specializationType) }'> \
                                        <span class='tree-item-name'><i ng-class='getTypeIconClass(specializationType)'></i> {{ specializationType.name }}</span> \
                                    </a> \
                                </div> \
                            </div> \
                        </div> \
                    </div> \
                    <div class='col-lg-4 col-md-4 col-sm-4 col-xs-12'> \
                        <div class='panel panel-default'> \
                            <div class='panel-heading'> \
                                <h4 class='panel-title'>Тип</h4> \
                            </div> \
                            <div class='panel-body'> \
                                <div class='borderTree tree'> \
                                    <a class='tree-item' \
                                        ng-repeat='specializationType in secondTypeList | filter: { hierarchie: 2 } | orderBy: \"order\" ' \
                                        ng-click='checkSecondSpecializationType(specializationType)'> \
                                        <span class='tree-item-name'><i ng-class='getSecondTypeIconClass(specializationType)'></i> {{ specializationType.name }}</span> \
                                    </a> \
                                </div> \
                            </div> \
                        </div> \
                    </div> \
                    <div class='col-lg-4 col-md-4 col-sm-4 col-xs-12'> \
                        <div class='panel panel-default'> \
                            <div class='panel-heading'> \
                                <h4 class='panel-title'>Вид</h4> \
                            </div> \
                            <div class='panel-body'> \
                                <div class='borderTree tree'> \
                                    <a class='tree-item' \
                                        ng-repeat='specializationType in secondTypeList | filter: { hierarchie: 3 } | orderBy: \"order\" ' \
                                        ng-click='checkSecondSpecializationType(specializationType)'> \
                                        <span class='tree-item-name'><i ng-class='getSecondTypeIconClass(specializationType)'></i> {{ specializationType.name }}</span> \
                                    </a> \
                                </div> \
                            </div> \
                        </div> \
                    </div> \
                </div>";

    return {
        restrict: "E",
        scope: {
            selectedTypes: "=",
            specializationTypeList: "="
        },
        replace: true,
        link: function (scope, element, attrs) {

            scope.selectedType = {};
            scope.secondTypeList = [];

            scope.checkSecondSpecializationType = function (specializationType) {
                var idx = scope.selectedTypes.indexOf(specializationType);
                if (idx > -1) {
                    scope.selectedTypes.splice(idx, 1);
                } else {
                    scope.selectedTypes.push(specializationType);
                }
            };

            scope.checkSpecializationType = function (specializationType) {
                scope.selectedType = specializationType;
                scope.secondTypeList = $filter('filter')(scope.specializationTypeList, { parentId: specializationType.parentId }, true);

                // remove parent type
                var id = scope.secondTypeList.indexOf(specializationType);
                scope.secondTypeList.splice(id, 1);

                var list = $filter('filter')(scope.selectedTypes, { parentId: specializationType.parentId }, true);                
                if (list.length) {
                    angular.forEach(scope.secondTypeList, function (value) {
                        var idx = scope.selectedTypes.indexOf(value);
                        if (idx > -1) {
                            scope.selectedTypes.splice(idx, 1);
                        }
                    });
                } else {
                    angular.forEach(scope.secondTypeList, function (value) {
                        var idx = scope.selectedTypes.indexOf(value);
                        if (idx <= -1) {
                            scope.selectedTypes.push(value);
                        }
                    });
                }
            };

            scope.isSelected = function (specializationType) {
                if (scope.selectedType === specializationType) {
                    return true;
                } else {
                    return false;
                }
            };

            scope.getTypeIconClass = function (specializationType) {                                                             
                var isChildExist = $filter('filter')(scope.selectedTypes, { parentId: specializationType.parentId }, true);
                if (isChildExist.length) {
                    var all = $filter('filter')(scope.specializationTypeList, { parentId: specializationType.parentId }, true);
                    var checked = $filter('filter')(scope.selectedTypes, { parentId: specializationType.parentId }, true);                    
                    if (all.length === (checked.length + 1)) {
                        return "fa fa-check-square-o fa-lg";
                    } else {                       
                        return "fa fa-minus-square-o fa-lg";                        
                    }                    
                } else {
                    return "fa fa-square-o fa-lg";
                }               
            };

            scope.isChecked = function (specializationType) {
                var idx = scope.selectedTypes.indexOf(specializationType);
                if (idx > -1) {
                    return true;
                } else {
                    return false;
                }
            };
            
            scope.getSecondTypeIconClass = function (specializationType) {
                var isSelected = scope.isChecked(specializationType);
                return isSelected ? "fa fa-check-square-o fa-lg" : "fa fa-square-o fa-lg";
            };

        },
        template: tpl
    };

}]);
