
app.factory('itemFactoryService', ['$q', function ($q) {
    var itemFactoryService = {};

    var svgItems = [];

    var tableShadow;
    var tableFill = "#a6dced";
    var tableStroke = "#55ccdd";
    var realScaleFactor;

    var loadSVGItems = function() {
        var test = '<svg  fill="#A6DCED" width="60" height="70" viewBox="0 0 60 70"  xmlns="http://www.w3.org/2000/svg">' +
            '<rect x="6" y="0" width="48" height="6"  stroke="#55ccdd" stroke-width="0"/>' +
            '<rect x="0" y="10" width="60" height="60" stroke="#55ccdd" stroke-width="0"/>' +
            '</svg>';
        fabric.loadSVGFromString(test, function (objects, options) {
            var obj = fabric.util.groupSVGElements(objects, options);
            obj.setShadow({ color: 'rgba(0,0,0,0.3)', offsetX: 5, offsetY: 5, blur: 5, affectStroke: false});

            svgItems.push({ itemType: "Test", item: obj });
        });
    };

    itemFactoryService.setRealScaleFactor = function (value) {
        realScaleFactor = value;
        tableShadow = { color: 'rgba(0,0,0,0.3)', offsetX: realScaleFactor / 10, offsetY: realScaleFactor / 10, blur: 5, affectStroke: false };
    };

    itemFactoryService.getTableShadow = function () {
        return tableShadow;
    };

    itemFactoryService.getItem = function (itemType) {
        var obj = null;

        if (itemType === 'gridH') {
            obj = new fabric.Line([0, 0, 0, 0], {
                stroke: '#FFFFFF',
                strokeWidth: 1,
                selectable: false,
                hasControls: false
            });
        };

        if (itemType === 'gridV') {
            obj = new fabric.Line([0, 0, 0, 0], {
                stroke: '#FFFFFF',
                strokeWidth: 1,
                selectable: false,
                hasControls: false
            });
        };

        if (itemType === 'CornerPoint') {
            obj = new fabric.Circle({
                radius: realScaleFactor / 3,
                fill: '#000',
                top: 0,
                left: 0,
                hasControls: false,
                wands:[]

            });
        };

        if (itemType === 'Wand') {
            obj = new fabric.Line([0, 0, 0, 0], {
                stroke: '#000',
                strokeWidth: 3,
                selectable: false,
                hasControls: false,
                points:[]
            });
        };

        if (itemType === 'Table1') {
            var table = new fabric.Rect({
                left: 0,
                top: 0,
                fill: tableFill,
                stroke: tableStroke,
                width: realScaleFactor,
                height: realScaleFactor,
                shadow:tableShadow
            });
            var chear = new fabric.Rect({
                left: realScaleFactor / 10,
                top: -realScaleFactor / 6,
                fill: tableFill,
                stroke: tableStroke,
                width: realScaleFactor * 0.8,
                height: realScaleFactor / 10,
                shadow: tableShadow
            });
            var text = new fabric.Text('', {
                left: realScaleFactor / 2,
                top: realScaleFactor / 2,
                fontSize: 10,
                textAlign: 'center',
                originX: 'center',
                originY: 'center',
                shadow: 'rgba(0,0,0,0.3) 5px 5px 5px'
            });
            obj = new fabric.Group([chear, table, text], {
                left: 0,
                top: 0,
                lockScalingX:true,
                lockScalingY:true,
                magnit: [{ x: realScaleFactor / 2, y: 0 }, { x: 0, y: realScaleFactor / 2 }, { x: - realScaleFactor / 2, y: 0 }]
            });

            obj.setControlVisible('tr', false);
            obj.setControlVisible('tl', false);
            obj.setControlVisible('br', false);
            obj.setControlVisible('bl', false);
            obj.setControlVisible('ml', false);
            obj.setControlVisible('mt', false);
            obj.setControlVisible('mr', false);
            obj.setControlVisible('mb', false);
        };

        if (itemType === 'Table2') {
            var table = new fabric.Rect({
                left: 0,
                top: 0,
                fill: tableFill,
                stroke: tableStroke,
                width: realScaleFactor,
                height: realScaleFactor,
                shadow: tableShadow
            });
            var chear = new fabric.Rect({
                left: realScaleFactor / 10,
                top: -realScaleFactor / 6,
                fill: tableFill,
                stroke: tableStroke,
                width: realScaleFactor * 0.8,
                height: realScaleFactor / 10,
                shadow: tableShadow
            });
            var chear2 = new fabric.Rect({
                left: realScaleFactor*(1 + 4/60),
                top: realScaleFactor / 10,
                fill: tableFill,
                stroke: tableStroke,
                width: realScaleFactor / 10,
                height: realScaleFactor * 0.8,
                shadow: tableShadow
            });
            var text = new fabric.Text('', {
                left: realScaleFactor / 2,
                top: realScaleFactor / 2,
                fontSize: 10,
                textAlign: 'center',
                originX: 'center',
                originY: 'center',
                shadow: 'rgba(0,0,0,0.3) 5px 5px 5px'
            });
            obj = new fabric.Group([chear, chear2, table, text], {
                left: 0,
                top: 0,
                lockScalingX:true,
                lockScalingY:true,
                magnit: [{ x: 0, y: realScaleFactor / 2 }, { x: -realScaleFactor / 2, y: 0 }]
            });

            obj.setControlVisible('tr', false);
            obj.setControlVisible('tl', false);
            obj.setControlVisible('br', false);
            obj.setControlVisible('bl', false);
            obj.setControlVisible('ml', false);
            obj.setControlVisible('mt', false);
            obj.setControlVisible('mr', false);
            obj.setControlVisible('mb', false);

        };
        
        if (itemType === 'Table3') {
            var table = new fabric.Rect({
                left: 0,
                top: 0,
                fill: tableFill,
                stroke: tableStroke,
                width: realScaleFactor,
                height: realScaleFactor,
                shadow: tableShadow
            });
            var chear = new fabric.Rect({
                left: realScaleFactor / 10,
                top: -realScaleFactor / 6,
                fill: tableFill,
                stroke: tableStroke,
                width: realScaleFactor * 0.8,
                height: realScaleFactor / 10,
                shadow: tableShadow
            });
            var chear2 = new fabric.Rect({
                left: realScaleFactor * (1 + 4 / 60),
                top: realScaleFactor / 10,
                fill: tableFill,
                stroke: tableStroke,
                width: realScaleFactor / 10,
                height: realScaleFactor * 0.8,
                shadow: tableShadow
            });
            var chear3 = new fabric.Rect({
                left: realScaleFactor / 10,
                top: realScaleFactor * (1 + 4 / 60),
                fill: tableFill,
                stroke: tableStroke,
                width: realScaleFactor * 0.8,
                height: realScaleFactor / 10,
                shadow: tableShadow
            });
            var text = new fabric.Text('', {
                left: realScaleFactor / 2,
                top: realScaleFactor / 2,
                fontSize: 10,
                textAlign: 'center',
                originX: 'center',
                originY: 'center',
                shadow: 'rgba(0,0,0,0.3) 5px 5px 5px'
            });
            obj = new fabric.Group([chear, chear2, chear3, table, text], {
                left: 0,
                top: 0,
                lockScalingX: true,
                lockScalingY: true,
                magnit: [ { x: -realScaleFactor / 2, y: 0 }]
            });

            obj.setControlVisible('tr', false);
            obj.setControlVisible('tl', false);
            obj.setControlVisible('br', false);
            obj.setControlVisible('bl', false);
            obj.setControlVisible('ml', false);
            obj.setControlVisible('mt', false);
            obj.setControlVisible('mr', false);
            obj.setControlVisible('mb', false);

        };
        
        if (itemType === 'Table4') {
            var table = new fabric.Rect({
                left: 0,
                top: 0,
                fill: tableFill,
                stroke: tableStroke,
                width: realScaleFactor,
                height: realScaleFactor,
                shadow: tableShadow
            });
            var chear = new fabric.Rect({
                left: realScaleFactor / 10,
                top: -realScaleFactor / 6,
                fill: tableFill,
                stroke: tableStroke,
                width: realScaleFactor * 0.8,
                height: realScaleFactor / 10,
                shadow: tableShadow
            });
            var chear2 = new fabric.Rect({
                left: realScaleFactor * (1 + 4 / 60),
                top: realScaleFactor / 10,
                fill: tableFill,
                stroke: tableStroke,
                width: realScaleFactor / 10,
                height: realScaleFactor * 0.8,
                shadow: tableShadow
            });
            var chear3 = new fabric.Rect({
                left: realScaleFactor / 10,
                top: realScaleFactor * (1 + 4 / 60),
                fill: tableFill,
                stroke: tableStroke,
                width: realScaleFactor * 0.8,
                height: realScaleFactor / 10,
                shadow: tableShadow
            });
            var chear4 = new fabric.Rect({
                left: -realScaleFactor / 6,
                top: realScaleFactor / 10,
                fill: tableFill,
                stroke: tableStroke,
                width: realScaleFactor / 10,
                height: realScaleFactor * 0.8,
                shadow: tableShadow
            });
            var text = new fabric.Text('', {
                left: realScaleFactor / 2,
                top: realScaleFactor / 2,
                fontSize: 10,
                textAlign: 'center',
                originX: 'center',
                originY: 'center',
                shadow: 'rgba(0,0,0,0.3) 5px 5px 5px'
            });
            obj = new fabric.Group([chear, chear2, chear3, chear4, table, text], {
                left: 0,
                top: 0,
                lockScalingX: true,
                lockScalingY: true,
                magnit: []
            });

            obj.setControlVisible('tr', false);
            obj.setControlVisible('tl', false);
            obj.setControlVisible('br', false);
            obj.setControlVisible('bl', false);
            obj.setControlVisible('ml', false);
            obj.setControlVisible('mt', false);
            obj.setControlVisible('mr', false);
            obj.setControlVisible('mb', false);

        };

        if (itemType === 'Rect') {
            obj = new fabric.Rect({
                left: 0,
                top: 0,
                fill: '#A6DCED',
                width: realScaleFactor,
                height: realScaleFactor,
                hasBorders: false
            });
            
            var text = getComment();
            
            obj.comment = text;
            obj.commentId = text.itemId;
        };


        if (itemType === 'Ellipse') {
            obj = new fabric.Circle({
                radius: realScaleFactor / 2,
                fill: '#A6DCED',
                hasBorders: false
            });
            var text = getComment();
            obj.comment = text;
            obj.commentId = text.itemId;
        };

        if (itemType === 'Beacon') {
            var beacon = new fabric.Circle({
                radius: realScaleFactor / 6,
                fill: 'red',
                top: 0,
                left: 0
            });
            var text = new fabric.Text('', {
                left: realScaleFactor / 2,
                top: realScaleFactor / 2,
                fontSize: 10,
                textAlign: 'center',
                originX: 'center',
                originY: 'center'
            });
            obj = new fabric.Group([beacon, text], {
                left: 0,
                top: 0,
                hasControls: false
            });
        };

        if (itemType === 'InnenWand') {
            obj = new fabric.Rect({
                left: 0,
                top: 0,
                fill: '#000000',
                stroke: '#000000',
                width: 10,
                height: 100,
                lockScalingX: true,
                lockScalingY:false

            });
        };

        if (obj != null) {
            obj.itemId = guid();
            obj.itemType = itemType;
        };

        if (obj == null) {
            for (var i = 0; i < svgItems.length; i++) {
                if (svgItems[i].itemType === itemType) {
                    obj = svgItems[i].item;
                    obj.itemId = guid();
                    obj.itemType = itemType;
                }
            }
        }



        return obj;

    };

    var getComment = function() {
        var text = new fabric.Text('', {
            left: realScaleFactor / 2,
            top: realScaleFactor / 2,
            fontSize: 10,
            textAlign: 'center',
            originX: 'center',
            originY: 'center',
            centeredRotation: true,
            centeredScaling: true,
            hasControls: false,
            shadow: 'rgba(0,0,0,0.3) 5px 5px 5px',
            itemId: guid(),
            itemType: 'comment',
            selectable: false
        });
        return text;
    }

    loadSVGItems();

    var guid = function () {
        function s4() {
            return Math.floor((1 + Math.random()) * 0x10000)
              .toString(16)
              .substring(1);
        }
        return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
          s4() + '-' + s4() + s4() + s4();
    }
    
    return itemFactoryService;
}]);

