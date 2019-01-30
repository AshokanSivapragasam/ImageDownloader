'use strict';

angular.module('myApp.comments', ['ngRoute'])

    .controller('CommentsController', ['$scope', '$http', '$sce', '$log', 'commonService', function ($scope, $http, $sce, $log, commonService) {
        $scope.isShowComments = false;
        $scope.comments = [];

        $scope.viewComments = function () {
            $scope.isShowComments = !$scope.isShowComments;

            if ($scope.isShowComments) {
                $http.get(commonService.webServiceBaseUrl +  '/resources/videocomments/' + commonService.currentVideoMirrorId)
                    .then(function (response) {
                        $scope.comments = response.data;
                        $log.log($scope.comments);
                    }, function (errorMessage) {
                        alert('failed');
                    });

                $scope.newComment = {
                    Id: 50,
                    By: "Ashokan Sivapragasam",
                    Email: 'ashokansivapragasam@ymail.com',
                    On: new Date(),
                    Words: "This is a new comment",
                    VideoMirrorRefId: commonService.currentVideoMirrorId
                };
            }
        };

        $scope.publishComment = function () {
            $scope.clonedComment = {};
            $scope.newComment.On = new Date();
            angular.copy($scope.newComment, $scope.clonedComment);

            $log.log(JSON.stringify($scope.newComment));
            $scope.newComment.Words = '';

            $http({
                url: commonService.webServiceBaseUrl +  '/resources/videocomments',
                contentType: 'application/json',
                method: 'POST',
                data: JSON.stringify($scope.clonedComment)
            })
                .success(function (response) {
                    $log.log('success');
                })
                .error(function (data) {
                    $log.log('failed');
                });

            $scope.comments.push($scope.clonedComment);
        };
    }])

    // Register the 'currentTime' directive factory method.
    // We inject $interval and dateFilter service since the factory method is DI.
    .directive('currentTime', ['$interval',
        function ($interval) {
            // return the directive link function. (compile function not needed)
            return {
                scope: {
                    newCommentOn: '&newCommentOn'
                },
                link: function (scope, element, attrs) {
                    var format,  // date format
                        stopTime; // so that we can cancel the time updates

                    // used to update the UI
                    function updateTime() {
                        element.text(new Date());
                        scope.newCommentOn = new Date();
                    }

                    // watch the expression, and update the UI on change.
                    scope.$watch(attrs.myCurrentTime, function (value) {
                        format = value;
                        updateTime();
                    });

                    stopTime = $interval(updateTime, 1000);

                    // listen on DOM destroy (removal) event, and cancel the next UI update
                    // to prevent updating time after the DOM element was removed.
                    element.on('$destroy', function () {
                        $interval.cancel(stopTime);
                    });
                }
            };
        }]);
