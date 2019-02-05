'use strict';

angular.module('myApp.relatedvideos', ['ngRoute'])

    .config(['$routeProvider', function($routeProvider) {
        $routeProvider.when('/relatedvideos', {
            templateUrl: 'relatedvideos/relatedvideos.html',
            controller: 'RelatedVideosController'
        });
    }])

    .controller('RelatedVideosController', ['$scope', '$http', function($scope, $http) {
        $http.get('http://hydpcm347350d/TamilYogiWebApi/resources/movies')
            .then(function(response){
                $scope.tamilMovies = response.data;
            }, function(errorMessage){
                alert('failed');
            });
    }]);