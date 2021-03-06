(function() {
    'use strict';

    describe('Directive: cdSpinner', function () {
        var element, scope, root;

        beforeEach(module('cardsApp'));

        beforeEach(inject(function ($rootScope, $compile) {
            scope = $rootScope.$new();
            root = $rootScope;

            element = angular.element('<div cd-spinner></div>');
            element = $compile(element)(scope);
            scope.$digest();

        }));

        it('should not be visible', function () {
            expect(element.hasClass('cd-hide')).toBe(true);
        });

        it('should be visible when ajax start', function() {
            root.$broadcast('ajax_start');
            expect(element.hasClass('cd-hide')).toBe(false);
        });

        it('should not be visible when ajax end', function() {
            root.$broadcast('ajax_start');
            root.$broadcast('ajax_end');

            expect(element.hasClass('cd-hide')).toBe(true);
        });

    });
})();
