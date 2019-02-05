'use strict';

// Declare app level module which depends on views, and components
angular.module('myApp', [
    'ngRoute',
    'ngMaterial',
    'ngMessages',
    'ngAnimate',
    'base64',
    'ui.bootstrap.datetimepicker',
    'myApp.view1',
    'myApp.bulksendrequest',
    'myApp.view2',
    'myApp.view3',
    'myApp.tamilnewmovies',
    'myApp.videoplayer',
    'myApp.relatedvideos',
    'myApp.fabsocialdiscussionspeeddial',
    'myApp.comments',
    'myApp.experiments',
    'myApp.calendar',
    'myApp.hdaudiosongs',
    'myApp.videobackground',
    'myApp.grabthebeast',
    'myApp.vsoreleasetool',
    'myApp.devtips',
    'myApp.uploadfile',
    'myApp.common',
    'myApp.version',
    "ngSanitize",
    "com.2fdevs.videogular",
    "com.2fdevs.videogular.plugins.controls",
    "com.2fdevs.videogular.plugins.overlayplay",
    "com.2fdevs.videogular.plugins.poster"
])
    .filter('propercase', function()
    {
        return function(text)
        {
            return text.replace(/[^A-Za-z0-9]/g, ' ').replace(/^\w|[A-Z]|\b\w|\s+/g, function (match, index) {
                if (match === '-' || match === '.' ) {
                    return " "; // or if (/\s+/.test(match)) for white spaces
                }
                return match.toUpperCase();
            }).trim();
        }
    })
    .controller('AppCtrl', function ($scope, $timeout, $mdSidenav, $log, $mdDialog, $mdMedia, $location, commonService) {
        $scope.siteuser = commonService.siteuser;
        $scope.isidentifieduser = commonService.isidentifieduser;
        $scope.copyrightedyear = new Date().getFullYear();
        $scope.goTo = function (route, side) {
            $location.path(route);
            $scope.close(side);
        };

        $scope.toggleLeft = buildDelayedToggler('left');
        $scope.toggleRight = buildToggler('right');
        $scope.isOpenRight = function () {
            return $mdSidenav('right').isOpen();
        };

        $scope.status = '  ';
        $scope.customFullscreen = $mdMedia('xs') || $mdMedia('sm');

        $scope.showAdvanced = function (ev, actiontype) {
            var useFullScreen = ($mdMedia('sm') || $mdMedia('xs')) && $scope.customFullscreen;
            $mdDialog.show({
                controller: UserLoginOrRegisterController,
                templateUrl: 'user' + actiontype + '.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: true,
                fullscreen: useFullScreen
            })
                .then(function (username) {
                    commonService.siteuser = username;
                    commonService.isidentifieduser = true;

                    $scope.siteuser = commonService.siteuser;
                    $scope.isidentifieduser = commonService.isidentifieduser;
                }, function () {
                    commonService.siteuser = commonService.siteuser;
                });
            $scope.$watch(function () {
                return $mdMedia('xs') || $mdMedia('sm');
            }, function (wantsFullScreen) {
                $scope.customFullscreen = (wantsFullScreen === true);
            });
        };

        function UserLoginOrRegisterController($scope, $mdDialog, commonService) {
            $scope.hide = function () {
                $mdDialog.hide();
            };
            $scope.cancel = function () {
                $mdDialog.cancel();
            };
            $scope.guest = function () {
                $mdDialog.hide();
            };
            $scope.login = function (username, password, rememberme) {
                if(username != undefined &&  password != undefined && username != '' && password != '') {
                    $mdDialog.hide(username);
                }
            };
            $scope.register = function (username, password) {
                if(username != undefined &&  password != undefined && username != '' && password != '') {
                    $mdDialog.hide(username);
                }
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
                timer = $timeout(function () {
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
            return debounce(function () {
                $mdSidenav(navID)
                    .toggle()
                    .then(function () {
                        $log.debug("toggle " + navID + " is done");
                    });
            }, 200);
        }

        function buildToggler(navID) {
            return function () {
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
                    $log.debug("close " + side + " is done");
                });
        };
    })
    .config(['$routeProvider', '$mdThemingProvider', '$mdIconProvider', function ($routeProvider, $mdThemingProvider, $mdIconProvider) {
        $routeProvider.otherwise({redirectTo: '/view1'});
        $mdThemingProvider.theme('default')
            .primaryPalette('blue')
            .accentPalette('orange');

        $mdThemingProvider.theme('youtube')
            .primaryPalette('grey')
            .accentPalette('orange');

        $mdIconProvider
            .iconSet('social', 'img/icons/social-icons.svg', 24)
            .iconSet('mdi', 'img/icons/mdi.svg', 24)
            .defaultIconSet('img/icons/core-icons.svg', 24);
    }]);
