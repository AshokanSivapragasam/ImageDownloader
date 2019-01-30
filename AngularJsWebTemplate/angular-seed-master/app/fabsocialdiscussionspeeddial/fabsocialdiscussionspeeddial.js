'use strict';

angular.module('myApp.fabsocialdiscussionspeeddial', ['ngRoute'])

    .config(['$routeProvider', function($routeProvider) {
        $routeProvider.when('/fabsocialdiscussionspeeddial', {
            templateUrl: '/fabsocialdiscussionspeeddial.html',
            controller: 'FabSocialDiscussionsSpeedDialController'
        });
    }])

    .controller('FabSocialDiscussionsSpeedDialController', ['$scope', '$http', function($scope, $http) {
        $scope.topDirections = ['left', 'up'];
        $scope.bottomDirections = ['down', 'right'];
        $scope.isOpen = true;
        $scope.availableModes = ['md-fling', 'md-scale'];
        $scope.selectedMode = 'md-scale';
        $scope.availableDirections = ['up', 'down', 'left', 'right'];
        $scope.selectedDirection = 'right';
    }]);