(function(jasmine) {
    'use strict';

    angular.module('cardsApp')

    describe('Controller: AreaListCtrl - Areas', function() {

        var subject, controller, scope, $httpBackend;

        beforeEach(module('cardsApp', 'areaMock'));
        beforeEach(inject(function ($httpBackend, $controller, $rootScope, areasData, AppSettings) {

            AppSettings.serviceBaseUrl = 'http://localhost/';
            $httpBackend.whenGET('http://localhost/areas').respond(areasData);

            scope = $rootScope.$new();
            controller = $controller('AreaListCtrl', {
                $scope: scope
            });

            $httpBackend.flush();

            subject = scope.data.areas;
        }));

        it('should define areas', function() {
            expect(subject).toBeDefined();
        });

        it('areas not empty', function() {
            expect(subject.length).toBeGreaterThan(0);
        });

    });

    describe('Controller: AreaListCtrl - MoveCard', function() {
        var scope, controller, http, params;

        beforeEach(module('cardsApp', 'areaMock'));
        beforeEach(inject(function($controller, $rootScope, $httpBackend, $routeParams, areasData) {
            params = $routeParams;

            http = $httpBackend;
            http.whenGET('http://localhost/areas').respond(areasData);

            scope = $rootScope.$new();
            controller = $controller('AreaListCtrl', {
                $scope: scope
            });
        }));

        it('should define moveCard() as function', function() {
            expect(scope.moveCard).toEqual(jasmine.any(Function));
        });

        it('should invoke api move card', function() {
            params.id = 1;
            http.expectPUT('http://localhost/cards/1/move').respond(200);

            scope.moveCard(1, 1);
            http.flush();

            http.verifyNoOutstandingExpectation();
            http.verifyNoOutstandingRequest();
        });

    });

    describe('Controller: AreaListCtrl - Area', function() {
        var subject, controller, scope;

        beforeEach(module('cardsApp', 'areaMock'));
        beforeEach(inject(function ($httpBackend, $controller, $rootScope, AppSettings, areasData) {

            AppSettings.serviceBaseUrl = 'http://localhost/';
            $httpBackend.whenGET('http://localhost/areas').respond(areasData);

            scope = $rootScope.$new();
                controller = $controller('AreaListCtrl', {
                $scope: scope
            });

            $httpBackend.flush();

            subject = scope.data.areas[0];
       }));

        it('should define id', function() {
            expect(subject.id).toBeDefined();
        });

        it('id has value', function() {
            expect(subject.id).toBe(1);
        });

        it('should define name', function() {
            expect(subject.name).toBeDefined();
        });

        it('name has value', function() {
            expect(subject.name).toBe('Backlog');
        });

        it('should define cards', function() {
            expect(subject.cards).toBeDefined();
        });

        it('cards not empty', function() {
            expect(subject.cards.length).toBeGreaterThan(0);
        });

    });


    describe('Controller: AreaListCtrl - Card', function() {
        var subject, controller, scope;

        beforeEach(module('cardsApp', 'areaMock'));
        beforeEach(inject(function ($httpBackend, $controller, $rootScope, AppSettings, areasData) {

            AppSettings.serviceBaseUrl = 'http://localhost/';
            $httpBackend.whenGET('http://localhost/areas').respond(areasData);

            scope = $rootScope.$new();
            controller = $controller('AreaListCtrl', {
                $scope: scope
            });

            $httpBackend.flush();

            subject = scope.data.areas[0].cards[0];
        }));

        it('should define name', function() {
            expect(subject.name).toBeDefined();
        });

        it('name has value', function() {
            expect(subject.name).toBe('Create a design template for cards');
        });

        it('should define description', function() {
            expect(subject.description).toBeDefined();
        });

        it('description has value', function() {
            expect(subject.description).toBe('description goes here');
        });

        it('should define labels', function() {
            expect(subject.labels).toBeDefined();
        });

        it('labels not empty', function() {
            expect(subject.labels.length).toBeGreaterThan(0);
        });

    });

    describe('Controller: AreaListCtrl - Label', function() {

        var subject, controller, scope;

        beforeEach(module('cardsApp', 'areaMock'));
        beforeEach(inject(function ($httpBackend, $controller, $rootScope, AppSettings, areasData) {

            AppSettings.serviceBaseUrl = 'http://localhost/';
            $httpBackend.whenGET('http://localhost/areas').respond(areasData);

            scope = $rootScope.$new();
            controller = $controller('AreaListCtrl', {
                $scope: scope
            });

            $httpBackend.flush();
            subject = scope.data.areas[0].cards[0].labels[0];
        }));

        it('should have value', function() {
            expect(subject).toBe('Bug');
        });
    });
})(jasmine);
