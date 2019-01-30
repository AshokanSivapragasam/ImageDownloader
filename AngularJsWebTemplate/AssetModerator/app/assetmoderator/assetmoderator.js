'use strict';

angular.module('myApp.assetmoderator', ['ngRoute', 'azureBlobUpload', 'ngSha'])

.config(['$routeProvider', function($routeProvider) {
  $routeProvider.when('/assetmoderator', {
    templateUrl: 'assetmoderator/assetmoderator.html',
    controller: 'AssetModeratorCtrl'
  });
}])

.controller('AssetModeratorCtrl', function($scope, $http, azureBlob, $sha, $interval, $mdDialog) {
  $scope.currentNavItem = 'page1';
  $scope.currentImageFiles = [];
  $scope.imageFiles = [];
    
  $scope.$watch('currentImageFiles', function(newImageFiles){
      angular.forEach(newImageFiles, function(newImageFile, index){
          $scope.imageFiles.push(newImageFile);
      });
  });
  
  var _serviceRootUrl_ = 'http://assetmoderatorwebapi.azurewebsites.net';
  var _blobServiceResourceUrl_ = '/api/AzureBlobItem';
  var _queueServiceResourceUrl_ = '/api/AzureQueueMessage';
  var _documentDbServiceResourceUrl_ = '/api/AzureDocumentDbItem';
  $scope.uploadFiles = function(){
    angular.forEach($scope.imageFiles, function(imageFileObject, index){
        if(imageFileObject.uploadProgressPercentage > 0 || imageFileObject.cognitiveProgressPercentage > 0) {
            console.log("File: " + imageFileObject.name + " is processed already. Skipping..");
        }
        else {
            //..upload file to blob container..
            uploadFileToBlobContainer($scope, $http, _serviceRootUrl_, _blobServiceResourceUrl_, _queueServiceResourceUrl_, imageFileObject, index);
        }
    });
  };
    
  $interval(function(){
      listDocumentsFromCosmosDb($scope, $http, _serviceRootUrl_, _documentDbServiceResourceUrl_);
  }, 3000); 
    
  $scope.theme = 'blue';

  var isThemeRed = false;

  $scope.showAdvanced = function(ev, currentImageFile) {
    $scope.currentImageFile = currentImageFile;
    $mdDialog.show({
      scope:$scope,
      preserveScope:true,
      controller: DialogController,
      templateUrl: 'assetmoderator/popuptemplate.html',
      parent: angular.element(document.body),
      targetEvent: ev,
      clickOutsideToClose:true
    })
    .then(function(answer) {
      $scope.status = 'You said the information was "' + answer + '".';
    }, function() {
      $scope.status = 'You cancelled the dialog.';
    });  
  }
  
})

.directive('customUploadFile', customUploadFile)

.directive('ngFileModel', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var model = $parse(attrs.ngFileModel);
            var isMultiple = attrs.multiple;
            var modelSetter = model.assign;
            element.bind('change', function () {
                var values = [];
                angular.forEach(element[0].files, function (item) {
                    var value = {
                       // File Name 
                        name: item.name,
                        //File Size 
                        size: item.size,
                        //Upload Progress Percentage
                        uploadProgressPercentage: 0,
                        //Cognitive Progress Percentage
                        cognitiveProgressPercentage: 0,
                        //CorrelationId
                        correlationid : generateGuid(),
                        //File URL to view 
                        url: URL.createObjectURL(item),
                        // File Input Value 
                        _file: item
                    };
                    values.push(value);
                });
                
                scope.$apply(function () {
                    if (isMultiple) {
                        modelSetter(scope, values);
                    } else {
                        modelSetter(scope, values[0]);
                    }
                });
            });
        }
    };
}]);

function customUploadFile() {
    var directive = {
        restrict : 'E',
        templateUrl : 'assetmoderator/customuploadfiletemplate.html',
        link : customUploadFileLink
    };
    return directive;
}

