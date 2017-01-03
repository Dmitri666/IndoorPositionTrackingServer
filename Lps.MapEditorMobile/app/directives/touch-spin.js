"use strict";
app.directive('toggle', [function () {
    return {
        restrict: 'A',
        link: function (scope, el, attrs) {

            if (attrs.toggle == 'touch-spin') {

                //el.tkTouchSpin();

                if (!el.length) return;

                if (typeof $.fn.TouchSpin != 'undefined') {
                    el.TouchSpin();
                }
            }

        }
    };
}]);