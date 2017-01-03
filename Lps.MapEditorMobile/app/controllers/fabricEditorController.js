(function () {
    'use strict';
    
    var _EditorController = function ($scope, itemFactoryService, ngAuthSettings) {
        var vm = this;

        var canvas;
        var bgColor1 = "#FFEEDD";
        var bgColor2 = "#FFFFFF";
        var bgLineColor = "#FFFFFF";
        var realScaleFactor;
        var zoom;
        var canvasWidth;
        var canvasHight;
        var editMode = true;
        var container;
        var htmlCanvas;
        var factor;
        
        var init = function () {

            initPrototype();

            //Get the canvas & context
            var ih = window.innerHeight;
            htmlCanvas = angular.element('canvas');
            var ctx = htmlCanvas[0].getContext('2d');

            factor = 1;

            // retina display?
            var isRetina = (window.devicePixelRatio > 1);

            // iOS? (-> no auto double)
            var isIOS = ((ctx.webkitBackingStorePixelRatio < 2) || (ctx.webkitBackingStorePixelRatio == undefined));

            if (isRetina && isIOS) {
                factor = 2;
            }
            ctx.scale(factor,factor);
            container = $('#editorContainer');
            //container.css('height', ih * 0.5);

            //Run function when browser  resize
            window.onresize = function (e) {
                vm.respondCanvas();
            };

            
            //Initial call
            vm.respondCanvas();
        };

        vm.respondCanvas = function() {
            htmlCanvas.attr('width', $(container).width()); //max width
            htmlCanvas.attr('height', $(container).height()); //max height
            htmlCanvas.css('width', $(container).width() * factor);
            htmlCanvas.css('height', $(container).height() * factor);
            canvasWidth = $(container).width();
            canvasHight = $(container).height();
            if (canvas != null) {
                canvasWidth = $(container).width();
                canvasHight = $(container).height();
                canvas.width = $(container).width();
                canvas.height = $(container).height();
                centredView();
                canvas.renderAll();
            }
        }

        init();

        canvas = new fabric.Canvas('canvas', {
            selection: false,
            backgroundColor: bgColor1
            
        });
        
        vm.setJsonModel = function(model) {
            if (model.json != null) {
                canvas.width = canvasWidth;
                canvas.height =canvasHight;
                if (model.json === "") {
                    initialiseCanvas();
                } else {
                    canvas.loadFromDatalessJSON(model.json,
                        function () {
                            zoom = model.zoom;
                            realScaleFactor = model.realScaleFactor;
                            itemFactoryService.setRealScaleFactor(realScaleFactor);
                            validateCanvasItems();
                            centredView();
                            //validateGrid();
                        },
                        function(e) {
                           
                        });
                }

            };
        };

        vm.setSvgModel = function (model) {
            editMode = false;
            zoom = 1;
            canvas.width = canvasWidth;
            canvas.height = canvasHight;
            canvas.loadFromDatalessJSON(model.tablesLayout,
                function () {
                    canvas.forEachObject(function (obj) {
                        if (obj.itemType.startsWith("Table")) {
                            obj.hasControls = false;
                            obj.lockMovementY = true;
                            obj.lockMovementX = true;
                            obj.lockRotation = true;
                            obj.lockScalingX = true;
                            obj.lockScalingY = true;
                        }
                    });

                    validateCanvasItems();
                    fabric.loadSVGFromString(model.svgLayout, function (objects, options) {
                        var obj = fabric.util.groupSVGElements(objects, options);
                        obj.hasControls = false;
                        obj.selectable = false;
                        obj.lockMovementY = true;
                        obj.lockMovementX = true;
                        obj.lockRotation = true;
                        obj.lockScalingX = true;
                        obj.lockScalingY = true;
                        obj.itemType = "svgLayout";
                        canvas.insertAt(obj, 0, false);
                        canvas.renderAll();

                        centredView();
                    });


                },
                function (error) {
                    console.log(error);
                });
        };

        vm.setRoomShape = function(points) {
            //centredView();
            var oldPoints = canvas.getObjectsByType('CornerPoint');
            for (var i = 0; i < oldPoints.length; i++) {
                var obj = oldPoints[i];
                for (var k = 0; k < obj.wands.length; k++) {
                    var wand = canvas.getObjectById(obj.wands[k]);
                    canvas.remove(wand);
                }
                canvas.remove(obj);
            }
            
            for (var i = 0; i < points.length; i++) {
                this.dropItem('CornerPoint', { x:points[i].x * realScaleFactor * zoom,y:points[i].y * realScaleFactor * zoom });
            }
            var newPoints = canvas.getObjectsByType('CornerPoint');
            var lastPoints = [];
            for (var i = 0; i < newPoints.length; i++) {
                if (newPoints[i].wands.length === 1) {
                    lastPoints.push(newPoints[i]);
                }
            }
            var wand = itemFactoryService.getItem('Wand');
            wand.scale(zoom);
            wand.points.push(lastPoints[0].itemId);
            wand.points.push(lastPoints[1].itemId);
            lastPoints[0].wands.push(wand.itemId);
            lastPoints[1].wands.push(wand.itemId);
            canvas.add(wand);
            applyWandCoords(wand);
            centredView();
        };

        var centredView = function () {
            if (editMode) {
                var itemsExtents = getItemsExtents();

                var center = { left: (itemsExtents.left + itemsExtents.rigth) / 2, top: (itemsExtents.top + itemsExtents.bottom) / 2 }
                var canvasCenter = canvas.getCenter();
                var moveX = canvasCenter.left - center.left;
                var moveY = canvasCenter.top - center.top;
                moveAll(moveX, moveY);
                itemsExtents = getItemsExtents();
                var width = itemsExtents.rigth - itemsExtents.left;
                var height = itemsExtents.bottom - itemsExtents.top;
                var scaleX = canvas.width / width;
                var scaleY = canvas.height / height;
                var zoomFactor;
                if (scaleX > scaleY) {
                    zoomFactor = scaleY;
                } else {
                    zoomFactor = scaleX;
                }
                setZoom(zoomFactor * zoom);
                return { moveX: moveX, moveY: moveY, zoomFactor: zoomFactor }
            } else {
                var svg = canvas.getObjectsByType("svgLayout")[0];
                var itemsExtents = { left: svg.left, top: svg.top, rigth: svg.left + svg.width * svg.scaleX, bottom: svg.top + svg.height * svg.scaleY }
                
                var center = { left: (itemsExtents.left + itemsExtents.rigth) / 2, top: (itemsExtents.top + itemsExtents.bottom) / 2 }
                var canvasCenter = canvas.getCenter();
                var moveX = canvasCenter.left - center.left;
                var moveY = canvasCenter.top - center.top;
                moveAll(moveX, moveY);
                itemsExtents = itemsExtents = { left: svg.left, top: svg.top, rigth: svg.left + svg.width * svg.scaleX, bottom: svg.top + svg.height * svg.scaleY }
                var width = itemsExtents.rigth - itemsExtents.left;
                var height = itemsExtents.bottom - itemsExtents.top;
                var scaleX = canvas.width / width;
                var scaleY = canvas.height / height;
                var zoomFactor;
                if (scaleX > scaleY) {
                    zoomFactor = scaleY;
                } else {
                    zoomFactor = scaleX;
                }
                setZoom(zoomFactor * zoom);
                return { moveX: moveX, moveY: moveY, zoomFactor: zoomFactor }
            }
        };

        var restoreSave = function (param) {
            setZoom(zoom / param.zoomFactor);
            moveAll(-param.moveX, -param.moveY);
        };

        vm.getJsonModel = function () {
            var param = centredView();
            var json = JSON.stringify(canvas);
            var model = { json: json, zoom: zoom, realScaleFactor: realScaleFactor, width: canvas.width, height: canvas.height };
            model.svgModel = getSvgModel();
            //restoreSave(param);
            
            return model;
        };

        var getSvgModel = function () {
            var tables = [];
            var beacons = [];
            canvas.forEachObject(function (obj) {
                if (obj.itemType != null && obj.itemType.startsWith("Table")) {
                    tables.push(obj);
                } else if (obj.itemType === 'Beacon') {
                    beacons.push(obj);
                }
            });
            for (var i = 0; i < tables.length; i++) {
                canvas.remove(tables[i]);
            }
            for (var i = 0; i < beacons.length; i++) {
                canvas.remove(beacons[i]);
            }
            
            var svgLayout = canvas.toSVG({
                viewBox: { x: 0, y: 0, width: canvas.width, height: canvas.height }
            });
            canvas.deactivateAll().renderAll();
            //window.open(canvas.toDataURL('png'));
            var png = canvas.toDataURL("image/jpeg", 1.0);

            for (var i = 0; i < tables.length; i++) {
                canvas.add(tables[i]);
            }

            for (var i = 0; i < beacons.length; i++) {
                canvas.add(beacons[i]);
            }
            

            var tablesModel = { objects: [] };
            for (var i = 0; i < tables.length; i++) {
                var item = tables[i];
                tablesModel.objects.push(item.toObject());
            }
            var tablesLayout = angular.toJson(tablesModel);

            var tableList = [];
            for (var i = 0; i < tables.length; i++) {
                var item = tables[i];
                var description = null;
                item._objects.map(function (obj) {
                    if (obj.type === 'text') {
                        description = obj.text;
                    }
                });
                var table = { itemId: item.itemId, itemType: item.itemType, top: item.top, left: item.left, description: description, angle: item.angle, width: item.width * item.scaleX, height: item.height * item.scaleY };
                tableList.push(table);
            }

            var beaconList =[];
            for (var i = 0; i < beacons.length; i++) {
                var item = beacons[i];
                var id3 = null;
                item._objects.map(function(obj) {
                    if (obj.type === 'text') {
                        id3 = obj.text;
                    }
                });
                var beacon = {itemId: item.itemId, top: item.top, left: item.left, id3: id3 };
                beaconList.push(beacon);
            }
            
            return { svgLayout: svgLayout, tablesLayout: tablesLayout, beacons: beaconList,tables: tableList, pngImage: png };
        };
        
        vm.zoomIn = function () {
            setZoom(zoom * 1.0/1.1);
        };

        vm.zoomOut = function () {
            setZoom(zoom * 1.1);
        };

        var setZoom = function (newZoom) {
            var factor = newZoom / zoom;
            var center = canvas.getCenter();

            if (!editMode) {
                var svgLayout = canvas.getObjectsByType('svgLayout')[0];
                if (svgLayout.width * zoom <= canvas.width && factor < 1) {
                    return;
                }
            }

            if (canvas.backgroundImage) {
                var bi = canvas.backgroundImage;
                //bi.width = bi.width * factor;
                //bi.height = bi.height * factor;
            }
            canvas.forEachObject(function (obj) {
                if (obj.itemType === 'gridH') {
                    var scaleX = obj.scaleX;
                    var scaleY = obj.scaleY;
                    var top = obj.top;

                    var tempScaleY = scaleY * factor;
                    var tempTop = center.top + (top - center.top ) * factor;

                    //obj.scaleX = tempScaleX;
                    obj.scaleY = tempScaleY;
                    obj.left = 0;
                    obj.width = canvas.width / scaleX;
                    obj.top = tempTop;
                    obj.setCoords();
                    if (obj.top < 0 || obj.top > canvas.height) {
                        canvas.remove(obj);
                    } 
                } else if (obj.itemType === 'gridV') {
                    var scaleX = obj.scaleX;
                    var scaleY = obj.scaleY;
                    var left = obj.left;
                    var tempScaleX = scaleX * factor;
                    var tempLeft = center.left + (left - center.left) * factor;

                    obj.scaleX = tempScaleX;
                    obj.left = tempLeft;
                    obj.height = canvas.height / scaleY;
                    obj.top = 0;
                    obj.setCoords();
                    if (obj.left < 0 || obj.left > canvas.width) {
                        canvas.remove(obj);
                    }
                } else if (obj.itemType === 'Wand') {
                    var scaleX = obj.scaleX;
                    var scaleY = obj.scaleY;
                    var left = obj.left;
                    var top = obj.top;
                    
                    
                    obj.scaleX = scaleX * factor;
                    obj.scaleY = scaleY * factor;
                    obj.left = center.left + (left - center.left) * factor;
                    obj.top = center.top + (top - center.top) * factor;
                    obj.setCoords();
                    
                } else {
                    var scaleX = obj.scaleX;
                    var scaleY = obj.scaleY;
                    var left = obj.left;
                    var top = obj.top;

                    var tempScaleX = scaleX * factor;
                    var tempScaleY = scaleY * factor;
                    var tempLeft = center.left + (left - center.left) * factor;
                    var tempTop = center.top + (top - center.top) * factor;

                    obj.scaleX = tempScaleX;
                    obj.scaleY = tempScaleY;
                    obj.left = tempLeft;
                    obj.top = tempTop;

                    obj.setCoords();
                }
            });

            zoom = newZoom;
            validateGrid();

            canvas.renderAll();
            canvas.calcOffset();
        };

        var getItemsExtents = function() {
            var itemsExtents = { left: null, top: null, rigth: null, bottom: null };

            canvas.forEachObject(function(obj) {
                if (obj.itemType != null && !obj.itemType.startsWith("grid") && obj.itemType !== 'Wand') {
                    if (itemsExtents.top == null || itemsExtents.top > obj.top) {
                        itemsExtents.top = obj.top;
                    }
                    if (itemsExtents.bottom == null || itemsExtents.bottom < obj.top + obj.height * obj.scaleY) {
                        itemsExtents.bottom = obj.top + obj.height * obj.scaleY;
                    }
                    if (itemsExtents.left == null || itemsExtents.left > obj.left) {
                        itemsExtents.left = obj.left;
                    }
                    if (itemsExtents.rigth == null || itemsExtents.rigth < obj.left + obj.width * obj.scaleX) {
                        itemsExtents.rigth = obj.left + obj.width * obj.scaleX;
                    }
                }
            });
            return itemsExtents;
        };

        var getGridExtents = function () {
            var gridExtents = { left: null, top: null, rigth: null, bottom: null };

            var items = canvas.getObjectsByType("gridH");
            for (var i = 0; i < items.length; i++) {
                var obj = items[i];
                if (gridExtents.top == null || gridExtents.top > obj.top) {
                    gridExtents.top = obj.top;
                }
                if (gridExtents.bottom == null || gridExtents.bottom < obj.top + obj.height * obj.scaleY) {
                    gridExtents.bottom = obj.top + obj.height * obj.scaleY;
                }
            };

            items = canvas.getObjectsByType("gridV");
            for (var i = 0; i < items.length; i++) {
                var obj = items[i];
                if (gridExtents.left == null || gridExtents.left > obj.left) {
                    gridExtents.left = obj.left;
                }
                if (gridExtents.rigth == null || gridExtents.rigth < obj.left + obj.width * obj.scaleX) {
                    gridExtents.rigth = obj.left + obj.width * obj.scaleX;
                }
            }
            return gridExtents;
        };

        var dropItem = function(obj) {
            obj.scale(zoom);
            
            if (obj.itemType === 'CornerPoint') {
                addCornerPoint(obj);
            } else {
                if (obj.itemType != null && obj.itemType.startsWith('Table')) {
                    var description = 1;
                    canvas.forEachObject(function (obj) {
                        if (obj.itemType.startsWith('Table')) {
                            description++;
                        }
                    });
                    for (var i = 0; i < obj._objects.length; i++) {
                        var subItem = obj._objects[i];
                        if (subItem.type === 'text') {
                            subItem.text = description.toString();
                        }
                    }

                };
                if (obj instanceof Array) {
                    for (var i = 0; i < obj.length; i++) {
                        canvas.add(obj[i]);
                    }
                } else {
                    canvas.add(obj);
                    if (obj.comment !== undefined) {
                        obj.comment.scale(zoom);
                        obj.comment.left = obj.getCenterPoint().x;
                        obj.comment.top = obj.getCenterPoint().y;
                        canvas.add(obj.comment);

                    }
                }
            }
        }

        vm.dropItem = function (objectType, position) {
            var obj = itemFactoryService.getItem(objectType);
            obj.top = position.y - obj.getCenterPoint().y;
            obj.left = position.x - obj.getCenterPoint().x;
            dropItem(obj);
        };

        vm.dropItemMobile = function (objectType, position) {
            var obj = itemFactoryService.getItem(objectType);
            obj.top = position.y - obj.getCenterPoint().y - canvas._offset.top;
            obj.left = position.x - obj.getCenterPoint().x - canvas._offset.left;
            dropItem(obj);
        };

        vm.deleteSelectedItem = function () {
            var obj = canvas.getActiveObject();
            if (obj != null) {
                var toDelete = [];
                if (obj.itemType === 'CornerPoint') {
                    for(var i = 0; i < obj.wands.length;i++) {
                        var wand = canvas.getObjectById(obj.wands[i]);
                        for (var j = 0; j < wand.points.length; j++) {
                            if (wand.points[j] !== obj.itemId) {
                                var point = canvas.getObjectById(wand.points[j]);
                                if (point !== null) {
                                    var tmp = point.wands;
                                    point.wands = [];
                                    for (var k = 0; k < tmp.length; k++) {
                                        if (tmp[k] !== wand.itemId) {
                                            point.wands.push(tmp[k]);
                                        }
                                    };
                                };
                            };
                        };
                        toDelete.push(wand);
                    };
                } else if (obj.commentId !== null) {
                    toDelete.push(canvas.getObjectById(obj.commentId));
                }
                canvas.remove(obj);
                for (var i = 0; i < toDelete.length; i++) {
                    canvas.remove(toDelete[i]);
                }
            }
        };

        var validateGrid = function () {
            if (!editMode) {
                return;
            };

            var gridExtents = getGridExtents();

            canvas.forEachObject(function (obj) {
                if (obj.itemType === "gridV") {
                    obj.height = canvas.height;
                    
                } else if (obj.itemType === "gridH") {
                    obj.width = canvas.width;
                    
                }
            });

            

            //add gridV
            while (gridExtents.left >= realScaleFactor * zoom) {
                var item = itemFactoryService.getItem("gridV");
                item.left = gridExtents.left - realScaleFactor * zoom;
                item.height = canvas.height;
                item.scaleX = zoom;
                canvas.insertAt(item, 0, false);
                gridExtents.left = item.left;
            }
            while (gridExtents.rigth < canvas.width - realScaleFactor * zoom) {
                var item = itemFactoryService.getItem("gridV");
                item.left = gridExtents.rigth + realScaleFactor * zoom;
                item.height = canvas.height;
                item.scaleX = zoom;
                canvas.insertAt(item, 0, false);
                gridExtents.rigth = item.left;
            }

            //add gridH
            while (gridExtents.top >= realScaleFactor * zoom) {
                var item = itemFactoryService.getItem("gridH");
                item.top = gridExtents.top - realScaleFactor * zoom;
                item.width = canvas.width;
                item.scaleY = zoom;
                canvas.insertAt(item, 0, false);
                gridExtents.top = item.top;
            }
            while (gridExtents.bottom < canvas.height - realScaleFactor * zoom) {
                var item = itemFactoryService.getItem("gridH");
                item.top = gridExtents.bottom + realScaleFactor * zoom;
                item.width = canvas.width;
                item.scaleY = zoom;
                canvas.insertAt(item, 0, false);
                gridExtents.bottom = item.top;
            }
        }

        var initialiseCanvas = function() {
            realScaleFactor = canvas.height / 10;
            itemFactoryService.setRealScaleFactor(realScaleFactor);
            zoom = 1;

            for (var i = 0; i < canvas.height / realScaleFactor ; i++) {
                var y = (i) * realScaleFactor;
                var obj = itemFactoryService.getItem("gridH");
                obj.top = y;
                obj.width = canvas.width;
                canvas.add(obj);
            };

            for (var i = 0; i < canvas.width / realScaleFactor; i++) {
                var x = (i) * realScaleFactor;
                var obj = itemFactoryService.getItem("gridV");
                    obj.left = x;
                    obj.height = canvas.height;
                    canvas.add(obj);
                
            };
            
        };

        var elementSelectedEditMode = function(elem) {
            var txtElem = null;
            if (elem.comment !== undefined) {
                txtElem = elem.comment;
            } else if (elem.type === 'group') {
                for (var i = 0; i < elem._objects.length; i++) {
                    if (elem._objects[i].type === 'text') {
                        txtElem = elem._objects[i];
                        break;
                    }     
                };
            };

            if (txtElem != null && $scope.editorModel.selectedElement !== txtElem) {
                $scope.editorModel.selectedElement = txtElem;

            } else {
                $scope.editorModel.selectedElement = elem;
            }

        };

        var setTableSelectedState = function(table)
        {
            table.direction = true;
            animateTable(table);
        }

        var animateTable = function(obj) {
            var options = {
                easing: fabric.util.ease.easeOutQuad,
                duration: 1000
            };
            
            obj.animate({
                opacity: obj.direction ? 1 : 0.7
                
            }, fabric.util.object.extend({
                onChange: canvas.renderAll.bind(canvas),
                onComplete: function () {
                    obj.direction = !obj.direction;
                    animateTable(obj);
                },
                abort: function () {
                    if (obj.customSelection === false) {
                        obj.opacity = 1;
                        return true;
                    }
                    return false;
                }
            }, options));
        };

        var elementSelectedViewMode = function (elem) {
            if (elem.itemType.startsWith('Table')) {
                // table ist gebucht
                if (elem.booked) {
                    canvas.discardActiveObject();
                    return;
                }

                if (elem.customSelection == undefined || !elem.customSelection) {
                    elem.customSelection = true;
                    
                    setTableSelectedState(elem);
                } else {
                    elem.customSelection = false;
                    for (var i = 0; i < elem._objects.length; i++) {
                        var subItem = elem._objects[i];
                        if (subItem.type !== 'text') {
                            subItem.fill = '#a6dced';
                        }
                    }
                }
            }

            var selectedTables = [];
            canvas.forEachObject(function (obj) {
                if (obj.itemType != null && obj.itemType.startsWith('Table')) {
                    if (obj.customSelection) {
                        var description = "";
                        for (var i = 0; i < obj._objects.length; i++) {
                            var subItem = obj._objects[i];
                            if (subItem.type === 'text') {
                                description = subItem.text;
                            }
                        }
                        selectedTables.push({ id: obj.itemId, description: description });
                    }
                }
            });
            $scope.editorModel.selectedTables = selectedTables;
            canvas.discardActiveObject();

        };

        var elementsDeselected = function () {
            if ($scope.editorModel.selectedElement != null) {
                $scope.editorModel.selectedElement = null;
                $scope.$parent.$applyAsync();
            }
        };

        vm.setBookingModel = function(model) {
            canvas.forEachObject(function(obj) {
                if (obj.itemType != null && obj.itemType.startsWith('Table')) {
                    obj.booked = null;
                    for (var i = 0; i < model.length; i++) {
                        if (obj.itemId === model[i].tableId) {
                            if (model[i].state !== 0) {
                                obj.booked = true;
                            }
                        }
                    }
                }
            });

            canvas.forEachObject(function(obj) {
                if (obj.itemType != null && obj.itemType.startsWith('Table')) {
                    var fill = obj.booked ? 'red' : '#a6dced';
                    for (var i = 0; i < obj._objects.length; i++) {
                        var subItem = obj._objects[i];
                        if (subItem.type !== 'text') {
                            subItem.fill = fill;
                        }
                    }
                }
            });

            canvas.renderAll();

        };

        function onIntersect(options) {
            if (!editMode) {
                return;
            };
            options.target.setCoords();
            canvas.forEachObject(function (obj) {
                if (obj === options.target) return;
                if (options.target.intersectsWithObject(obj)) {
                    if (obj.itemType === 'CornerPoint' && options.target.itemType === 'CornerPoint') {
                        if (options.target.wands.length === 1 && obj.wands.length === 1) {
                            var wand = canvas.getObjectById(obj.wands[0]);
                            if (wand.points[0] === obj.itemId) {
                                wand.points[0] = options.target.itemId;
                            } else if (wand.points[1] === obj.itemId) {
                                wand.points[1] = options.target.itemId;
                            }
                            options.target.wands.push(wand.itemId);
                            canvas.remove(obj);
                            applyWandCoords(wand);
                        };

                    } else if (obj.itemType.startsWith('Table') && options.target.itemType.startsWith('Table')) {

                        var source = options.target;
                        var target = obj;
                        if (source.magnit.length === 0 || target.magnit.length === 0) {
                            return;
                        }


                        var scp = getTableCenterPoint(source);
                        var tcp = getTableCenterPoint(target);

                        var sVector = new Victor(scp.x, scp.y);
                        var tVector = new Victor(tcp.x, tcp.y);

                        sVector.subtract(tVector);
                        
                        
                        var targetMagnit = null;
                        var tMagnits = getTableMagnits(target);
                        for (var i = 0; i < tMagnits.length; i++) {
                            var magnit = tMagnits[i];
                            
                            var tmpAngle = Math.abs(magnit.angleDeg() - sVector.angleDeg()) % 360;
                            if (tmpAngle < 45) {
                                targetMagnit = magnit;
                            }
                        };
                        if (targetMagnit === null) {
                            return;
                        };

                       
                        // find source magnit
                        var sMagnit = null;
                        var tmpDistance = null;
                        var sMagnits = getTableMagnits(source);
                        for (var i = 0; i < sMagnits.length; i++) {
                            var magnit = sMagnits[i];

                            var dist = new Victor(targetMagnit.x, targetMagnit.y).add(new Victor(magnit.x, magnit.y)).length();
                            if (sMagnit === null ) {
                                sMagnit = magnit;
                                tmpDistance = dist;
                            } else if (tmpDistance > dist) {
                                sMagnit = magnit;
                                tmpDistance = dist;
                            }
                        };

                        var diffAngle = ((targetMagnit.angleDeg() + 180) % 360  - sMagnit.angleDeg()) % 360;
                        
                        source.setAngle(source.angle + diffAngle);

                        targetMagnit.multiplyScalar(2);
                        var newScp = tVector.add(targetMagnit);
                        scp = getTableCenterPoint(source);
                        source.left += newScp.x - scp.x;
                        source.top += newScp.y - scp.y;


                    };
                }
                
            });
        }

        function getTableCenterPoint(tableGroup) {
            var table;
            for (var i = 0; i < tableGroup._objects.length; i++) {
                var obj = tableGroup._objects[i];
                if (obj.type === 'rect' && obj.width === obj.height) {
                    table = obj;
                    break;
                }

            }

            var gcp = tableGroup.getCenterPoint();
            var tcp = table.getCenterPoint();
            var vtcp = new Victor(tcp.x, tcp.y).rotateDeg(tableGroup.angle).multiplyScalar(tableGroup.scaleX);
            return new fabric.Point(gcp.x + vtcp.x, gcp.y + vtcp.y);

        };

        function getTableMagnits(tableGroup) {
            var magnits = [];
            for (var i = 0; i < tableGroup.magnit.length; i++) {
                var magnit = new Victor(tableGroup.magnit[i].x, tableGroup.magnit[i].y).rotateDeg(tableGroup.angle).multiplyScalar(tableGroup.scaleX);
                magnits.push(magnit);

            };
            return magnits;
        }


        function addCornerPoint(cornerPoint) {
            var points = canvas.getObjectsByType("CornerPoint");
            var freePoint = null;
            for (var i = 0; i < points.length; i++) {
                if (points[i].wands.length < 2) {
                    freePoint = points[i];
                }
            }
            if (freePoint != null) {
                
                var wand = itemFactoryService.getItem("Wand");
                wand.scale(zoom);
                wand.points.push(freePoint.itemId);
                wand.points.push(cornerPoint.itemId);

                freePoint.wands.push(wand.itemId);
                cornerPoint.wands.push(wand.itemId);
                canvas.insertAt(wand,getMaxGridIndex(),false);
                canvas.add(cornerPoint);
                applyWandCoords(wand, freePoint, cornerPoint);
                canvas.renderAll();
            } else {
                canvas.add(cornerPoint);
                canvas.renderAll();
            };
        };

        function applyWandCoords(wand, startPoint, endPoint) {
            if (startPoint == null || endPoint == null) {
                startPoint = canvas.getObjectById(wand.points[0]);
                endPoint = canvas.getObjectById(wand.points[1]);
            };

            wand.x1 = startPoint.getCenterPoint().x;
            wand.y1 = startPoint.getCenterPoint().y;
            wand.x2 = endPoint.getCenterPoint().x;
            wand.y2 = endPoint.getCenterPoint().y;

            wand.left = Math.min(startPoint.getCenterPoint().x,endPoint.getCenterPoint().x);
            wand.top = Math.min(startPoint.getCenterPoint().y, endPoint.getCenterPoint().y);
            wand.width = Math.abs(endPoint.getCenterPoint().x - startPoint.getCenterPoint().x) / wand.scaleX;
            wand.height = Math.abs(endPoint.getCenterPoint().y - startPoint.getCenterPoint().y) / wand.scaleY;
            
        };

        function validateCanvasItems() {
            canvas.forEachObject(function(obj) {
                if (obj.itemType != null && obj.itemType.startsWith("Table")) {
                    obj.lockScalingX = true;
                    obj.lockScalingY = true;
                    obj.setControlVisible('tr', false);
                    obj.setControlVisible('tl', false);
                    obj.setControlVisible('br', false);
                    obj.setControlVisible('bl', false);
                    obj.setControlVisible('ml', false);
                    obj.setControlVisible('mt', false);
                    obj.setControlVisible('mr', false);
                    obj.setControlVisible('mb', false);
                    for (var i = 0; i < obj._objects.length; i++) {
                        var groupItem = obj._objects[i];
                        if (groupItem.type !== 'text') {
                            groupItem.fill = "#a6dced";
                        } 
                    }
                };
                if (obj.itemType === 'Ellipse' || obj.itemType === 'Rect') {
                    obj.fill = "#a6dced";
                    if (obj.commentId !== null) {
                        obj.comment = canvas.getObjectById(obj.commentId);
                        
                    }
                }
            });
            
            canvas.renderAll();
        };

        function getMaxGridIndex() {
            var lines = canvas.getObjects('line');
            return lines.length;
        }
        
        // canvas events
        canvas.on('object:moving', function (e) {
            var activeObject = e.target;
            if (activeObject.itemType === 'CornerPoint') {
                onIntersect(e);

                for (var i = 0; i < activeObject.wands.length; i++) {
                    var wand = canvas.getObjectById(activeObject.wands[i]);
                    applyWandCoords(wand);
                }
                
            } else if (activeObject.itemType === 'svgLayout') {
                
                
            } else if (activeObject.itemType != null && activeObject.itemType.startsWith('Table')) {
                onIntersect(e);

            };
            if (activeObject.comment !== undefined) {
                activeObject.comment.top = activeObject.getCenterPoint().y;
                activeObject.comment.left = activeObject.getCenterPoint().x;
            }
        });

        canvas.on('object:rotating', function (e) {
            var activeObject = e.target;
            if (activeObject.type === 'group') {
                for (var i = 0; i < activeObject._objects.length; i++) {

                    var subItem = activeObject._objects[i];
                    if (subItem.type === 'text') {
                        subItem.angle = -activeObject.angle;
                        
                        
                    }
                }
            }
        });

        canvas.on('object:selected', function(e) {
            if (editMode) {
                elementSelectedEditMode(e.target);
            } else {
                elementSelectedViewMode(e.target);
            }
            $scope.$parent.$applyAsync();
        });

        function postScalingGroup(groupObject) {
            
            for (var i = 0; i < groupObject._objects.length; i++) {
                var scaleX = groupObject.scaleX;
                var scaleY = groupObject.scaleY;
                var subItem = groupObject._objects[i];
                if (subItem.type === 'text') {
                    var tmpAngle = subItem.angle;
                    subItem.angle = 0;
                    if (scaleX < 1)
                        subItem.scaleX = (1 + (1 - scaleX)) * zoom;
                    else
                        subItem.scaleX = (1 / (scaleX)) * zoom;
                    if (scaleY < 1)
                        subItem.scaleY = (1 + (subItem.scaleY - scaleY))*zoom;
                    else
                        subItem.scaleY = (1 / (scaleY)) * zoom;

                        
                    //subItem.angle = tmpAngle;

                }
            }
        }

        canvas.on({
            'object:scaling': function(e) {
                var activeObject = e.target;
                if (activeObject.comment !== undefined) {
                    activeObject.comment.top = activeObject.getCenterPoint().y;
                    activeObject.comment.left = activeObject.getCenterPoint().x;
                }
                if (activeObject.type === 'group') {
                    postScalingGroup(activeObject, e.target.scaleX, e.target.scaleY);
                }
                canvas.renderAll();
            }
    });
        
        
        if (fabric.isTouchSupported === false) {
            var mouseDownContext = null;
            canvas.on('mouse:down', function(e) {
                var mouseEvent = e.e;
                var obj = canvas.getActiveObject();
                if (obj == null || obj.itemType === 'svgLayout') {
                    mouseDownContext = {
                        canvasTx: canvas.viewportTransform[4],
                        canvasTy: canvas.viewportTransform[5],
                        mouseDownX: mouseEvent.screenX,
                        mouseDownY: mouseEvent.screenY
                    };
                    elementsDeselected();
                };

            });

            canvas.on('mouse:up', function (e) {
                mouseDownContext = null;
            });

            canvas.on('mouse:move', function (e) {
                if (mouseDownContext != null) {
                    var mouseEvent = e.e;
                    var deltaX = (mouseEvent.screenX - mouseDownContext.mouseDownX);
                    var deltaY = (mouseEvent.screenY - mouseDownContext.mouseDownY);

                    moveAll(deltaX, deltaY);
                    mouseDownContext.mouseDownX = mouseEvent.screenX;
                    mouseDownContext.mouseDownY = mouseEvent.screenY;
                };

            });

        } else {
            var touchStartContext = null;
            canvas.on({
                'touch:gesture': function (e) {
                    try {
                        var self = e.self;
                        var obj = canvas.getActiveObject();
                        if (obj != null) {
                            return;
                        };
                        if (self.fingers < 2) {
                            return;
                        }
                        var lPinchScale = self.scale;
                        var scaleDiff = (lPinchScale - 1) / 10 + 1;
                        setZoom(zoom * scaleDiff);
                        console.log(scaleDiff);
                        //var o = canvas.getObjectById('1');
                        //if (o != null) {
                        //    canvas.remove(o);
                        //}
                        //var test = new fabric.Text(scaleDiff.toString());
                        //test.left = 200;
                        //test.top = 200;
                        //test.itemId = '1';
                        //canvas.add(test);
                    } catch (e) {
                        alert(e);
                    }

                },
                'touch:drag': function (e) {
                    if (e.self.fingers !== 1) {
                        return;
                    }
                    var touchEvent = e.e;
                    if (touchEvent.type === undefined) {
                        var r = "";
                    }
                    if (touchEvent.type === 'touchstart') {
                        var obj = canvas.getActiveObject();
                        if (obj == null || obj.itemType === 'svgLayout') {
                            touchStartContext = {
                                touchStartX: e.self.x,
                                touchStartY: e.self.y
                            };
                            elementsDeselected();
                        };
                    } else if (touchEvent.type === 'touchmove' && touchStartContext != null) {
                        
                        var deltaX = (e.self.x - touchStartContext.touchStartX);
                        var deltaY = (e.self.y - touchStartContext.touchStartY);

                        moveAll(deltaX, deltaY);
                        touchStartContext.touchStartX = e.self.x;
                        touchStartContext.touchStartY = e.self.y;
                    } else if (e.self.state === 'up') {
                        touchStartContext = null;
                    };
                },
                'touch:orientation': function (e) {
                    console.log(e.e.type);
                    
                },
                'touch:shake': function (e) {
                    console.log(e.e.type);
                },
                'touch:longpress': function (e) {
                    
                    console.log(e.e.type);
                }
            });
        };

        var moveAll = function (deltaX, deltaY) {
            if (deltaX === 0 && deltaY === 0) {
                return;
            };
            if (!editMode) {
                var svgLayout = canvas.getObjectsByType('svgLayout')[0];
                if (svgLayout.left + deltaX > 0) {
                    if (deltaX > 0) {
                        deltaX = 0;
                    }
                }
                if (svgLayout.top + deltaY > 0) {
                    if (deltaY > 0) {
                        deltaY = 0;
                    }
                }
                if (svgLayout.left + svgLayout.width * svgLayout.scaleX + deltaX < canvas.width) {
                    if (deltaX < 0) {
                        deltaX = 0;
                    }
                }
                if (svgLayout.top + svgLayout.height * svgLayout.scaleY + deltaY < canvas.height) {
                    if (deltaY < 0) {
                        deltaY = 0;
                    }
                }
            }

            if (canvas.backgroundImage) {
                var bi = canvas.backgroundImage;
                bi.left += deltaX;
                bi.top += deltaY;
                //bi.width = bi.width * factor; bi.height = bi.height * factor;
            }
            canvas.forEachObject(function (obj) {
                if (obj.itemType === 'gridH') {
                    obj.left = 0;
                    obj.width = canvas.width;
                    obj.top += deltaY;
                    if (obj.top < 0 || obj.top > canvas.height) {
                        canvas.remove(obj);
                    }
                } else if (obj.itemType === 'gridV') {
                    obj.top = 0;
                    obj.height = canvas.height;
                    obj.left += deltaX;
                    if (obj.left < 0 || obj.left > canvas.width) {
                        canvas.remove(obj);
                    }

                } else {
                    obj.left += deltaX;
                    obj.top += deltaY;
                }
                obj.setCoords();

            });

            validateGrid();

            canvas.renderAll();
            canvas.calcOffset();
        };
        
        $(canvas.wrapperEl).on('mousewheel', function (e) {
            var target = canvas.findTarget(e);
            var delta = e.originalEvent.wheelDelta / 120;
            console.log(delta);
            if (delta > 0) {
                vm.zoomIn();
            } else if (delta < 0) {
                vm.zoomOut();
            }
            return false;
            
        });

        $scope.$on("$destroy", function handler() {
            window.onresize = null;
        });

    };


    var initPrototype = function() {
        //  prototype
        if (!String.prototype.startsWith) {
            String.prototype.startsWith = function(searchString, position) {
                position = position || 0;
                return this.indexOf(searchString, position) === position;
            };
        }

        fabric.Canvas.prototype.getObjectById = function(itemId) {
            var object = null,
                objects = this.getObjects();

            for (var i = 0, len = this.size(); i < len; i++) {
                if (objects[i].itemId && objects[i].itemId === itemId) {
                    object = objects[i];
                    break;
                }
            }

            return object;
        };

        fabric.Canvas.prototype.getObjectsByType = function(itemType) {
            var objectList = [],
                objects = this.getObjects();

            for (var i = 0, len = this.size(); i < len; i++) {
                if (objects[i].itemType && objects[i].itemType === itemType) {
                    objectList.push(objects[i]);
                }
            }

            return objectList;
        };

        fabric.Object.prototype.transparentCorners = true;

        fabric.Object.prototype.toObject = (function(toObject) {
            return function() {
                if (this.itemType === 'CornerPoint') {
                    return fabric.util.object.extend(toObject.call(this), {
                        itemId: this.itemId,
                        itemType: this.itemType,
                        hasControls: this.hasControls,
                        wands: this.wands

                    });
                } else if (this.itemType === 'Wand') {
                    return fabric.util.object.extend(toObject.call(this), {
                        itemId: this.itemId,
                        itemType: this.itemType,
                        hasControls: this.hasControls,
                        selectable: this.selectable,
                        points: this.points
                    });
                } else if ((this.itemType === 'gridV') || (this.itemType === 'gridH')) {
                    return fabric.util.object.extend(toObject.call(this), {
                        itemId: this.itemId,
                        itemType: this.itemType,
                        hasControls: this.hasControls,
                        selectable: this.selectable

                    });
                } else if (this.itemType != null && this.itemType.startsWith('Table')) {
                    return fabric.util.object.extend(toObject.call(this), {
                        itemId: this.itemId,
                        itemType: this.itemType,
                        magnit: this.magnit
                    });
                } else if (this.itemType === 'Rect' || this.itemType === 'Ellipse') {
                    return fabric.util.object.extend(toObject.call(this), {
                        itemId: this.itemId,
                        itemType: this.itemType,
                        commentId: this.commentId
                    });
                } else {
                    return fabric.util.object.extend(toObject.call(this), {
                        itemId: this.itemId,
                        itemType: this.itemType,
                        hasControls: this.hasControls
                    });
                };
            };
        })(fabric.Object.prototype.toObject);


    };

    app.controller("fabricEditorController", ['$scope', 'itemFactoryService', 'ngAuthSettings', _EditorController]);
   
})();


