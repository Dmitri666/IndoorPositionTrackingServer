'use strict';
app.controller('roomEditorController', [
    '$scope', '$window', '$routeParams', '$location', 'roomModelService', 
    function($scope, $window, $routeParams, $location, roomModelService) {
        var editorModel = {
            jsonModel: { json:null, zoom:null },
            selectedElement: null,
            zoom: 1,
            toggleMeasure: false
        };

        $scope.editorModel = editorModel;

        var loadRoomModel = function() {
            if ($routeParams.id !== undefined) {
                roomModelService.getJsonModel($routeParams.id).then(function(model) {
                    
                    if (model.json == null) {
                        editorModel.jsonModel = { json: "", zoom: 1 };
                    } else {
                        editorModel.jsonModel = { json: model.json, zoom: model.zoom, realScaleFactor: model.realScaleFactor }
                    };
                }, function(error) {
                    console.log(error);
                });
            }
        };
        
        $scope.back = function () {
            $location.path('/businesshours/' + $routeParams.id);
        };

        $scope.saveRoom = function () {
            $scope.$broadcast('save', $routeParams.id);
            
        };

        $scope.next = function() {
            $location.path('/roomslist');
        };

        $scope.setRoomShape = function(type) {
            var cornerPoints = [];
            if (type == '0') {
                cornerPoints.push({ x: 0, y: 0 });
                cornerPoints.push({ x: 0, y: 8 });
                cornerPoints.push({ x: 8, y: 8 });
                cornerPoints.push({ x: 8, y: 0 });

            } else if (type == '1') {
                cornerPoints.push({ x: 0, y: 0 });
                cornerPoints.push({ x: 0, y: 5 });
                cornerPoints.push({ x: 3, y: 8 });
                cornerPoints.push({ x: 8, y: 8 });
                cornerPoints.push({ x: 8, y: 0 });
            } else if (type == '2') {
                cornerPoints.push({ x: 0, y: 0 });
                cornerPoints.push({ x: 0, y: 5 });
                cornerPoints.push({ x: 2, y: 5 });
                cornerPoints.push({ x: 2, y: 8 });
                cornerPoints.push({ x: 6, y: 8 });
                cornerPoints.push({ x: 6, y: 5 });
                cornerPoints.push({ x: 8, y: 5 });
                cornerPoints.push({ x: 8, y: 0 });
            } else if (type == '3') {
                cornerPoints.push({ x: 0, y: 0 });
                cornerPoints.push({ x: 0, y: 8 });
                cornerPoints.push({ x: 8, y: 8 });
                cornerPoints.push({ x: 8, y: 6 });
                cornerPoints.push({ x: 6, y: 6 });
                cornerPoints.push({ x: 6, y: 0 });
            } else {
                alert('not implemented ' + type);
                return;
            }

            editorModel.shape = cornerPoints;

        };
        
        
        $scope.deleteItem = function () {
            $scope.$broadcast('delete',editorModel.selectedElement);
        }

        $scope.toggleMeasure = function () {
            editorModel.toggleMeasure = !editorModel.toggleMeasure;
        }

        $scope.undo2d = function () {
            $scope.$broadcast('undo');
        }

        $scope.redo2d = function() {
            $scope.$broadcast('redo');
        }

        $scope.zoomIn = function () {
            editorModel.zoom = editorModel.zoom * 1.1;
        }

        $scope.zoomOut = function() {
            editorModel.zoom = editorModel.zoom / 1.1;
        }

        $scope.centreView = function() {
            $scope.$broadcast('centreView');
        }
        
        $scope.dropItem = function (itemType,position) {
            $scope.$broadcast('dropItem',{itemType:itemType,position:position});
        }

        $scope.$on("$destroy", function handler() {

        });

        

        loadRoomModel();
    }
]);