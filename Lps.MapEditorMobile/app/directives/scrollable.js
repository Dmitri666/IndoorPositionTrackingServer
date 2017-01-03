"use strict";
app.directive('scrollable', [function () {
    return {
        restrict: 'A',
        link: function (scope, el) {

            if (!el.length) return;
            
            var nice = el.niceScroll({
                cursorborder: 0,
                cursorcolor: '#16ae9f',
                horizrailenabled: false
            });           
        }
    };
}])