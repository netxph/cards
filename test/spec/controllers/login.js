(function(jasmine) {
    'use strict';

    describe('Controller: LoginCtrl', function () {

        beforeEach(module('cardsApp'));

        var LoginCtrl,
            scope;

        beforeEach(inject(function ($controller, $rootScope, State) {
            State.userName = 'John Doe';
            scope = $rootScope.$new();
            LoginCtrl = $controller('LoginCtrl', {
                $scope: scope
            });
        }));

        it('should define controller', function () {
            expect(LoginCtrl).toBeDefined();
        });

        it('should define getUserName() as function', function() {
            expect(scope.getUserName).toEqual(jasmine.any(Function));
        });

        it('should getUserName() return a value', function() {
            expect(scope.getUserName()).toBe('John Doe');
        });

    });
})(jasmine);
