'use strict';

angular.module('myApp.view1', ['ngRoute'])

    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/view1', {
            templateUrl: 'view1/view1.html',
            controller: 'View1Ctrl'
        });
    }])

    .controller('View1Ctrl', ['$scope', '$http', '$sce', 'commonService', function ($scope, $http, $sce, commonService) {
        /*$scope.usersData = [];
        $http.get('https://api.github.com/users')
         .success(function(data){
           $scope.usersData = data;
         })
         .error(function(data){
           alert('failed');
         });*/

        $scope.media = {
            sources: [
                {
                    src: $sce.trustAsResourceUrl(commonService.vaultBaseUrl + "/videos/gametrailers/AssassinsCreedUnityReview.mp4"),
                    type: "video/mp4"
                },
                {
                    src: $sce.trustAsResourceUrl("http://static.videogular.com/assets/videos/videogular.mp4"),
                    type: "video/mp4"
                },
                {
                    src: $sce.trustAsResourceUrl("http://static.videogular.com/assets/videos/videogular.webm"),
                    type: "video/webm"
                },
                {
                    src: $sce.trustAsResourceUrl("http://static.videogular.com/assets/videos/videogular.ogg"),
                    type: "video/ogg"
                }
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
                poster: "http://www.videogular.com/assets/images/videogular.png"
            }
        };
    }]);
