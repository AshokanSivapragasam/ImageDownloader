'use strict';

angular.module('myApp.vsoreleasetool', ['ngRoute', 'base64'])

    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/vsoreleasetool', {
            templateUrl: 'vsoreleasetool/vsoreleasetool.html',
            controller: 'VsoReleaseToolController'
        });
    }])

    .controller('VsoReleaseToolController', ['$scope', '$http', '$base64', 'commonService', function ($scope, $http, $base64, commonService) {
        $http.get('https://microsoftit.vsrm.visualstudio.com/defaultcollection/OneITVSO/_apis/release/releases/185377?api-version=3.0-preview.1&definitionId=2239&$expand=environments', {headers: {'Authorization': 'Basic ' + $base64.encode(':eifmwfiuysk75kshcixbib6oce3w5huajghgdj6ulnwzesubqazq'), 'Accept': 'application/json;odata=verbose'}})
            .then(function (response) {
                $scope.release = response.data;
            }, function (errorMessage) {
                alert('failed');
            });
    }]);
