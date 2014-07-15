(function(angular) {
    'use strict';

    describe('Controller: SessionBarCtrl', function () {

        beforeEach(module('cardsApp'));

        var controller,
        scope,
        session,
        http;

        beforeEach(inject(function ($window, $controller, $rootScope, $httpBackend, Session) {
            delete $window.sessionStorage.user;
            
            http = $httpBackend;
            session = Session;

            scope = $rootScope.$new();
            controller = $controller('SessionBarCtrl', {
                $scope: scope
            });
        }));

        afterEach(function() {
            http.verifyNoOutstandingRequest();
            http.verifyNoOutstandingExpectation();
        });

        it('should define SessionBarCtrl', function() {
            expect(controller).not.toBeNull();
        });

        it('should define SessionBarCtrl.isAuthenticated', function() {
            expect(scope.isAuthenticated).toEqual(jasmine.any(Function));
        });

        it('should isAuthenticated return true', function() {
            session.create('me@cards.com');

            expect(scope.isAuthenticated()).toBe(true);
        });

        it('should isAuthenticated return false', function() {
            session.destroy();

            expect(scope.isAuthenticated()).toBe(false);
        });

        it('should define SessionBarCtrl.getCurrentUser', function() {
            expect(scope.getCurrentUser).toEqual(jasmine.any(Function));
        });

        it('should getCurrentUser has value', function() {
            session.create('me@cards.com');

            expect(scope.getCurrentUser()).toBe('me@cards.com');
        });

        it('should getCurrentUser is null', function() {
            session.destroy();

            expect(scope.getCurrentUser()).toBeNull();
        });

        it('should define SesssionBarCtrl.logout', function() {
            expect(scope.logout).toEqual(jasmine.any(Function));
        });

        it('should isAuthenticated return false when logout', function() {
            session.create('me@cards.com');

            http.expectDELETE('http://localhost/session').respond(200);

            scope.logout();
            http.flush();

            expect(scope.isAuthenticated()).toBe(false);
        });

    });
})(angular);
