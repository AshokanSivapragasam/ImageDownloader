'use strict';

angular.module('myApp.videoplayer', ['ngRoute'])

    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/videoplayer/:movieid', {
            templateUrl: 'videoplayer/videoplayer.html',
            controller: 'VideoPlayerController'
        });
    }])

    .controller('VideoPlayerController', ['$rootScope', '$scope', '$http', '$sce', '$routeParams', '$timeout', '$log', 'commonService', function ($rootScope, $scope, $http, $sce, $routeParams, $timeout, $log, commonService) {
        commonService.currentMovieId = $routeParams.movieid;
        $scope.API = null;
        $scope.onPlayerReady = function (API) {
            $scope.API = API;
        };

        $scope.setVideo = function (videoUrl, videoMimeType, videoPosterUrl) {
            $scope.API.stop();

            $scope.config = {
                preload: "none",
                autoHide: false,
                autoHideTime: 3000,
                autoPlay: false,
                sources: [
                    {src: $sce.trustAsResourceUrl(videoUrl), type: videoMimeType}
                ],
                tracks: [
                    {
                        src: "http://www.videogular.com/assets/subs/pale-blue-dot.vtt",
                        kind: "subtitles",
                        srclang: "en",
                        label: "English",
                        default: ""
                    }
                ],
                theme: "bower_components/videogular-themes-default/videogular.css",
                plugins: {
                    poster: videoPosterUrl
                }
            };
        };

        $http.get(commonService.webServiceBaseUrl +  '/resources/videomirrors/' + commonService.currentMovieId)
            .then(function (response) {
                $timeout(function () {
                    $scope.videoMirror = response.data;
                    commonService.currentVideoMirrorId = $scope.videoMirror.VideoMirrorId;
                    $log.log(commonService.currentVideoMirrorId);
                    $scope.setVideo($scope.videoMirror.VideoUrl, $scope.videoMirror.VideoMimeType, $scope.videoMirror.VideoPosterUrl);
                })
            }, function (errorMessage) {
                alert('failed');
            });
    }])

    .directive('loadOnScroll', [ '$window', function ($window) {
		 return {
			 link: function (scope, element, attrs) {
					 var offset = parseInt(attrs.threshold) || 0;
					 var e = element[0];

					 angular.element($window).bind('scroll', function () {
						if (scope.$eval((e.scrollTop + e.offsetHeight) >= (e.scrollHeight - offset))) {
							//alert('ah nice!' + (e.scrollTop + e.offsetHeight) + ' - ' + (e.scrollHeight - offset));
						} else {
							 //alert('aww!' + (e.scrollTop + e.offsetHeight) + ' - ' + (e.scrollHeight - offset));
						}
						scope.$apply();
						 
						 /*if (scope.$eval((e.scrollTop + e.offsetHeight) >= (e.scrollHeight - offset))) {
							scope.$apply();
						 }*/
					 });
				 }
		 };
     }]);
