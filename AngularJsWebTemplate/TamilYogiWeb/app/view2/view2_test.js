'use strict';

describe('myApp.view2 module', function () {

    beforeEach(module('myApp.view2'));
    beforeEach(module('myApp.common'));

    describe('view2 controller', function () {

        it('should be defined', inject(function ($controller) {
            //spec body
            var view2Ctrl = $controller('View2Ctrl', {$timeout: $timeout, $q: $q, $log: $log});
            expect(view2Ctrl).toBeDefined();
        }));

    });
});