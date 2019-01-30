'use strict';

// Declare app level module which depends on views, and components
angular.module('myApp', [
  'ngRoute',
  'ngMaterial',
  'ngSha',
  'myApp.view1',
  'myApp.view2',
  'myApp.assetmoderator',
  'azureBlobUpload',
  'myApp.version'
]).
config(['$locationProvider', '$routeProvider', '$mdThemingProvider', '$mdIconProvider', function($locationProvider, $routeProvider, $mdThemingProvider, $mdIconProvider) {
  $locationProvider.hashPrefix('!');

  $routeProvider.otherwise({redirectTo: '/view1'});
    
  $mdThemingProvider.theme('red')
    .primaryPalette('red');

  $mdThemingProvider.theme('blue')
    .primaryPalette('blue');
    
  $mdIconProvider
    .iconSet('social', 'img/icons/social-icons.svg', 24)
    .iconSet('mdi', 'img/icons/mdi.svg', 24)
    .defaultIconSet('img/icons/core-icons.svg', 24);
}]);
