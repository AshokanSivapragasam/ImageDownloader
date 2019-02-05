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
    
  var _blobRootUrl_ = 'https://cognitiveassetstorage.blob.core.windows.net/mediacontentfiles/';
  var _queueRootUrl_ = 'https://cognitiveassetstorage.queue.core.windows.net/mediacontentreceipts/messages';
  var _storageSasToken_ = '?sv=2017-04-17&ss=bfqt&srt=sco&sp=rwdlacup&se=2017-12-31T20:24:06Z&st=2017-10-31T12:24:06Z&spr=https&sig=ScI%2Fi9szTpAK2wd0dDgskYFQSGSWhKttS8Wdu5rETIw%3D';
  var _cosmosDbUrl_ = 'https://contentassetmoderator.azurewebsites.net/api/CosmosDbRestApi?code=KqpxGRZ9A8Pp8dGIIp/NrGkjKHCdG8Idl1OYH0BWo6Ld8eUpjGrzfw==';
  $scope.uploadFiles = function(){
    angular.forEach($scope.imageFiles, function(imageFileObject, index){
        if(imageFileObject.uploadProgressPercentage > 0 || imageFileObject.cognitiveProgressPercentage > 0) {
            console.log("File: " + imageFileObject.name + " is processed already. Skipping..");
        }
        else {
            //..upload file to blob container..
            uploadFileToBlobContainer($scope, _blobRootUrl_, _storageSasToken_, imageFileObject, index, azureBlob);

            //..add message receipt to queue..
            var _imageUrl_ = _blobRootUrl_ + imageFileObject._file.name;
            addMessageReceiptToQueue($http, _queueRootUrl_, _storageSasToken_, _imageUrl_, imageFileObject.correlationid, imageFileObject.size);
        }
    });
  };
    
  $interval(function(){
      listDocumentsFromCosmosDb($scope, $http, _cosmosDbUrl_);
     
  }, 3000); 
    
  $scope.theme = 'blue';

  var isThemeRed = false;

  /*$interval(function () {
    $scope.theme = isThemeRed ? 'blue' : 'red';

    isThemeRed = !isThemeRed;
  }, 2000);*/

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

function uploadFileToBlobContainer(scope, _blobRootUrl_, _storageSasToken_, imageFileObject, imageFileObjectIndex, azureBlob){
    /* start uploading the file to azure blob */
    var azureBlobConfiguration = {
      baseUrl: _blobRootUrl_ + imageFileObject._file.name,
      sasToken: _storageSasToken_,
      file: imageFileObject._file,
      progress: function(percentComplete, responsedata, responsestatus, responseheaders, responseconfig){
        scope.imageFiles[imageFileObjectIndex].uploadProgressPercentage = percentComplete;
      }
    };

    azureBlob.upload(azureBlobConfiguration);
    /* end uploading the file to azure blob */
};

function addMessageReceiptToQueue(http, _queueRootUrl_, _storageSasToken_, imageUrl, _correlationid_, imageFileObjectSize){
    /* start adding the message to azure queue */
    var queueMessagePlainText = "{'imageurl': '"+ imageUrl +"', 'correlationid': '" + _correlationid_ + "', 'imagesize': '" + imageFileObjectSize + "'}";
    var queueMessageBase64 = btoa(queueMessagePlainText);

    var queueMessage = "<QueueMessage><MessageText>" + queueMessageBase64 + "</MessageText></QueueMessage>";

    http.post(_queueRootUrl_ +  _storageSasToken_, queueMessage,
        {headers: {'Content-Type': 'application/xml'}})
        .then(function (response) {
            //$scope.queueApiResponse = response.data;
        }, function (errorMessage) {
            alert(errorMessage.xhrStatus);
        });
    /* end adding the message to azure queue */
};

function listDocumentsFromCosmosDb(scope, http, _cosmosDbUrl_){
    
    http.get(_cosmosDbUrl_,
        {
            headers: {
                'Accept': 'application/json'
            }
        })
        .then(function (response) {
            var _documents_ = response.data;
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

function addDocumentToCosmosDb(){
    /* start adding the token to azure cosmosdb */
    /*var cosmosDbDocument = "{'id': '" + _correlationid_ + "', imageurl': '"+ azureBlobConfiguration.baseUrl +"', 'correlationid': '" + _correlationid_ + "', 'imagesize': '" + imageFileObject.size + "', 'imageProcessingStatus': false, 'contentanalysisresponse': null}";

    var _cosmosDbUrl_ = 'https://cognitiveassetscosmosdb.documents.azure.com/dbs/MediaContentAnalysis/colls/MediaProperties/docs';
    var utcDateRfc1123Format = (new Date()).toUTCString();
    var httpVerb = 'POST';
    var resourceType = 'docs';
    var resourceId = 'dbs/MediaContentAnalysis/colls/MediaProperties';
    var masterKey = 'Txxlu9mwdokCu3lmAwi3onYWpmVpB32i5D3VR25faEmvoBrWBPr8lXhkVnfZdOxeIyquDUSLCdevphDnLCtMGA';
    var masterToken = 'master';
    var tokenVersion = '1.0';

    var cosmosDbAuthToken = getAuthorizationTokenUsingMasterKey(httpVerb, resourceType, resourceId, utcDateRfc1123Format, masterKey, masterToken, tokenVersion, $sha);

    $http.post(_cosmosDbUrl_, cosmosDbDocument,
        {
            headers: {
                'x-ms-date': utcDateRfc1123Format,
                'authorization': cosmosDbAuthToken,
                'Cache-Control': 'no-cache',
                //'User-Agent': 'Microsoft.Azure.Documents.Client/1.6.0.0',
                'x-ms-version': '2015-12-16',
                'Accept': 'application/x-www-form-urlencoded',
                //'Host': 'cognitiveassetscosmosdb.documents.azure.com',
                //'Cookie': 'x-ms-session-token#0=602; x-ms-session-token=602'
                //'Content-Length': '344',
                //'Expect': '100-continue'
            }
        })
        .then(function (response) {
            $scope.cosmosDbResponse = response.data;
        }, function (errorMessage) {
            alert(errorMessage.xhrStatus);
        });*/
    /* end adding the token to azure cosmosdb */
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

function getAuthorizationTokenUsingMasterKey(httpVerb, resourceType, resourceId, utcDateRfc1123Format, masterKey, masterToken, tokenVersion, $sha) { 
    var payload = (httpVerb || "").toLowerCase() + "\n" +   
               (resourceType || "").toLowerCase() + "\n" +   
               (resourceId || "") + "\n" +   
               utcDateRfc1123Format.toLowerCase() + "\n" +   
               "" + "\n";  

    // Initialize angular-sha
	$sha.setConfig({
       algorithm: 'SHA-256', // hashing algorithm to use
       inputType: 'TEXT', // Input type
       returnType: 'B64', // Return type
       secretType: 'B64' // Secret for HMAC
    });
    
    // Hash the value; 
    var myHash = $sha.hash(payload);
    
    // OR Using HMAC;
	var signature = $sha.hmac(payload, masterKey);

    return encodeURIComponent("type=" + masterToken + "&ver=" + tokenVersion + "&sig=" + signature);  
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