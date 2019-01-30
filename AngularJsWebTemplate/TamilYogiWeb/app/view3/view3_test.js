'use strict';

describe('myApp.view3 module', function () {

    beforeEach(module('myApp.view3'));
    beforeEach(module('myApp.common'));

    describe('view3 controller', function () {

        // Then we create some variables we're going to use
        var driversController, scope;

        beforeEach(inject(function ($controller, $rootScope) {
            scope = $rootScope.$new();
        }));

        it('controller should be defined', inject(function ($controller) {
            //spec body
            var view3Ctrl = $controller('firstCtrl', {$scope: scope});
            expect(view3Ctrl).toBeDefined();
        }));
    });
});
