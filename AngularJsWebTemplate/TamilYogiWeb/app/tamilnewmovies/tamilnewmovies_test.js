'use strict';

<<<<<<< HEAD
describe('A suite for module, myApp.tamilnewmovies', function () {

    beforeEach(module('myApp.tamilnewmovies'));
    beforeEach(module('myApp.common'));
    beforeEach(module('ngMock'));

    describe('A suite for controller, TamilNewMoviesController', function () {

        // Then we create some variables we're going to use
        var $httpBackend, scope, commonService;

        beforeEach(inject(function (_$injector_, _$rootScope_) {
            // Set up the mock http service responses
            $httpBackend = _$injector_.get('$httpBackend');
            commonService = _$injector_.get('commonService');
            scope = _$rootScope_.$new();
        }));

        it('is a spec for checking the controller, TamilNewMoviesController is defined', inject(function ($controller) {
            //spec body
            var tamilNewMoviesController = $controller('TamilNewMoviesController', {$scope: scope});
            debugger
            expect(tamilNewMoviesController).toBeDefined();
        }));

        it('is a spec for checking the web service base url of service, common', inject(function ($controller) {
            //spec body
            var tamilNewMoviesController = $controller('TamilNewMoviesController', {$scope: scope});
            expect(tamilNewMoviesController).toBeDefined();
            expect(commonService.webServiceBaseUrl).toBeDefined();
            expect(commonService.webServiceBaseUrl.length).toBeGreaterThan(5);
            expect(commonService.webServiceBaseUrl).toEqual('http://localhost/TamilYogiWebApi');
        }));

        it('is a spec for checking the tamilMovies in scope of controller, TamilNewMoviesController', inject(function ($controller) {
            //spec body
            var tamilNewMoviesController = $controller('TamilNewMoviesController', {$scope: scope});

            $httpBackend
              .when('GET', /^http:\/\/localhost\/TamilYogiWebApi\/resources\/movies/)
              .respond(200, [{"MovieId":1,"Title":"Pasanga-2","Caption":"","Tags":"","Genre":"Action, Thriller","Quality":"TcRip","AgeRestrictions":"12+","Certificate":"U","Language":"Tamil","Country":"India","ThumbnailUrl":"http://localhost/vault/images/thumbnails/Pasanga-2-2015-228x160.jpg","ReleasedDateTime":"2016-04-12T18:32:14.467"}
                            ,{"MovieId":2,"Title":"Thanga Magan","Caption":"","Tags":null,"Genre":"Romance, Action, Thriller","Quality":"DvdRip","AgeRestrictions":"18+","Certificate":"UA","Language":"Tamil","Country":"India","ThumbnailUrl":"http://localhost/vault/images/thumbnails/Thanga-Magan-2015-228x160.jpg","ReleasedDateTime":"2016-04-17T18:32:14.467"}]);

            $httpBackend.flush();

            expect(tamilNewMoviesController).toBeDefined();
            expect(scope).toBeDefined();
            expect(scope.tamilMovies).toBeDefined();
            expect(scope.tamilMovies.length).toBeGreaterThan(0);
            expect(scope.tamilMovies[0].Title).toEqual('Pasanga-2');
        }));
    });
});
=======
describe('A global suite for tamilnewmovies in myApp', function () {

    beforeEach(module('myApp.tamilnewmovies'));
    beforeEach(module('myApp.common'));

    describe('A local suite for tamilnewmovies controller in myApp', function () {

        // Then we create some variables we're going to use
        var scope;

        beforeEach(inject(function ($controller, $rootScope) {
            scope = $rootScope.$new();
        }));

        it('Checks whether TamilNewMovies Controller is defined or not', inject(function ($controller) {
            //spec body
            var tamilNewMoviesController = $controller('TamilNewMoviesController', {$scope: scope});
            expect(tamilNewMoviesController).toBeDefined();
        }));

        it('Checks whether one or more tamil new movies are available', inject(function ($controller) {
            //spec body
            var tamilNewMoviesController = $controller('TamilNewMoviesController', {$scope: scope});
            expect(scope).toBeDefined();
            expect(scope.tamilMovies).toBeGreaterThan(0);
        }));
    });
});
>>>>>>> 38e1872c719cf735271a70ef973285d8989eead7
