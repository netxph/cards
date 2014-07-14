(function(angular) {
    'use strict';

    describe('Directive: cdSessionBar', function () {

        // load the directive's module
        beforeEach(module('cardsApp'));

        var element,
        scope;

    beforeEach(inject(function ($rootScope) {
        scope = $rootScope.$new();
    }));

    it('should define element', inject(function ($compile) {
        element = angular.element('<p cd-session-bar></p>');
        element = $compile(element)(scope);
        expect(element.text()).not.toBeNull();
    }));
    });
})(angular);
