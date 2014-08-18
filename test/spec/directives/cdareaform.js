(function(angular) {
    'use strict';

    describe('Directive: cdAreaForm', function () {

        // load the directive's module
        beforeEach(module('cardsApp'));

        var element,
        scope;

        beforeEach(inject(function ($rootScope) {
            scope = $rootScope.$new();
        }));

        it('should define directive', inject(function ($compile) {
            element = angular.element('<cd-area-form></cd-area-form>');
            element = $compile(element)(scope);
            expect(element).toBeDefined();
        }));

    });

})(angular);
