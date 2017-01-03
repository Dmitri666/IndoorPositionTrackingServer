//http://stackoverflow.com/questions/22928398/grid-a-licious-with-ng-repeat-in-angularjs-way

"use strict";
app.directive('onLastRepeat', function () {
    return function (scope, element, attrs) {
        if (scope.$last){
            scope.$emit('onRepeatLast', element, attrs);            
        }
    };
});

"use strict";
app.directive('gridalicious', function () {
    return {
        restrict: 'A',
        link: function (scope, el, attrs) {            
            scope.$on('onRepeatLast', function () {
                el.gridalicious({
                    gutter: 15,
                    width: el.data('width') || 200,
                    selector: '> div',
                    animationOptions: {
                        complete: function () {                            
                            $(window).trigger('resize');
                        }
                    }
                });
            });

        }
    };
});



