'use strict';

describe('A suite for module, myApp.view1', function () {

    beforeEach(module('myApp.view1'));
    beforeEach(module('myApp.common'));
    beforeEach(module('ngMock'));

    describe('A suite for controller, View1Ctrl', function () {

        // Then we create some variables we're going to use
        var $httpBackend, scope;

        beforeEach(inject(function (_$injector_, _$rootScope_) {
            // Set up the mock http service responses
            $httpBackend = _$injector_.get('$httpBackend');
            scope = _$rootScope_.$new();
        }));

        it('is a spec for checking the controller, View1Ctrl is defined', inject(function ($controller) {
            //spec body
            var view1Ctrl = $controller('View1Ctrl', {$scope: scope});
            debugger
            expect(view1Ctrl).toBeDefined();
        }));

        it('is a spec for checking the media sources in scope of controller, View1Ctrl', inject(function ($controller) {
            //spec body
            var view1Ctrl = $controller('View1Ctrl', {$scope: scope});
            expect(view1Ctrl).toBeDefined();
            expect(scope).toBeDefined();
            expect(scope.media).toBeDefined();
            expect(scope.media.sources).toBeDefined();
            expect(scope.media.sources.length).toBeGreaterThan(0);
            expect(scope.media.sources[0]).toBeDefined();
            expect(scope.media.sources[0].src).toBeDefined();
            expect(scope.media.sources[0].type).toBeDefined();
            expect(scope.media.sources[0]).not.toBeNull();
            expect(scope.media.sources[0].src).not.toBeNull();
            expect(scope.media.sources[0].type).not.toBeNull();
        }));

        it('is a spec for checking the media tracks in scope of controller, View1Ctrl', inject(function ($controller) {
            //spec body
            var view1Ctrl = $controller('View1Ctrl', {$scope: scope});
            expect(view1Ctrl).toBeDefined();
            expect(scope).toBeDefined();
            expect(scope.media).toBeDefined();
            expect(scope.media.tracks).toBeDefined();
            expect(scope.media.tracks.length).toBeGreaterThan(0);
            expect(scope.media.tracks[0].src).toBeDefined();
            expect(scope.media.tracks[0].src).not.toBeNull();
            expect(scope.media.tracks[0].src.length).toBeGreaterThan(0);
            expect(scope.media.tracks[0].label).toEqual('English');
            expect(scope.media.tracks[0].kind).toEqual('subtitles');
            expect(scope.media.tracks[0].srclang).toEqual('en');
        }));

        it('is a spec for checking the media theme in scope of controller, View1Ctrl', inject(function ($controller) {
            //spec body
            var view1Ctrl = $controller('View1Ctrl', {$scope: scope});
            expect(view1Ctrl).toBeDefined();
            expect(scope).toBeDefined();
            expect(scope.media).toBeDefined();
            expect(scope.media.theme).toBeDefined();
            expect(scope.media.theme.length).toBeGreaterThan(0);
        }));

        it('is a spec for checking the media plugins in scope of controller, View1Ctrl', inject(function ($controller) {
            //spec body
            var view1Ctrl = $controller('View1Ctrl', {$scope: scope});
            expect(view1Ctrl).toBeDefined();
            expect(scope).toBeDefined();
            expect(scope.media).toBeDefined();
            expect(scope.media.plugins).toBeDefined();
            expect(scope.media.plugins.poster).toBeDefined();
            expect(scope.media.plugins.poster.length).toBeGreaterThan(0);
        }));

        xit('is a spec for checking the usersData in scope of controller, View1Ctrl', inject(function ($controller) {
            //spec body
            var view1Ctrl = $controller('View1Ctrl', {$scope: scope});

            $httpBackend
              .when('GET', /^https:\/\/api.github.com\/users/)
              .respond(200, [{
                          		"login" : "mojombo",
                          		"id" : 1,
                          		"avatar_url" : "https://avatars.githubusercontent.com/u/1?v=3",
                          		"gravatar_id" : "",
                          		"url" : "https://api.github.com/users/mojombo",
                          		"html_url" : "https://github.com/mojombo",
                          		"followers_url" : "https://api.github.com/users/mojombo/followers",
                          		"following_url" : "https://api.github.com/users/mojombo/following{/other_user}",
                          		"gists_url" : "https://api.github.com/users/mojombo/gists{/gist_id}",
                          		"starred_url" : "https://api.github.com/users/mojombo/starred{/owner}{/repo}",
                          		"subscriptions_url" : "https://api.github.com/users/mojombo/subscriptions",
                          		"organizations_url" : "https://api.github.com/users/mojombo/orgs",
                          		"repos_url" : "https://api.github.com/users/mojombo/repos",
                          		"events_url" : "https://api.github.com/users/mojombo/events{/privacy}",
                          		"received_events_url" : "https://api.github.com/users/mojombo/received_events",
                          		"type" : "User",
                          		"site_admin" : false
                          	}]);

            $httpBackend.flush();

            expect(view1Ctrl).toBeDefined();
            expect(scope).toBeDefined();
            expect(scope.usersData).toBeDefined();
            expect(scope.usersData.length).toBeGreaterThan(0);
            expect(scope.usersData[0].login).toEqual('mojombo');
        }));
    });
});
