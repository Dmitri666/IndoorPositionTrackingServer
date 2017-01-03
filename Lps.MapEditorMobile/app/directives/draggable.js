"use strict";
app.directive('cnddraggable', ['ngAuthSettings', function (ngAuthSettings) {
    return function (scope, element) {
        var el = element[0];

        if ('ontouchstart' in window) {
            el.draggable = true;
            element.bind("touchstart", function (e) {
                e.preventDefault();
                return false;
            });
            element.bind('touchmove',function (e) {
                    e.preventDefault();
                    return false;
                }
            );

            var ontouchend = function (e) {
                e.preventDefault();
                var itemType = e.target.getAttribute('data-item-type');
                scope.dropItem(itemType, { x: e.originalEvent.changedTouches[0].clientX, y: e.originalEvent.changedTouches[0].clientY });
                return false;
            };

            element.bind('touchend', ontouchend);
        } else {
            el.draggable = true;

            el.addEventListener(
                'dragstart',
                function (e) {
                    e.dataTransfer.effectAllowed = 'move';
                    e.dataTransfer.setData('ItemType', e.target.getAttribute('data-item-type'));
                    return false;
                },
                false
            );
            el.addEventListener(
                'dragend',
                function (e) {
                    return false;
                },
                false
            );
        };




    }
}]);

