//http://plnkr.co/edit/Z583G0NVsy2WtmS3jQWh?p=preview
app.directive("rate", function () {
    return {
        restrict: "E",
        scope: {
            max: "=",
            selrating: "="
        },
        link: function (scope, element, attrs, ngModelCtrl) {

            scope.ratings = new Array(scope.max);

            //hover logic
            var hovered = -1;
            scope.isHovered = function (idx) {
                return idx <= hovered;
            };
            scope.hover = function (idx) {
                hovered = idx;
            };
            scope.nohover = function (idx) {
                hovered = -1;
            };

            //selection logic
            scope.select = function (idx) {
                return scope.selrating = idx + 1;
            };

            scope.isSelected = function (idx) {
                return scope.selrating > idx;
            };
        },
        template: '<ul class="rate">\
                    <li ng-repeat="rating in ratings track by $index" ng-class="{hovered: isHovered($index), selected: isSelected($index)}"\
                        ng-mouseenter="hover($index)" ng-mouseleave="nohover()"\
                        ng-click="select($index)">\
                  </li></ul>'
    };
});
