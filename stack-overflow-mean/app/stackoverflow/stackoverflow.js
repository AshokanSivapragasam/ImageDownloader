'use strict';

angular.module('myApp.stackoverflow', ['ngRoute'])

    .config(['$routeProvider', function($routeProvider) {
        $routeProvider.when('/stackoverflow/:problemId', {
            templateUrl: 'stackoverflow/stackoverflow.html',
            controller: 'StackOverflowCtrl'
        });
    }])

    .controller('StackOverflowCtrl', ['$scope', '$http', '$routeParams', 'commonService', function($scope, $http, $routeParams, commonService) {
        $scope.problems = {};
        $http.get(commonService.webServiceBaseUrl +  '/problems/' + $routeParams.problemId)
            .then(function (response) {
                $scope.problem = response.data;
            }, function (errorMessage) {
                alert('failed');
            });

        /*$scope.problem = {
            title: 'Recursion in Angular directives',
            votes: 51,
            description: 'The first one has the problem that you can\'t remove previously compiled code unless you comprehensibly manage the manual compile process. The second approach has the problem of... not being a directive and missing out on its powerful capabilities, but more urgently, it can\'t be parameterised the same way a directive can be; it\'s simply bound to a new controller instance.',
            tags: ['javascript', 'angularjs', 'recursion'],
            by: {
                displayName: 'Ashokan Sivapragasam',
                profilePictureUri: 'http://localhost/vault/images/icons/user.png'
            },
            when: '2016-01-01 12:34:56',
            hits: [{
                label: 'asked',
                value: '3 years ago'
            },{
                label: 'viewed',
                value: '1056 times'
            },{
                label: 'active',
                value: '23 days ago'
            }]
        };*/

        /*$scope.answers = [{
            votes: 5,
            description: 'Inspired by the solutions described in the thread mentioned by @dnc253, I abstracted the recursion functionality into a service. See this Plunker for a demo. I like this solution best because: You don\'t need an special directive which makes your html less clean. The recursion logic is abstracted away into the RecursionHelper service, so you keep your directives clean.',
            by: {
                displayName: 'Vinoth Sivapragasam',
                profilePictureUri: 'http://localhost/vault/images/icons/user.png'
            },
            when: '2016-01-01 12:34:56'
        }, {
            votes: 10,
            description: 'As of Angular 1.5.x, no more tricks are required, the following has been made possible. No more need for dirty work arounds! This discovery was a by product of my hunt for a better/cleaner solution for a recursive directive. You can find it here https://jsfiddle.net/cattails27/5j5au76c/. It supports as far is 1.3.x.',
            by: {
                userDisplayName: 'Prasanna Sivapragasam',
                userProfilePictureUri: 'http://localhost/vault/images/icons/user.png'
            },
            when: '2016-01-01 12:34:56'
        }, {
            votes: -2,
            description: 'As of Angular 1.x, no more tricks are required, the following has been made possible. No more need for dirty work arounds! This discovery was a by product of my hunt for a better/cleaner solution for a recursive directive. You can find it here https://jsfiddle.net/cattails27/5j5au76c/. It supports as far is 1.3.x.',
            by: {
                userDisplayName: 'Master X',
                userProfilePictureUri: 'http://localhost/vault/images/icons/user.png'
            },
            when: '2016-01-01 12:34:56'
        }];*/
    }]);