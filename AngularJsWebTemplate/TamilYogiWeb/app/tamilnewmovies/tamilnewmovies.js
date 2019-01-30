'use strict';

angular.module('myApp.tamilnewmovies', ['ngRoute'])

    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/tamilnewmovies', {
            templateUrl: 'tamilnewmovies/tamilnewmovies.html',
            controller: 'TamilNewMoviesController'
        });
    }])

    .controller('TamilNewMoviesController', ['$scope', '$http', 'commonService', function ($scope, $http, commonService) {
        $http.get(commonService.webServiceBaseUrl +  '/resources/movies')
            .then(function (response) {
                $scope.tamilMovies = response.data;
            }, function (errorMessage) {
                alert('failed');
            });
    }]);
