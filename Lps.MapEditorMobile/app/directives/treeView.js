"use strict";
app.value('treeViewDefaults', {
    foldersProperty: 'folders',
    filesProperty: 'files',
    displayProperty: 'name',
    collapsible: true
});

app.directive('treeView', ['$q', 'treeViewDefaults', function ($q, treeViewDefaults) {
    return {
        restrict: 'A',
        scope: {
            treeView: '=treeView',
            treeViewOptions: '=treeViewOptions',
            selectedFile: '=',
        },
        replace: true,
        template:
            '<div class="tree">' +
                '<div tree-view-node="treeView">' +
                '</div>' +
            '</div>',
        controller: ['$scope', function ($scope) {
            var self = this;            

            var options = angular.extend({}, treeViewDefaults, $scope.treeViewOptions);

            self.toggleSelectedFile = function (specialization) {
                var idx = $scope.selectedFile.indexOf(specialization);
                if (idx > -1) {
                    $scope.selectedFile.splice(idx, 1);
                } else {
                    $scope.selectedFile.push(specialization);
                }
            };

            self.selectNode = function (node, breadcrumbs) {

                if (typeof options.onNodeSelect === "function") {
                    options.onNodeSelect(node, breadcrumbs);
                }
            };

            self.selectFile = function (file, breadcrumbs) {

                self.toggleSelectedFile(file);

                if (typeof options.onNodeSelect === "function") {
                    options.onNodeSelect(file, breadcrumbs);
                }
            };

            self.isSelected = function (node) {                
                var idx = $scope.selectedFile.indexOf(node);
                if (idx > -1) {
                    return true;
                } else {
                    return false;
                }
            };

            self.getOptions = function () {
                return options;
            };
        }]
    };
}]);

app.directive('treeViewNode', ['$q', '$compile', function ($q, $compile) {
    return {
        restrict: 'A',
        require: '^treeView',
        link: function (scope, element, attrs, controller) {

            var options = controller.getOptions(),
                foldersProperty = options.foldersProperty,
                filesProperty = options.filesProperty,
                displayProperty = options.displayProperty,
                collapsible = options.collapsible;

            scope.expanded = collapsible == false;
            
            scope.getFolderIconClass = function () {
                return scope.expanded && scope.hasChildren() ? 'fa fa-minus fa-lg' : 'fa fa-plus fa-lg';
            };

            scope.getFileIconClass = function (node) {
                var isSelected = controller.isSelected(node);
                return isSelected ? "fa fa-check-square-o fa-lg" : "fa fa-square-o fa-lg";
            };

            scope.hasChildren = function () {
                var node = scope.node;
                return Boolean(node && (node[foldersProperty] && node[foldersProperty].length) || (node[filesProperty] && node[filesProperty].length));
            };

            scope.selectNode = function (event) {
                event.preventDefault();

                if (collapsible) {
                    toggleExpanded();
                }

                var breadcrumbs = [];
                var nodeScope = scope;
                while (nodeScope.node) {
                    breadcrumbs.push(nodeScope.node[displayProperty]);
                    nodeScope = nodeScope.$parent;
                }
                controller.selectNode(scope.node, breadcrumbs.reverse());
            };

            scope.selectFile = function (file, event) {
                event.preventDefault();

                var breadcrumbs = [file[displayProperty]];
                var nodeScope = scope;
                while (nodeScope.node) {
                    breadcrumbs.push(nodeScope.node[displayProperty]);
                    nodeScope = nodeScope.$parent;
                }
                controller.selectFile(file, breadcrumbs.reverse());
            };

            scope.isSelected = function (node) {
                return controller.isSelected(node);
            };

            function toggleExpanded() {
                //if (!scope.hasChildren()) return;
                scope.expanded = !scope.expanded;
            }

            function render() {
                var template =
                    '<div class="tree-folder" ng-repeat="node in ' + attrs.treeViewNode + '.' + foldersProperty + '">' +
                        '<a href="#" class="tree-folder-header inline" ng-click="selectNode($event)" ng-class="{ selected: isSelected(node) }">' +
                            '<i ng-class="getFolderIconClass()"></i> ' +
                            '<span class="tree-folder-name">{{ node.' + displayProperty + ' }}</span> ' +
                        '</a>' +
                        '<div class="tree-folder-content"' + (collapsible ? ' ng-show="expanded"' : '') + '>' +
                            '<div tree-view-node="node">' +
                            '</div>' +
                        '</div>' +
                    '</div>' +
                    '<a href="#" class="tree-item" ng-repeat="file in ' + attrs.treeViewNode + '.' + filesProperty + '" ng-click="selectFile(file, $event)" ng-class="{ selected: isSelected(file) }">' +
                        '<span class="tree-item-name"><i ng-class="getFileIconClass(file)"></i> {{ file.' + displayProperty + ' }}</span>' +
                    '</a>';

                //Rendering template.
                element.html('').append($compile(template)(scope));
            }

            render();
        }
    };
}]);
