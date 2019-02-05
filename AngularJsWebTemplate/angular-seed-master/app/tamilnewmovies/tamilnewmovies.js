'use strict';

angular.module('myApp.tamilnewmovies', ['ngRoute'])

    .config(['$routeProvider', function($routeProvider) {
        $routeProvider.when('/tamilnewmovies', {
            templateUrl: 'tamilnewmovies/tamilnewmovies.html',
            controller: 'TamilNewMoviesController'
        });
    }])

    .controller('TamilNewMoviesController', ['$scope', '$http', function($scope, $http) {
        $http.get('http://hydpcm347350d/TamilYogiWebApi/resources/movies')
            .then(function(response){
                $scope.tamilMovies = response.data;
            }, function(errorMessage){
                alert('failed');
            });
    }]);