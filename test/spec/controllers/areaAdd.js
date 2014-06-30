(function() {
    'use strict';

    describe('Controller: AreaAddCtrl - Area', function() {

        var subject, controller, scope, $httpBackend;

        beforeEach(module('cardsApp'));
        beforeEach(inject(function ($controller, $rootScope) {

            scope = $rootScope.$new();
            controller = $controller('AreaAddCtrl', {
                $scope: scope
            });

            subject = scope.area;
        }));

        it('should define area', function() {
            expect(subject).toBeDefined();
        });

        it('area should define name', function() {
            expect(subject.name).toBeDefined();
        });

        it('area.name should be empty', function() {
            expect(subject.name).toBe('');
        });

        it('area should define cards', function() {
            expect(subject.cards).toBeDefined();
        });

        it('area.cards should be empty', function() {
            expect(subject.cards.length).toBe(0);
        });

    });

    describe('Controller: AreaAddCtrl - AddMethod', function() {
        var http, subject, controller, scope, area;

        beforeEach(module('cardsApp'));
        beforeEach(inject(function ($controller, $rootScope, $httpBackend, AppSettings) {

            http = $httpBackend;

            AppSettings.serviceBaseUrl = 'http://localhost/';
            http.whenPOST('http://localhost/areas').respond({});

            scope = $rootScope.$new();
            controller = $controller('AreaAddCtrl', {
                $scope: scope
            });

            area = scope.area;
            area.name = "Todo";

        }));

        afterEach(function() {
            http.verifyNoOutstandingExpectation();
            http.verifyNoOutstandingRequest();
        });

        it('should not return nil', function() {
            subject = scope.addArea();
            http.flush();
            
            expect(subject).not.toBeNull();
        });

        it('should perform POST in api', function() {
            http.expectPOST('http://localhost/areas');
            subject = scope.addArea();
            http.flush();
        });

    });
})();