function customUploadFileLink(scope, element, attrs) {
    var hiddenFileInputSelector = $(element[0].querySelector('#hiddenFileInputSelector'));
    var customFileInputSelector = $(element[0].querySelector('#customFileInputSelector'));

    if (hiddenFileInputSelector.length && customFileInputSelector.length) {
        customFileInputSelector.click(function (e) {
            hiddenFileInputSelector.click();
        });
    }

    hiddenFileInputSelector.on('change', function (elem) {
        var files = elem.target.files;

        var _selectedImageFiles = '';
        var maxCharsThreshold = 400;
        angular.forEach(files, function (item) {
            _selectedImageFiles += item.name + ';';
        });

        scope.selectedImageFiles = _selectedImageFiles;
        scope.$apply();
    });
}

function uploadFileToBlobContainer(scope, http, _serviceRootUrl_, _blobServiceResourceUrl_, _queueServiceResourceUrl_, imageFileObject, imageFileObjectIndex, imageUrl){
    /* start uploading the file to azure blob */
    
    var fd = new FormData();
    fd.append('file', imageFileObject._file);
    
    http.post(_serviceRootUrl_ +  _blobServiceResourceUrl_, fd, 
        {transformRequest: angular.identity,
         headers: {'Content-Type': undefined}})
        .then(function (response) {
            var blobApiReponse = response.data;
        
            scope.imageFiles[imageFileObjectIndex].uploadProgressPercentage = 70;
            /*..start | callback for adding receipt to azure queue..*/
            var queueMessageJson = "{'imageurl': '"+ blobApiReponse.blobImageUrl +"', 'correlationid': '" + imageFileObject.correlationid + "', 'imagesize': '" + imageFileObject.size + "'}";
            var queueMessageJsonStringified = JSON.stringify(queueMessageJson);
            
            http.post(_serviceRootUrl_ +  _queueServiceResourceUrl_, queueMessageJsonStringified,
                {headers: {'Content-Type': 'application/json'}})
                .then(function (response) {
                    scope.imageFiles[imageFileObjectIndex].uploadProgressPercentage = 100;
                }, function (errorMessage) {
                    alert(errorMessage.xhrStatus);
                });
            /*..end | callback for adding receipt to azure queue..*/
            
        }, function (errorMessage) {
            alert(errorMessage.xhrStatus);
        });
    /* end uploading the file to azure blob */
};

function listDocumentsFromCosmosDb(scope, http, _serviceRootUrl_, _documentDbServiceResourceUrl_){
    
    http.get(_serviceRootUrl_ + _documentDbServiceResourceUrl_,
        {
            headers: {
                'Accept': 'application/json'
            }
        })
        .then(function (response) {
            var _documents_ = response.data.documents;
            angular.forEach(_documents_, function(_document_, docIndex){
                angular.forEach(scope.imageFiles, function(_imageFile_, imageIndex){
                    if(scope.imageFiles[imageIndex].correlationid == _document_.correlationid) {
                        scope.imageFiles[imageIndex].cognitiveProgressPercentage = 100;
                        scope.imageFiles[imageIndex].cognitiveDocument = _document_;
                    }
                });
            });
        }, function (errorMessage) {
            alert(errorMessage.xhrStatus);
        });
};

function generateGuid() {
    var d = new Date().getTime();
    if(window.performance && typeof window.performance.now === "function"){
        d += performance.now();; //use high-precision timer if available
    }
    var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
        var r = (d + Math.random()*16)%16 | 0;
        d = Math.floor(d/16);
        return (c=='x' ? r : (r&0x3|0x8)).toString(16);
    });
    return uuid;
};

function DialogController($scope, $mdDialog) {
    $scope.hide = function() {
      $mdDialog.hide();
    };

    $scope.cancel = function() {
      $mdDialog.cancel();
    };

    $scope.answer = function(answer) {
      $mdDialog.hide(answer);
    };
  }