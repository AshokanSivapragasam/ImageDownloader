'use strict';

angular.module('myApp.hdaudiosongs', ['ngRoute'])

    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/hdaudiosongs', {
            templateUrl: 'hdaudiosongs/hdaudiosongs.html',
            controller: 'HdAudioSongsController'
        });
    }])

    .controller('HdAudioSongsController', ['$rootScope', '$scope', '$http', '$sce', '$routeParams', '$timeout', '$log', 'commonService', function ($rootScope, $scope, $http, $sce, $routeParams, $timeout, $log, commonService) {
        $scope.hdaudiosongs = [
            {
                id: 'audioplayer1',
                title: 'Munbe vaa rocking theme',
                url: $sce.trustAsResourceUrl(commonService.vaultBaseUrl +  '/audio/Munbe_vaa_theme.mp3'),
                thumbnailUrl: $sce.trustAsResourceUrl(commonService.vaultBaseUrl +  '/images/thumbnails/Pasanga-2-2015-228x160.jpg'),
                audioType: 'audio/mp3',
                openPlayer: false
            },
            {
                id: 'audioplayer2',
                title: 'Munbe vaa rocking theme',
                url: $sce.trustAsResourceUrl(commonService.vaultBaseUrl +  '/audio/Munbe_vaa_theme.mp3'),
                thumbnailUrl: $sce.trustAsResourceUrl(commonService.vaultBaseUrl +  '/images/thumbnails/Bajirao-Mastani-2015-228x160.jpg'),
                audioType: 'audio/mp3',
                openPlayer: false
            },
            {
                id: 'audioplayer3',
                title: 'Munbe vaa rocking theme',
                url: $sce.trustAsResourceUrl(commonService.vaultBaseUrl +  '/audio/Munbe_vaa_theme.mp3'),
                thumbnailUrl: $sce.trustAsResourceUrl(commonService.vaultBaseUrl +  '/images/thumbnails/Thanga-Magan-2015-228x160.jpg'),
                audioType: 'audio/mp3',
                openPlayer: false
            },
            {
                id: 'audioplayer4',
                title: 'Munbe vaa rocking theme',
                url: $sce.trustAsResourceUrl(commonService.vaultBaseUrl +  '/audio/Munbe_vaa_theme.mp3'),
                thumbnailUrl: $sce.trustAsResourceUrl(commonService.vaultBaseUrl +  '/images/thumbnails/Pasanga-2-2015-228x160.jpg'),
                audioType: 'audio/mp3',
                openPlayer: false
            },
            {
                id: 'audioplayer5',
                title: 'Munbe vaa rocking theme',
                url: $sce.trustAsResourceUrl(commonService.vaultBaseUrl +  '/audio/Munbe_vaa_theme.mp3'),
                thumbnailUrl: $sce.trustAsResourceUrl(commonService.vaultBaseUrl +  '/images/thumbnails/Bajirao-Mastani-2015-228x160.jpg'),
                audioType: 'audio/mp3',
                openPlayer: false
            },
            {
                id: 'audioplayer6',
                title: 'Munbe vaa rocking theme',
                url: $sce.trustAsResourceUrl(commonService.vaultBaseUrl +  '/audio/Munbe_vaa_theme.mp3'),
                thumbnailUrl: $sce.trustAsResourceUrl(commonService.vaultBaseUrl +  '/images/thumbnails/Thanga-Magan-2015-228x160.jpg'),
                audioType: 'audio/mp3',
                openPlayer: false
            },
            {
                id: 'audioplayer7',
                title: 'Munbe vaa rocking theme',
                url: $sce.trustAsResourceUrl(commonService.vaultBaseUrl +  '/audio/Munbe_vaa_theme.mp3'),
                thumbnailUrl: $sce.trustAsResourceUrl(commonService.vaultBaseUrl +  '/images/thumbnails/Pasanga-2-2015-228x160.jpg'),
                audioType: 'audio/mp3',
                openPlayer: false
            },
            {
                id: 'audioplayer8',
                title: 'Munbe vaa rocking theme',
                url: $sce.trustAsResourceUrl(commonService.vaultBaseUrl +  '/audio/Munbe_vaa_theme.mp3'),
                thumbnailUrl: $sce.trustAsResourceUrl(commonService.vaultBaseUrl +  '/images/thumbnails/Bajirao-Mastani-2015-228x160.jpg'),
                audioType: 'audio/mp3',
                openPlayer: false
            },
            {
                id: 'audioplayer9',
                title: 'Munbe vaa rocking theme',
                url: $sce.trustAsResourceUrl(commonService.vaultBaseUrl +  '/audio/Munbe_vaa_theme.mp3'),
                thumbnailUrl: $sce.trustAsResourceUrl(commonService.vaultBaseUrl +  '/images/thumbnails/Thanga-Magan-2015-228x160.jpg'),
                audioType: 'audio/mp3',
                openPlayer: false
            }
        ];
    }])

    .directive('customAudioSkin', ['$parse', function ($parse) {
        return {
            restrict: 'E',
            scope: {
                audiothumbnailurl: '=',
                audiotitle: '=',
                audiourl: '=',
                audioplayerid: '=',
                audiotype: '=',
                audioplayedpercentage: '=',
                openaudioplayer: '=',
                canplay: '=',
                radialprogressbardecoratorclass: '=',
                customaudioskinoverlayclass: '='
            },
            templateUrl: "hdaudiosongs/custom-audio-player-template.html",
            controller: ['$scope', function ($scope) {
                $scope.playMusic = function (audioPlayerId) {
                    document.getElementById(audioPlayerId).play();
                }

                $scope.pauseMusic = function (audioPlayerId) {
                    document.getElementById(audioPlayerId).pause();
                }
                
                $scope.stopMusic = function (audioPlayerId) {
                    document.getElementById(audioPlayerId).pause();
                    document.getElementById(audioPlayerId).currentTime = 0;
                }
                
                $scope.toggleMusic = function (audioPlayerId) {
                    if($scope.canplay) {
                        document.getElementById(audioPlayerId).play();
                    }
                    else {
                        document.getElementById(audioPlayerId).pause();
                        document.getElementById(audioPlayerId).currentTime = 0;
                    }
                    $scope.canplay = !$scope.canplay;
                }
            }],
            link: function (scope, element, attributes) {
                var customaudioplayer = $(element[0].querySelector('.customaudioplayer'));
                
                customaudioplayer.on('timeupdate', function () {
                    var self = this;
                    scope.$apply(function () {
                        scope.audioplayedpercentage = Math.ceil((self.currentTime / self.duration)*100);
                        scope.radialprogressbardecoratorclass = "c100 p" + Math.ceil((self.currentTime / self.duration)*100) + " small dark" + (scope.audioplayedpercentage > 0 ? ' block' : ' none')
                        scope.customaudioskinoverlayclass = scope.audioplayedpercentage > 0 ? 'customaudioskinoverlay block' : 'customaudioskinoverlay none';
                     });
                });
            }
        };
    }]);
