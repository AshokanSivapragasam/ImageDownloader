'use strict';

angular.module('myApp.uploadfile', ['ngRoute'])

    .config(['$routeProvider', function($routeProvider) {
        $routeProvider.when('/uploadfile', {
            templateUrl: 'uploadfile/uploadfile.html',
            controller: 'UploadFileCtrl'
        });
    }])

    .controller('UploadFileCtrl', ['$scope', '$http', '$sce', 'fileUploadService', function($scope, $http, $sce, fileUploadService) {
        $scope.tutorial = {
            title: 'test',
            description: 'testing upload file module'
        };
        $scope.saveTutorial = function (data) {
            fileUploadService.saveModelToDatabase(data, 'http://hydpcm347350d/TamilYogiWebApi/resources/filemanager/PostFileManager');
        }
    }])

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
            var fd = getModelAsFormData(file);
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
        }

        return {
            saveModelToDatabase: saveModelToDatabase,
            uploadFileToUrl: uploadFileToUrl
        };
    }])

    .directive('fileModel', ['$parse', function ($parse) {
        return{
            restrict: 'A',
            link: function(scope, element, attributes){
                var model = $parse(attributes.fileModel);
                var modelSetter = model.assign;

                element.bind('change', function(){
                    scope.$apply(function(){
                        modelSetter(scope, element[0].files[0]);
                    });
                });
            }
        };
    }]);