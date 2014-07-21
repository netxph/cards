(function(angular) {
    'use strict';

    describe('Directive: cdQuickadd', function () {

        // load the directive's module
        beforeEach(module('cardsApp'));

        var element,
        scope;

        beforeEach(inject(function ($rootScope) {
            scope = $rootScope.$new();
        }));

        it('should define quickadd directive', inject(function ($compile) {
            element = angular.element('<cd-quickadd></cd-quickadd>');
            element = $compile(element)(scope);
            expect(element.text()).not.toBeNull();
        }));
    });
})(angular);
