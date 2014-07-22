(function(angular) {
    'use strict';

    describe('Directive: cdSearch', function () {

        // load the directive's module
        beforeEach(module('cardsApp'));

        var element,
        scope;

    beforeEach(inject(function ($rootScope) {
        scope = $rootScope.$new();
    }));

    it('should define directive', inject(function ($compile) {
        element = angular.element('<cd-search></cd-search>');
        element = $compile(element)(scope);
        expect(element).not.toBeNull();
    }));
    });
})(angular);
