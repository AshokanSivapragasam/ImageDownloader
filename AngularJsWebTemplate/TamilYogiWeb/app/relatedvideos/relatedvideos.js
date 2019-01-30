'use strict';

angular.module('myApp.relatedvideos', ['ngRoute'])

    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/relatedvideos', {
            templateUrl: 'relatedvideos/relatedvideos.html',
            controller: 'RelatedVideosController'
        });
    }])

    .controller('RelatedVideosController', ['$scope', '$http', 'commonService', function ($scope, $http, commonService) {
        $http.get(commonService.webServiceBaseUrl +  '/resources/movies')
            .then(function (response) {
                $scope.tamilMovies = response.data;
            }, function (errorMessage) {
                alert('failed');
            });
    }]);