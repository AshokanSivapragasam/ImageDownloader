angular.module('myApp.experiments', ['ngMaterial', 'ngRoute'])

.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/experiments', {
        templateUrl: 'experiments/experiments.html',
        controller: 'ExperimentsCtrl'
    });
}])
.directive('sticky', ['$mdSticky', function ($mdSticky) {
    return {
        restrict : 'A',
        link : function(scope, element) {
            $mdSticky(scope, element);
        }
    }
}])
.controller('ExperimentsCtrl', ['$scope', function ($scope) {
    var items = [];
    for(var i = 0; i < 200; i++) {
      items.push({name: i.toString()});
    }
    $scope.items = items;
}]);
