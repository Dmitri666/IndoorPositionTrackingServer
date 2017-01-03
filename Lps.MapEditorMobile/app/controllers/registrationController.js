'use strict';
app.controller('registrationController', function ($rootScope, $scope, $filter, $location, $routeParams, roomService, mapService, filesUploadService, roomModelService, FileUploader, ngAuthSettings) {

    $scope.isFirstRegistartion = $routeParams.id ? true : false;

    $scope.address = "";
    $scope.registration = {
        id: "",
        name: "",
        street: "",
        streetNumber: "",
        city: "",
        plz: "",
        email: "",
        phone: "",
        homepage: "",
        latitude: "",
        longitude: "",
        isVisibleBusinessHours: false,
        kitchenTypes: [],
        KitchenInternationalTypes: [],
        specializationTypes: [],
        businessHours: [],        
        photos: []
    };    
   
    $scope.activeTab = $routeParams.id ? 1 : 6;
    $scope.setTab = function (tab) {
        if ($scope.isFirstRegistartion) {
            $scope.activeTab = tab;
        }
    };

    $scope.next = function (tab) {                
        if ($scope.registration === undefined) {
            return;
        }
        $scope.address = $scope.registration.city + "," + $scope.registration.street + " " + $scope.registration.streetNumber;
        mapService.getLocation($scope.address).then(function (result) {
            if (result.success) {
                $scope.registration.latitude = result.location.lat();
                $scope.registration.longitude = result.location.lng();                

                if (!$routeParams.id) {
                    $scope.registration.kitchenMenus = $scope.items;
                    $scope.registration.businessHours = $scope.businessHours;
                }

                roomService.saveRoom($scope.registration).then(function (result) {                    
                    if (result.success) {
                        if (!$scope.isFirstRegistartion) {
                            $location.path('/registration/' + result.roomId);
                        } else {
                            $scope.activeTab = tab;
                        }
                    }
                }, function (error) {
                    alert("error:" + error);
                    $scope.message = error.error_description;
                });
            }
        });        
    };      

    $scope.back = function () {
        $location.path('/roomslist');
    };
   
    /////// Öffnungszeiten

    $scope.businessHours = [
    {
        day: 0,
        openTime: '09:00',
        closeTime: '22:00',
        close: false,
        pauseStart: '',
        pauseEnd: ''
    },
    {
        day: 1,
        openTime: '09:00',
        closeTime: '22:00',
        close: false,
        pauseStart: '',
        pauseEnd: ''
    },
    {
        day: 2,
        openTime: '09:00',
        closeTime: '22:00',
        close: false,       
        pauseStart: '',
        pauseEnd: ''
    },
    {
        day: 3,
        openTime: '09:00',
        closeTime: '22:00',
        close: false,      
        pauseStart: '',
        pauseEnd: ''
    },
    {
        day: 4,
        openTime: '09:00',
        closeTime: '22:00',
        close: false,      
        pauseStart: '',
        pauseEnd: ''
    },
    {
        day: 5,
        openTime: '09:00',
        closeTime: '22:00',
        close: true,      
        pauseStart: '',
        pauseEnd: ''
    },
    {
        day: 6,
        openTime: '09:00',
        closeTime: '22:00',
        close: true,       
        pauseStart: '',
        pauseEnd: ''
    }];    

    $scope.applyToAll = function (picker) {
        angular.forEach($scope.businessHours, function (item) {
            item.close = picker.close;
            item.openTime = picker.openTime;
            item.closeTime = picker.closeTime;
            item.pauseStart = picker.pauseStart;
            item.pauseEnd = picker.pauseEnd;
        });                
    };

    /////// Bilder für Location   

    function initFileUploader() {
        // Uploader Plugin Code
        var uploader = $scope.uploader = new FileUploader({
            url: ngAuthSettings.apiServiceBaseUri + 'api/files/upload'
        });

        // FILTERS
        uploader.filters.push({
            name: 'extensionFilter',
            fn: function (item, options) {
                var filename = item.name;
                var extension = filename.substring(filename.lastIndexOf('.') + 1).toLowerCase();
                if (extension == "jpg" || extension == "doc" || extension == "docx" || extension == "rtf")
                    return true;
                else {
                    alert('Invalid file format. Please select a file with pdf/doc/docs or rtf format  and try again.');
                    return false;
                }
            }
        });
        uploader.filters.push({
            name: 'sizeFilter',
            fn: function (item, options) {
                var fileSize = item.size;
                fileSize = parseInt(fileSize) / (1024 * 1024);
                if (fileSize <= 5)
                    return true;
                else {
                    alert('Selected file exceeds the 5MB file size limit. Please choose a new file and try again.');
                    return false;
                }
            }
        });
        uploader.filters.push({
            name: 'itemResetFilter',
            fn: function (item, options) {
                if (this.queue.length < 5)
                    return true;
                else {
                    alert('You have exceeded the limit of uploading files.');
                    return false;
                }
            }
        });

        // CALLBACKS
        uploader.onWhenAddingFileFailed = function (item, filter, options) {
            console.info('onWhenAddingFileFailed', item, filter, options);
        };
        uploader.onAfterAddingFile = function (fileItem) {
            //alert('Files ready for upload.');
        };
        uploader.onSuccessItem = function (fileItem, response, status, headers) {
            var photoSetting = {
                Image: response,
                RoomId: $routeParams.id ? $routeParams.id: $scope.registration.id
            };
            filesUploadService.addPhotoToRoom(photoSetting).then(function (result) {
                if (result.success) {
                    $scope.registration.photos.push(result.photo);
                }                
            }, function (error) {                
                alert("error: " + error);
            });
        };
        uploader.onErrorItem = function (fileItem, response, status, headers) {
            alert('We were unable to upload your file. Please try again.');
        };
        uploader.onCancelItem = function (fileItem, response, status, headers) {
            //alert('File uploading has been cancelled.');
        };
        uploader.onAfterAddingAll = function (addedFileItems) {
            console.info('onAfterAddingAll', addedFileItems);
        };
        uploader.onBeforeUploadItem = function (item) {
            console.info('onBeforeUploadItem', item);
        };
        uploader.onProgressItem = function (fileItem, progress) {
            console.info('onProgressItem', fileItem, progress);
        };
        uploader.onProgressAll = function (progress) {
            console.info('onProgressAll', progress);
        };

        uploader.onCompleteItem = function (fileItem, response, status, headers) {
            console.info('onCompleteItem', fileItem, response, status, headers);
        };
        uploader.onCompleteAll = function () {
            console.info('onCompleteAll');
        };
    }


    /////// Menu
    
    $scope.filters = {
        name: "",
        type: ""
    };

    $scope.structure = {
        folders: [
           { name: 'Мясо', files: [{ name: 'Баранина' }, { name: 'Говядина ' }, { name: 'Конина' }, { name: 'Оленина' }, { name: 'Свинина' }, { name: 'Телятина' }, { name: 'Верблюжатина' }, { name: 'другое' }] },
           { name: 'Рыба', files: [{ name: 'Белая' }, { name: 'Красная' }, { name: 'Лосось' }] },
           { name: 'Овощи', files: [{ name: 'Капуста' }, { name: 'Лук зеленый' }, { name: 'Морковь' }, { name: 'Огурец' }, { name: 'Картофель' }, { name: 'Свёкла' }, { name: 'Петрушка' }, { name: 'Редис' }, { name: 'Чеснок' }, { name: 'Кукуруза' }, { name: 'Спаржа' }, { name: 'Ревень' }] },
           { name: 'Фрукты', files: [{ name: 'Яблоко' }, { name: 'Слива' }, { name: 'Груша' }, { name: 'Молина' }, { name: 'Клубника' }, { name: 'Орехи' }, { name: 'Апельсин' }, { name: 'Персик' }, { name: 'Черешня' }, { name: 'Другие' }] },
           { name: 'Зерновые', files: [{ name: 'Рис' }, { name: 'Рожь' }, { name: 'Пшеница' }, { name: 'Ячмень' }, { name: 'Гречиха' }, { name: 'Кукуруза' }, { name: 'Бобовые' }] },
           { name: 'Специи', files: [{ name: 'Пряности' }, { name: 'Соль' }, { name: 'Сахар' }, { name: 'Соус' }, { name: 'Горчица' }, { name: 'Хрен' }, { name: 'Лимонная кислота' }] },
           { name: 'Особенности', files: [{ name: 'Без лактоза' }, { name: 'Мало жира' }, { name: 'Без глютэна' }, { name: 'Без специй' }] },
           { name: 'Напитки', files: [{ name: 'какао' }, { name: 'чай' }, { name: 'кофе' }, { name: 'тоник' }, { name: 'молоко' }, { name: 'Квас' }, { name: 'Вино' }, { name: 'Пиво' }, { name: 'Водка' }, { name: 'Виски' }, { name: 'Коньяк' }, { name: 'Текила' }, { name: 'Граппа' }, { name: 'Шампанское' }] },
        ]
    };
    $scope.selectedFile = [];
    
    $scope.kitchenMenuTypeList = [];
    $scope.item = {
        description: "",
        kitchenMenuTypeName: ""
    };
    $scope.items = [{
        kitchenMenuTypeId: "",
        kitchenMenuTypeName: "",
        name: "Mezeteller",
        price: 56,
        description: "mit Würfelschafskäse, Peperoni, Oliven, Gurken, Tomaten, Honigmelone mit Rinderschinken dazu Brot"
    },
	{
	    kitchenMenuTypeId: "",
	    kitchenMenuTypeName: "",
		name: "Satalatar",
		price: 45,
		description: "Rucolasalat mit Parmesankäse und Tomaten angemacht mit Granatapfel-Viniagrette"
	},
	{
	    kitchenMenuTypeId: "",
	    kitchenMenuTypeName: "",
		name: "Lammfrikadelle",
		price: 18,
		description: "Hackfleischfrikadelle aus der Pfanne auf Wunsch mit Tomatensauce"
	}];

    $scope.$watchCollection("selectedFile", function (newVal, oldVal) {        
        var selectedText = "";
        angular.forEach($scope.selectedFile, function (value, key) {
            if (key < $scope.selectedFile.length - 1) {
                selectedText = selectedText + value.name + ", ";
            } else {
                selectedText = selectedText + value.name;
            }            
        });        
        if ($scope.selectedFile.length > 0) {            
            selectedText = "(Состав: " + selectedText + ")";
        }
        var matches = $scope.item.description.match(/\(.*?\)/g);
        if (matches) {
            $scope.item.description = $scope.item.description.replace(/\(.*?\)/g, selectedText);
        } else {
            $scope.item.description = $scope.item.description + selectedText;
        }
    });

    $scope.editing = false;

    $scope.removeItem = function (index) {
        $scope.items.splice(index, 1);
    };
    $scope.editItem = function (index) {
        $scope.editing = $scope.items.indexOf(index);
    };
    $scope.saveField = function (index) {
        if ($scope.editing !== false) {
            $scope.editing = false;
        }
    };
    $scope.addItem = function (item) {
        $scope.items.push(item);
        $scope.item = {
            description: ""
        };
        $scope.selectedFile.length = 0;

        $scope.next(5);
    };
    $scope.getKitchenMenuTypeName = function (item) {
        if ($scope.kitchenMenuTypeList.length === 0) {
            return 'Not found';
        }
        if (!item.kitchenMenuTypeId || 0 === item.kitchenMenuTypeId.length) {
            item.kitchenMenuTypeId = $scope.kitchenMenuTypeList[0].id;
            return $scope.kitchenMenuTypeList[0].name;
        }
        var found = $filter('filter')($scope.kitchenMenuTypeList, { id: item.kitchenMenuTypeId }, true);
        if (found.length) {
            return found[0].name;
        } else {
            return 'Not found';
        }
    };

    function init() {
        //roomService.getKitchenTypes().then(function (result) {
        //    $scope.kitchenTypesList = result;
        //}, function (error) {
        //    alert("error: " + JSON.stringify(error));
        //});
        //roomService.getKitchenInternationalTypes().then(function (result) {
        //    $scope.kitchenInternationalTypesList = result;
        //}, function (error) {
        //    alert("error: " + JSON.stringify(error));
        //});
        roomService.getKitchenMenuTypes().then(function (result) {
            $scope.kitchenMenuTypeList = result;            
        }, function (error) {
            alert("error: " + JSON.stringify(error));
        });
        roomService.getSpecializationTypes().then(function (result) {
            $scope.specializationTypeList = result;
        }, function (error) {
            alert("error: " +JSON.stringify(error));
        });        

        if ($routeParams.id) {
            roomService.getLocale($routeParams.id).then(function (result) {
                $scope.registration = result;

                //$scope.convertArrayToImageGalery();
                //if ($scope.registration.businessHours.length > 0) {
                $scope.businessHours = $scope.registration.businessHours;
                //}
                //if ($scope.registration.kitchenMenus.length > 0) {
                $scope.items = $scope.registration.kitchenMenus;
                //}

                $rootScope.loading = false;

            }, function (error) {
                alert("error: " + error);
            });
        }
    };

    init();
    initFileUploader();

    ////// Graphics Editor

    var editorModel = {
        jsonModel: { json: null, zoom: null },
        selectedElement: null,
        zoom: 1,
        toggleMeasure: false
    };

    $scope.editorModel = editorModel;

    var loadRoomModel = function () {
        var RoomId = $routeParams.id ? $routeParams.id : $scope.registration.id;
        if (RoomId !== undefined) {
            roomModelService.getJsonModel(RoomId).then(function (model) {
                if (model.json == null) {
                    editorModel.jsonModel = { json: "", zoom: 1 };
                } else {
                    editorModel.jsonModel = { json: model.json, zoom: model.zoom, realScaleFactor: model.realScaleFactor }
                };
            }, function (error) {
                console.log(error);
            });
        } else {
            editorModel.jsonModel = { json: "", zoom: 1 };
        }
    };

    $scope.saveRoom = function () {
        $scope.$broadcast('save', $routeParams.id);
    };

    $scope.setRoomShape = function (type) {
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
        $scope.$broadcast('delete', editorModel.selectedElement);
    }

    $scope.toggleMeasure = function () {
        editorModel.toggleMeasure = !editorModel.toggleMeasure;
    }

    $scope.undo2d = function () {
        $scope.$broadcast('undo');
    }

    $scope.redo2d = function () {
        $scope.$broadcast('redo');
    }

    $scope.zoomIn = function () {
        editorModel.zoom = editorModel.zoom * 1.1;
    }

    $scope.zoomOut = function () {
        editorModel.zoom = editorModel.zoom / 1.1;
    }

    $scope.centreView = function () {
        $scope.$broadcast('centreView');
    }

    $scope.dropItem = function (itemType, position) {
        $scope.$broadcast('dropItem', { itemType: itemType, position: position });
    }

    $scope.$on("$destroy", function handler() {

    });

    loadRoomModel();

});

