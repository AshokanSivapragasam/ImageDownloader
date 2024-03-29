'use strict';

// Declare app level module which depends on views, and components
angular.module('myApp', [
    'ngRoute',
    'ngMaterial',
    'ngMessages',

    'ngAnimate',
    'ui.bootstrap.datetimepicker',
    'myApp.view1',
    'myApp.bulksendrequest',
    'myApp.movies',
    'myApp.view2',
    'myApp.view3',
    'myApp.tamilnewmovies',
    'myApp.videoplayer',
    'myApp.relatedvideos',
    'myApp.fabsocialdiscussionspeeddial',
    'myApp.comments',
    'myApp.experiments',
    'myApp.hdaudiosongs',
    'myApp.videobackground',
    'myApp.uploadfile',
    'myApp.common',
    'myApp.version',
    "ngSanitize",
    "com.2fdevs.videogular",
    "com.2fdevs.videogular.plugins.controls",
    "com.2fdevs.videogular.plugins.overlayplay",
    "com.2fdevs.videogular.plugins.poster"
])
    
    .controller('AppCtrl', function ($scope, $timeout, $mdSidenav, $log, $mdDialog, $mdMedia, $location) {
        $scope.copyrightedyear = new Date().getFullYear();
        $scope.goTo = function(route, side){
            $location.path(route);
            $scope.close(side);
        };

        $scope.toggleLeft = buildDelayedToggler('left');
        $scope.toggleRight = buildToggler('right');
        $scope.isOpenRight = function(){
            return $mdSidenav('right').isOpen();
        };

        $scope.status = '  ';
        $scope.customFullscreen = $mdMedia('xs') || $mdMedia('sm');

        $scope.showAdvanced = function(ev) {
            var useFullScreen = ($mdMedia('sm') || $mdMedia('xs'))  && $scope.customFullscreen;
            $mdDialog.show({
                controller: DialogController,
                templateUrl: 'dialog1.tmpl.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose:true,
                fullscreen: useFullScreen
            })
                .then(function(answer) {
                    $scope.status = 'You said the information was "' + answer + '".';
                }, function() {
                    $scope.status = 'You cancelled the dialog.';
                });
            $scope.$watch(function() {
                return $mdMedia('xs') || $mdMedia('sm');
            }, function(wantsFullScreen) {
                $scope.customFullscreen = (wantsFullScreen === true);
            });
        };

        function DialogController($scope, $mdDialog) {
            $scope.hide = function() {
                $mdDialog.hide();
            };
            $scope.cancel = function() {
                $mdDialog.cancel();
            };
            $scope.answer = function(answer) {
                $mdDialog.hide(answer);
            };
        }

        /**
         * Supplies a function that will continue to operate until the
         * time is up.
         */
        function debounce(func, wait, context) {
            var timer;
            return function debounced() {
                var context = $scope,
                    args = Array.prototype.slice.call(arguments);
                $timeout.cancel(timer);
                timer = $timeout(function() {
                    timer = undefined;
                    func.apply(context, args);
                }, wait || 10);
            };
        }
        /**
         * Build handler to open/close a SideNav; when animation finishes
         * report completion in console
         */
        function buildDelayedToggler(navID) {
            return debounce(function() {
                $mdSidenav(navID)
                    .toggle()
                    .then(function () {
                        $log.debug("toggle " + navID + " is done");
                    });
            }, 200);
        }
        function buildToggler(navID) {
            return function() {
                $mdSidenav(navID)
                    .toggle()
                    .then(function () {
                        $log.debug("toggle " + navID + " is done");
                    });
            }
        }

        $scope.close = function (side) {
            $mdSidenav(side).close()
                .then(function () {
                    $log.debug("close "+ side +" is done");
                });
        };
    })
    .config(['$routeProvider', '$mdThemingProvider', '$mdIconProvider', function($routeProvider, $mdThemingProvider, $mdIconProvider) {
        $routeProvider.otherwise({redirectTo: '/view1'});
        $mdThemingProvider.theme('default')
            .primaryPalette('blue')
            .accentPalette('orange');

        $mdIconProvider
            .iconSet('social', 'img/icons/social-icons.svg', 24)
            .iconSet('mdi', 'img/icons/mdi.svg', 24)
            .defaultIconSet('img/icons/core-icons.svg', 24);
    }]);
