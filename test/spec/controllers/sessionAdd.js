(function() {
    'use strict';

    describe('Controller: SessionAddCtrl - Session', function () {
        var subject, scope, controller;

        beforeEach(module('cardsApp'));
        beforeEach(inject(function ($controller, $rootScope) {
            scope = $rootScope.$new(); 

            controller = $controller('SessionAddCtrl', {
                $scope: scope
            });

            subject = scope.session;
        }));

        it('should define session', function() {
            expect(subject).toBeDefined();
        });

        it('should define session.userId', function() {
            expect(subject.userId).toBeDefined();
        });

        it('should session.userId be empty', function() {
            expect(subject.userId).toBe('');
        });

        it('should define session.password', function() {
            expect(subject.password).toBeDefined();
        });

        it('should session.password be empty', function() {
            expect(subject.password).toBe('');
        });

    });

    describe('Controller: SessionAddCtrl - Login Definition', function () {

        var subject, scope, controller;

        beforeEach(module('cardsApp'));
        beforeEach(inject(function ($controller, $rootScope) {
            scope = $rootScope.$new();
            controller = $controller('SessionAddCtrl', {
                $scope: scope
            });

            subject = scope.login;
        }));

        it('should define login method', function() {
            expect(subject).toBeDefined();
        });

        it('should login is function', function () {
            expect(subject).toEqual(jasmine.any(Function));
        });
    });

    describe('Controller: SessionAddCtrl - Login', function () {

        var subject, scope, controller, http;

        beforeEach(module('cardsApp'));
        beforeEach(inject(function ($controller, $rootScope, $httpBackend, AppSettings) {
            scope = $rootScope.$new();
            controller = $controller('SessionAddCtrl', {
                $scope: scope
            });

            http = $httpBackend;

            AppSettings.serviceBaseUrl = 'http://localhost/';
            http.expectPOST('http://localhost/session').respond(200);

            scope.session = {
                userId: 'me@cards.com',
                password: 'password'
            };
            subject = scope.login();
        }));

        afterEach(function() {
            http.verifyNoOutstandingRequest();
            http.verifyNoOutstandingExpectation();
        });

        it('should not return null', function () {
            expect(subject).not.toBeNull();
        });

    });

})();
