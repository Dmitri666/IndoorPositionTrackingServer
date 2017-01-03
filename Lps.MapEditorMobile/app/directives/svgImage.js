"use strict";
app.directive('svgImage', [function () {
    return function (scope, elem, attrs) {
        scope.$watch(attrs.svgImage, function (newValue, oldValue) {
            if (newValue && newValue !== oldValue) {
                elem.attr('src', 'data:image/svg+xml;base64,' + window.btoa(newValue));
            }
        });
    };
}]);
