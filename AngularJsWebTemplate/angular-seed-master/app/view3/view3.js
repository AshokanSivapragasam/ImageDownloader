'use strict';

angular.module('myApp.view3', ['ngRoute'])

.config(['$routeProvider', function($routeProvider) {
  $routeProvider.when('/view3', {
    templateUrl: 'view3/view3.html',
    controller: 'firstCtrl'
  });
}])

.controller("firstCtrl", function ($scope) {
    $scope.$on('eventName', function (event, args) {
        $scope.message = args.message;
        console.log($scope.message);
        
        $scope.$broadcast('eventStatus', { status: 'I recieved at ' + new Date() + '' });
    });
});



