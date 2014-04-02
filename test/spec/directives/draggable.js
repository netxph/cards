(function() {
    'use strict';

    describe('Directive: draggable', function () {

        // load the directive's module
        beforeEach(module('cardsApp'));

        var element,
        scope;

        beforeEach(inject(function ($rootScope) {
            scope = $rootScope.$new();
        }));

        it('should element is draggable', inject(function ($compile) {
            //element = angular.element('<draggable></draggable>');
            //element = $compile(element)(scope);
            //expect(element.text()).toBe('this is the draggable directive');
        }));
    });
})();
