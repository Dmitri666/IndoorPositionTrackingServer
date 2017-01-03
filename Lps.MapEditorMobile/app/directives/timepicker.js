"use strict";
app.directive('timepicker', [function () {
    return {
        restrict: 'C',
        link: function (scope, el) {
            if (!el.length) return;

            if (typeof $.fn.timepicker != 'undefined') {
                el.timepicker({
                    showAnim: 'blind',
                    showPeriodLabels: false,
                    hours: { starts: 10, ends: 23 },
                    minutes: { interval: 15 },
                    rows: 3,
                    minuteText: 'Minuten',
                    hourText: 'Stunden'
                });
            }
        }
    };
}]);