'use strict';
app.controller('tryRoomController', ['$scope', '$window', '$routeParams', '$location',  
    function ($scope, $window, $routeParams, $location) {

        var key = "tryRoomController";
        var editorModel = {
            jsonModel: { json:null, zoom:null },
            selectedElement: null,
            zoom: 1,
            toggleMeasure: false
        };

        $scope.editorModel = editorModel;
        
        var loadRoomModel = function () {            
            editorModel.jsonModel = { json: "", zoom: 1 };            
        };
       
        //$scope.back = function () {
        //    $location.path('/businesshours/' + $routeParams.id);
        //};       

        //$scope.saveRoom = function () {            
        //    $scope.$broadcast('save', $routeParams.id, key);          
        //};

        $scope.goToLogin = function () {            
            $location.path('/login');
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
             //$scope.saveRoom();            
        });
       
        loadRoomModel();
    }
]);