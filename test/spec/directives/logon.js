(function() {
    'use strict';

    describe('Directive: cdlogon', function () {
        var root, element, scope, state;

        beforeEach(module('cardsApp'));

        beforeEach(inject(function ($rootScope, $compile) {
            scope = $rootScope.$new();
            root = $rootScope;
            
            element = angular.element('<div cd-logon></div>');
                    }));

        it('should contain not signed in link', inject(function (State, $compile) {
            State.userName = '';

            element = $compile(element)(scope);
            scope.$digest();

            root.$broadcast('auth_changed');

            expect(element.html()).toMatch('Not Signed In');
        }));

        it('should contain sign out link', inject(function(State, $compile) {
            State.userName = 'John Doe';

            element = $compile(element)(scope);
            scope.$digest();

            root.$broadcast('auth_changed');

            expect(element.html()).toMatch('Sign Out');
        }));
    });
})();
