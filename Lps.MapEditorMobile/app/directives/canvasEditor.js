"use strict";
app.directive('canvasEditor', [
    'filesUploadService', 'roomModelService', function (filesUploadService, roomModelService) {


        return {
            restrict: 'E',
            scope: {
                editorModel: '=model',
                readOnlyMode: '=readonly'
            },
            controller: 'fabricEditorController',
            controllerAs: 'Editor',
            templateUrl: 'app/views/fabricCanvas.html',
            link: function(scope, element,attrs,controller) {
                // again we need the native object
                var el = element[0].children[0].children[0];

                if ('ontouchstart' in window) {
                    scope.$on("dropItem", function(event, data) {
                            scope.Editor.dropItemMobile(data.itemType, data.position);
                        }
                    );
                } else {
                    el.addEventListener(
                        'dragover',
                        function(e) {
                            e.dataTransfer.dropEffect = 'move';
                            // allows us to drop
                            if (e.preventDefault) e.preventDefault();
                            return false;
                        },
                        false
                    );
                    el.addEventListener(
                        'dragenter',
                        function(e) {
                            return false;
                        },
                        false
                    );
                    el.addEventListener(
                        'drop',
                        function(e) {
                            if (e.stopPropagation) {
                                e.stopPropagation();
                            }
                            var itemType = e.dataTransfer.getData('ItemType');
                            scope.Editor.dropItem(itemType, { x: e.layerX, y: e.layerY });
                            return false;
                        },
                        false
                    );
                };

                scope.$watch("editorModel.jsonModel",
                    function (model, e) {
                        if (model != null) {
                            scope.Editor.setJsonModel(model);
                        }
                    }
                );

                scope.$watch("editorModel.svgModel",
                    function (model, e) {
                        if (model != null) {
                            scope.Editor.setSvgModel(model);
                        }
                    }
                );

                scope.$watch("editorModel.shape",
                    function (shape, e) {
                        if (shape != null) {
                            scope.Editor.setRoomShape(shape);
                        }
                    }
                );

                scope.$watch("editorModel.zoom",
                    function (newZoom, altZoom) {
                        if (newZoom != null && altZoom != null && newZoom !== altZoom) {
                            if (newZoom > altZoom) {
                                scope.Editor.zoomOut();
                            } else {
                                scope.Editor.zoomIn();
                            }
                        }
                    }
                );

                
                scope.$watch("editorModel.toggleMeasure",
                    function(newValue, altValue) {
                        if (newValue != null && altValue != null && newValue != altValue) {
                            if (newValue) {
                                //scope.Editor.ep2d.toggleMeasure(1);
                            } else {
                                //scope.Editor.ep2d.toggleMeasure(0);
                            }
                        }
                    }
                );

                scope.$watch("editorModel.tableReservationModel",
                    function(reservationModel, altValue) {
                        if (reservationModel != null) {
                            scope.Editor.setBookingModel(reservationModel);
                        }
                    }
                );

                scope.$on("undo",
                    function() {
                        //scope.Editor.ep2d.undo();
                    }
                );

                scope.$on("redo",
                    function() {
                        //scope.Editor.ep2d.redo();
                    }
                );

                scope.$on("respondCanvas",
                    function() {
                        scope.Editor.respondCanvas();
                    }
                );

                scope.$on("delete",
                    function(event,element) {
                        scope.Editor.deleteSelectedItem();
                    }
                );

                scope.$on("save",
                    function (ctx, roomId) {
                        var model = scope.Editor.getJsonModel();                        
                        roomModelService.saveJsonModel({
                            id: roomId,
                            json: model.json,
                            zoom: model.zoom,
                            width: model.width,
                            height: model.height,
                            svgLayout: model.svgModel.svgLayout,
                            tablesLayout: model.svgModel.tablesLayout,
                            beacons: model.svgModel.beacons,
                            tables: model.svgModel.tables,
                            realScaleFactor: model.realScaleFactor,
                            backgroundImage: model.svgModel.pngImage
                        })
                        .then(function () { });
                        //roomModelService.savePngImage(roomId,model.svgModel.pngImage);
                    }
                );               
            }
        }
    }
]);