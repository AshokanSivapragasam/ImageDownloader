'use strict';

angular.module('myApp.uploadfile', ['ngRoute'])

    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/uploadfile', {
            templateUrl: 'uploadfile/uploadfile.html',
            controller: 'UploadFileCtrl'
        });
    }])

    .controller('UploadFileCtrl', ['$scope', '$http', '$sce', 'fileUploadServiceDeprecated', 'commonService', function ($scope, $http, $sce, fileUploadServiceDeprecated, commonService) {
        $scope.tutorial = {
            title: 'test',
            description: 'testing upload file module'
        };
        $scope.saveTutorial = function (data) {
            fileUploadServiceDeprecated.saveModelToDatabase(data, commonService.webServiceBaseUrl +  '/resources/filemanager/PostFileManager');
        }
    }])

    .factory('fileUploadServiceDeprecated', ['$q', '$http', function ($q, $http) {
        var getModelAsFormData = function (data) {
            var dataAsFormData = new FormData();
            angular.forEach(data, function (value, key) {
                dataAsFormData.append(key, value);
            });

            return dataAsFormData;
        };

        var fwdFileToDatabase = function (data, url) {
            // ADD LISTENERS.
            var objXhr = new XMLHttpRequest();
            objXhr.addEventListener("progress", updateProgress, false);
            objXhr.addEventListener("load", transferComplete, false);

            // SEND FILE DETAILS TO THE API.
            objXhr.open("POST", url);
            objXhr.send(getModelAsFormData(data));
        };

        // UPDATE PROGRESS BAR.
        function updateProgress(e) {
            /*if (e.lengthComputable) {
             document.getElementById('pro').setAttribute('value', e.loaded);
             document.getElementById('pro').setAttribute('max', e.total);
             }*/

            alert("In progress");
        };

        // CONFIRMATION.
        function transferComplete(e) {
            alert("Files uploaded successfully.");
        };

        var saveModelToDatabase = function (data, url) {
            var deferred = $q.defer();
            $http({
                url: url,
                method: 'POST',
                headers: {'Content-Type': undefined},
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

        return {
            saveModelToDatabase: saveModelToDatabase,
            fwdFileToDatabase: fwdFileToDatabase
        };
    }])

    .directive('fileModel', ['$parse', function ($parse) {
        return {
            restrict: 'A',
            link: function (scope, element, attributes) {
                var model = $parse(attributes.fileModel);
                var modelSetter = model.assign;

                element.bind('change', function () {
                    scope.$apply(function () {
                        modelSetter(scope, element[0].files[0]);
                    });
                });
            }
        };
    }]);