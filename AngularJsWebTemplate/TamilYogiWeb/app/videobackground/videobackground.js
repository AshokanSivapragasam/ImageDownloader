'use strict';

angular.module('myApp.videobackground', ['ngRoute'])

    .config(['$routeProvider', function($routeProvider) {
        $routeProvider.when('/videobackground', {
            templateUrl: 'videobackground/videobackground.html',
            controller: 'VideoBackgroundController'
        });
    }])

    .controller('VideoBackgroundController', ['$scope', function($scope){
        $scope.isAudioOn = false;
        
        $scope.toggleVideo = function (videoPlayerId) {
            $scope.isAudioOn = !$scope.isAudioOn;
            if($scope.isAudioOn) {
                document.getElementById(videoPlayerId).muted = false;
            }
            else {
                document.getElementById(videoPlayerId).muted = true;
            }
        }
        
        $scope.playVideo = function (videoPlayerId) {
            document.getElementById(videoPlayerId).play();
        }

        $scope.stopVideo = function (videoPlayerId) {
            document.getElementById(videoPlayerId).pause();
        }
    }]);
