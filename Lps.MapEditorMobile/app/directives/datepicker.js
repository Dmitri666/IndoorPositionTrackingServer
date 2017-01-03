"use strict";
app.directive('datepicker', [function () {
    return {
        restrict: 'C',
        link: function (scope, el) {
            if (!el.length) return;

            if (typeof $.fn.datepicker != 'undefined') {
                el.datepicker();
            }
        }
    };
}]);