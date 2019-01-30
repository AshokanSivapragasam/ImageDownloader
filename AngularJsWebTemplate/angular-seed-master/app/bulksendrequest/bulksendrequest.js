'use strict';

angular.module('myApp.bulksendrequest', ['ngRoute'])

.directive('apsUploadFile', apsUploadFile)

.directive('fileModel', ['$parse', function ($parse) {
        return{
            restrict: 'A',
            link: function(scope, element, attributes){
                var model = $parse(attributes.fileModel);
                var modelSetter = model.assign;

                alert(element[0].files[0].fileName);
                element.bind('change', function(){
                    scope.$apply(function(){
                        modelSetter(scope, element[0].files[0]);
                    });
                });
            }
        };
    }])

.config(['$routeProvider', function($routeProvider) {
    $routeProvider.when('/bulksendrequest', {
        templateUrl: 'bulksendrequest/bulksendrequest.html',
        controller: 'BulksendRequestCtrl'
    });
}])

.controller('BulksendRequestCtrl', ['$scope', '$http', 'fileUploadService', function($scope, $http, fileUploadService) {
        $scope.bulkSendModel = {
            BatchId: 'b480ee7d-2444-4f37-ba6d-db279b87b60a',
            BulksendId: 'b480ee7d-2444-4f37-ba6d-db279b87b60a',
            TenantAccountId: '10290011',
            EmailContentId: '317215',
            BulksendApproach: true,
            BulksendInputDataFile: '',
            EmailClassification: false,
            DataImportType: false,
            IsEmailSendInvoke: true,
            WantToScheduleEmailSendTime: true,
            EmailSendScheduleDatetime: '2015-01-01',
            DataExtensionTemplateName: 'TriggeredSendDataExtension'
        };
     
        $scope.sendBulkSendRequest = function(){
            fileUploadService.uploadFileToUrl($scope.bulksenddatafile, 'http://hydpcm347350d/TamilYogiWebApi/resources/filemanager/PostFileManager');
            fileUploadService.createBulksendRequest($scope.bulkSendModel, 'http://hydpcm347350d/TamilYogiWebApi/resources/eirequests/AddBulksendRequest');
        };
    }])

    .config(function($mdThemingProvider) {
        // Configure a dark theme with primary foreground yellow
        $mdThemingProvider.theme('docs-dark', 'default')
            .primaryPalette('yellow')
            .dark();
    })

    .factory('fileUploadService', ['$q', '$http', function ($q, $http){
        var getModelAsFormData = function (data) {
            var dataAsFormData = new FormData();
            angular.forEach(data, function(key, value){
                dataAsFormData.append(value, key);
                alert(value + ' | ' + key);
            });

            return dataAsFormData;
        };

        var saveModelToDatabase = function (data, url) {
            var deferred = $q.defer();
            $http({
                url: url,
                method: 'POST',
                headers: { 'Content-Type' : undefined },
                data: getModelAsFormData(data),
                transformRequest: angular.identity,
            }).success(function (result) {
                alert(result);
                deferred.resolve(result);
            }).error(function (result, status) {
                alert(status);
                deferred.reject(status);
            });
            return deferred.promise;
        };
        
        var uploadFileToUrl = function(file, uploadUrl){
            var fd = FormData();
            fd.append('file', file);
            $http.post(uploadUrl, fd, {
                transformRequest: angular.identity,
                headers: {'Content-Type': undefined}
            })
            .success(function (result) {
                alert(result);
            })
            .error(function (result, status) {
                alert(status);
            });
        };
        
        var createBulksendRequest = function(dataModel, createBulksendResourceUrl) {
            $http({
                    url: 'http://hydpcm347350d/TamilYogiWebApi/resources/eirequests/AddBulksendRequest',
                    method: 'POST',
                    data: JSON.stringify($scope.bulkSendModel),
                    contentType: 'application/json'
                }).success(function(data){
                    alert(data.EmailInterchangeId);
                }).error(function(data){
                    alert("failed");
                });  
        };

        return {
            saveModelToDatabase: saveModelToDatabase,
            uploadFileToUrl: uploadFileToUrl
        };
    }]);

function apsUploadFile() {
    var directive = {
        restrict: 'E',
        templateUrl: 'bulksendrequest/upload.file.template.html',
        link: apsUploadFileLink
    };
    return directive;
}

function apsUploadFileLink(scope, element, attrs) {
    var input = $(element[0].querySelector('#fileInput'));
    //var button = $(element[0].querySelector('#uploadButton'));
    var textInput = $(element[0].querySelector('#textInput'));

    if (input.length && textInput.length) {
        textInput.click(function(e) {
            input.click();
        });
    }

    input.on('change', function(e) {
        var files = e.target.files;
        if (files[0]) {
            scope.fileName = files[0].name;
        } else {
            scope.fileName = null;
        }
        scope.$apply();
    });
}