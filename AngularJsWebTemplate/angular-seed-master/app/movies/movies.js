'use strict';

angular.module('myApp.movies', ['ngRoute'])

    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/movies', {
            templateUrl: 'movies/movies.html',
            controller: 'MoviesCtrl'
        });
    }])
    
    .controller('MoviesCtrl', ['$scope', '$http', function ($scope, $http) {
        $http.get('http://hydpcm347350d/TamilYogiWebApi/resources/movie')
            .then(function (response) {
                $scope.movies = response.data;
            }, function(data){
                alert('failed');
            });
        }])
    /*.controller('Custom2Controller', ['$scope', 'ServiceHelloWorld', function($scope, ServiceHelloWorld){
        ServiceHelloWorld.degree = 'BE';
        $scope.greetings = [];
        ServiceHelloWorld.degree = 'BTech IT';
        $scope.greetings.push(ServiceHelloWorld.greet('Ashok'));
        
        ServiceHelloWorld.degree = 'BE Mech';
        $scope.greetings.push(ServiceHelloWorld.greet('Vinoth'));
        
        ServiceHelloWorld.degree = 'BE CSE';
        $scope.greetings.push(ServiceHelloWorld.greet('Prasanna'));
    }])*/
    ;