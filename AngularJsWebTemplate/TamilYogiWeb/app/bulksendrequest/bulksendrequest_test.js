'use strict';

describe('A suite for module, myApp.relatedvideos', function () {

    beforeEach(module('myApp.relatedvideos'));
    beforeEach(module('myApp.common'));
    beforeEach(module('ngMock'));

    describe('A suite for controller, RelatedVideosController', function () {

        // Then we create some variables we're going to use
        var $httpBackend, scope, commonService;

        beforeEach(inject(function (_$injector_, _$rootScope_) {
            // Set up the mock http service responses
            $httpBackend = _$injector_.get('$httpBackend');
            commonService = _$injector_.get('commonService');
            scope = _$rootScope_.$new();
        }));

        it('is a spec for checking the controller, RelatedVideosController is defined', inject(function ($controller) {
            //spec body
            var relatedVideosController = $controller('RelatedVideosController', {$scope: scope});
            debugger
            expect(relatedVideosController).toBeDefined();
        }));

        it('is a spec for checking the web service base url of service, common', inject(function ($controller) {
            //spec body
            var relatedVideosController = $controller('RelatedVideosController', {$scope: scope});
            expect(relatedVideosController).toBeDefined();
            expect(commonService.webServiceBaseUrl).toBeDefined();
            expect(commonService.webServiceBaseUrl.length).toBeGreaterThan(5);
            expect(commonService.webServiceBaseUrl).toEqual('http://localhost/TamilYogiWebApi');
        }));

        it('is a spec for checking the tamilMovies in scope of controller, RelatedVideosController', inject(function ($controller) {
            //spec body
            var relatedVideosController = $controller('RelatedVideosController', {$scope: scope});

            $httpBackend
              .when('GET', /^http:\/\/localhost\/TamilYogiWebApi\/resources\/movies/)
              .respond(200, [{"MovieId":1,"Title":"Pasanga-2","Caption":"","Tags":"","Genre":"Action, Thriller","Quality":"TcRip","AgeRestrictions":"12+","Certificate":"U","Language":"Tamil","Country":"India","ThumbnailUrl":"http://localhost/vault/images/thumbnails/Pasanga-2-2015-228x160.jpg","ReleasedDateTime":"2016-04-12T18:32:14.467"}
                            ,{"MovieId":2,"Title":"Thanga Magan","Caption":"","Tags":null,"Genre":"Romance, Action, Thriller","Quality":"DvdRip","AgeRestrictions":"18+","Certificate":"UA","Language":"Tamil","Country":"India","ThumbnailUrl":"http://localhost/vault/images/thumbnails/Thanga-Magan-2015-228x160.jpg","ReleasedDateTime":"2016-04-17T18:32:14.467"}]);

            $httpBackend.flush();

            expect(relatedVideosController).toBeDefined();
            expect(scope).toBeDefined();
            expect(scope.tamilMovies).toBeDefined();
            expect(scope.tamilMovies.length).toBeGreaterThan(0);
            expect(scope.tamilMovies[0].Title).toEqual('Pasanga-2');
        }));
    });
});
