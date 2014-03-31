(function() {
    'use strict';

    describe('Directive: cdError', function () {
        var scope, element, root;

        beforeEach(module('cardsApp'));

        beforeEach(inject(function ($rootScope, $compile) {
            scope = $rootScope.$new();
            root = $rootScope;

            element = angular.element('<div cd-error></div>');
            element = $compile(element)(scope);
            
            scope.$digest();
        }));

        it('should not be visible', function () {
            expect(element.hasClass('cd-hide')).toBe(true);
        });

        it('should be visible on ajax error', function() {
            root.$broadcast('ajax_error');
            expect(element.hasClass('cd-hide')).toBe(false);
        });

        it('should not be visible on ajax success', function() {
            root.$broadcast('ajax_success');
            expect(element.hasClass('cd-hide')).toBe(true);
        });
    });
})();
