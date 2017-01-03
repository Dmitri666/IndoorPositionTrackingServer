"use strict";
app.directive('backImg', [function () {
    return function (scope, elem, attrs) {
        attrs.$observe('backImg', function (val) {            
            elem.css({
                'background-image': 'url(' + val + ')',
                'background-size': 'cover',
                'background-repeat': 'no-repeat',
                'background-position': 'center center'
            });
        });
    }
}]);