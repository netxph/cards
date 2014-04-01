(function(jasmine) {
    'use strict';

    describe('Controller: LoginCtrl', function () {

        beforeEach(module('cardsApp'));

        var LoginCtrl,
            scope,
            http;

        beforeEach(inject(function ($controller, $rootScope, $httpBackend, State) {
            http = $httpBackend;
            State.userName = 'John Doe';
            scope = $rootScope.$new();
            LoginCtrl = $controller('LoginCtrl', {
                $scope: scope
            });
        }));

        afterEach(function() {
            http.verifyNoOutstandingRequest();
            http.verifyNoOutstandingExpectation();
        });

        it('should define controller', function () {
            expect(LoginCtrl).toBeDefined();
        });

        it('should define getUserName() as function', function() {
            expect(scope.getUserName).toEqual(jasmine.any(Function));
        });

        it('should getUserName() return a value', function() {
            expect(scope.getUserName()).toBe('John Doe');
        });

        it('should define signOut() as function', function() {
            expect(scope.signOut).toEqual(jasmine.any(Function));
        });

        it('should invoke session delete api', function() {
            http.expectDELETE('http://localhost/session').respond(200);
            scope.signOut();
            http.flush();
        });

    });
})(jasmine);
