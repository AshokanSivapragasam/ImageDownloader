'use strict';

angular.module('myApp.grabthebeast', ['ngRoute'])

    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/grabthebeast', {
            templateUrl: 'grabthebeast/grabthebeast.html',
            controller: 'GrabTheBeastController'
        });
    }])

    .controller('GrabTheBeastController', ['$scope', '$http', 'commonService', function ($scope, $http, commonService) {
        $http.get('https://microsoftit.vsrm.visualstudio.com/DefaultCollection/OneITVSO/_apis/release/definitions/2239?$expand=environments&api-version=3.0-preview.1')
            .then(function (response) {
                alert(response.data);
                $scope.tamilMovies = response.data;
            }, function (errorMessage) {
                alert('failed');
            });
    }]);
