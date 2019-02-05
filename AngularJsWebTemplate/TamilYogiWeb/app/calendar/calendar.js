angular.module('myApp.calendar', ['ngRoute'])

    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/calendar', {
            templateUrl: 'calendar/calendar.html',
            controller: 'CalendarController'
        });
    }])


    .controller('CalendarController', function ($scope) {
        
    });

