'use strict';

angular.module('myApp.vsoreleasetools', ['ngRoute', 'base64', 'ngMaterial'])

.config(['$routeProvider', function($routeProvider) {
  $routeProvider.when('/vsoreleasetools', {
    templateUrl: 'vsoreleasetools/vsoreleasetools.html',
    controller: 'VsoReleaseToolsCtrl'
  });
}])

.controller('VsoReleaseToolsCtrl', ['$scope', '$http', '$base64', 'commonService', function ($scope, $http, $base64, commonService) {
    
    $scope.selectedReleaseDefinitionName = {id : '--not selected yet--'};
    
    $scope.selectReleaseDefinition = {
        all : null,
        allMatched : null,
        isDisabled : false,
        noCache : true,
        selectedItem : null,
        searchText : 'ET_MIP'
    };
    
    $scope.getAllReleaseDefinitions = function(){
        $http.get(commonService.webServiceUrlAllVsoReleaseDefinitions, {headers: {'Authorization': 'Basic ' + $base64.encode(':eifmwfiuysk75kshcixbib6oce3w5huajghgdj6ulnwzesubqazq'), 'Accept': 'application/json;odata=verbose'}})
            .then(function (response) {
                $scope.selectReleaseDefinition.all = response.data.value;
                $scope.selectReleaseDefinition.all = $scope.selectReleaseDefinition.allMatched;
            }, function (errorMessage) {
                alert('failed');
            });
    };
    
    $scope.getAReleaseDefinition = function(){
        $http.get(commonService.webServiceUrlVsoReleaseDefinition, {headers: {'Authorization': 'Basic ' + $base64.encode(':eifmwfiuysk75kshcixbib6oce3w5huajghgdj6ulnwzesubqazq'), 'Accept': 'application/json;odata=verbose'}})
            .then(function (response) {
                $scope.releaseDefinition = response.data;
            }, function (errorMessage) {
                alert('failed');
            });
    };
    
    $scope.getAllReleasesByDefinitionId = function(){
        $http.get(commonService.webServiceUrlAllVsoReleasesByDefinitionId, {headers: {'Authorization': 'Basic ' + $base64.encode(':eifmwfiuysk75kshcixbib6oce3w5huajghgdj6ulnwzesubqazq'), 'Accept': 'application/json;odata=verbose'}})
            .then(function (response) {
                $scope.allReleasesByDefinitionId = response.data;
            }, function (errorMessage) {
                alert('failed');
            });
    };
    
    /**
     * Search for states... use $timeout to simulate
     * remote dataservice call.
     */
    function query(query) {
      var results = query ? $scope.selectReleaseDefinition.allMatched.filter(createFilterFor(query)) : $scope.selectReleaseDefinition.allMatched,
          deferred;
      if (true) {
        deferred = $q.defer();
        $timeout(function () { deferred.resolve( results ); }, Math.random() * 1000, false);
        return deferred.promise;
      } else {
        return results;
      }
    }
    
    /**
     * Create filter function for a query string
     */
    function createFilterFor(query) {
      var lowercaseQuery = angular.lowercase(query);

      return function filterFn(state) {
        return (state.value.indexOf(lowercaseQuery) === 0);
      };

    }
    
    $scope.getAllReleaseDefinitions();

}]);