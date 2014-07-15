(function(angular) {
    'use strict';

    describe('Controller: SessionaddCtrl', function () {

        beforeEach(module('cardsApp'));

        var controller,
        scope,
        session,
        http,
        location;

        beforeEach(inject(function ($window, $controller, $rootScope, $httpBackend, $location, Session) {

            delete $window.sessionStorage.user;

            location = $location;
            http = $httpBackend;
            scope = $rootScope.$new();
            controller = $controller('SessionAddCtrl', {
                $scope: scope
            });

            session = Session;
        }));

        afterEach(function() {
            http.verifyNoOutstandingRequest();
            http.verifyNoOutstandingExpectation();
        });

        it('should define SessionAddCtrl', function() {
            expect(controller).not.toBeNull();
        });

        it('should define SessionAddCtrl.login()', function() {
            expect(scope.login).toEqual(jasmine.any(Function)); 
        });

        it('should be authenticated when login successful', function() {
            http.expectPOST('http://localhost/session').respond('me@cards.com');
            
            scope.login();
            http.flush();

            expect(session.isAuthenticated()).toBe(true);
        });

        it('should store user name in session', function() {
            http.expectPOST('http://localhost/session').respond('me@cards.com');
            
            scope.login();
            http.flush();

            expect(session.getCurrentUser()).toBe('me@cards.com');
        });

        it('should be not authenticated when login failed', function() {
            http.expectPOST('http://localhost/session').respond(401);

            scope.login();
            http.flush();

            expect(session.isAuthenticated()).toBe(false);
        });
    });
})(angular);
