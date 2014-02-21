(function() {
    'use strict';

    describe('Controller: CardEditCtrl - Update Card - State', function() {

        var scope, controller;

        beforeEach(module('cardsApp'));
        beforeEach(inject(function($controller, $rootScope) {

            scope = $rootScope.$new();
            controller = $controller('CardEditCtrl', {
                $scope: scope
            });

        }));

        it('should define editCard()', function() {
            expect(scope.editCard).toBeDefined();
        });

        it('should define editCard as function', function() {
            expect(scope.editCard).toEqual(jasmine.any(Function));
        });

    });

    describe('Controller: CardEditCtrl - Update Card', function() {
        
        var subject, scope, controller, http;

        beforeEach(module('cardsApp', 'cardMock'));
        beforeEach(inject(function($controller, $rootScope, $httpBackend, $routeParams, cardData) {
           
            $routeParams.id = 1;
            http = $httpBackend;
            
            http.whenGET('http://localhost/cards/1').respond(cardData);
            http.whenPUT('http://localhost/cards/1').respond(200);

            scope = $rootScope.$new(); 
            controller = $controller('CardEditCtrl', {
                $scope: scope
            });

            http.flush();

            subject = scope.editCard();
        }));

        beforeEach(function() {
            http.expectPUT('http://localhost/cards/1', scope.data.card);
        });

        afterEach(function() {
            http.verifyNoOutstandingRequest();
            http.verifyNoOutstandingExpectation();
        });
        
        it('should not be null', function() {
            expect(subject).not.toBeNull();
        });

        it('should card.id have value', function() {
            expect(subject.id).toBe(1);
        });
    });

    describe('Controller: CardEditCtrl - Get Card', function() {
       
        var subject, scope, controller, http;

        beforeEach(module('cardsApp', 'cardMock'));
        beforeEach(inject(function ($controller, $rootScope, $httpBackend, $routeParams, cardData) {
            $routeParams.id = 1;
            
            scope = $rootScope.$new();

            http = $httpBackend;
            http.whenGET('http://localhost/cards/1').respond(cardData);

            controller = $controller('CardEditCtrl', {
                $scope: scope
            });

            subject = scope.data.card;
        }));

        afterEach(function() {
            http.verifyNoOutstandingExpectation();
        });

        it('should define card', function() {
            http.flush();
            expect(subject).toBeDefined();
        });

        it('should define areaId', function() {
            http.flush();
            expect(subject.areaId).toBeDefined();
        });

        it('areaId should have value', function() {
            http.flush();
            expect(subject.areaId).toBe(1);
        });

        it('should define id', function() {
            http.flush();
            expect(subject.id).toBeDefined();
        });

        it('id should have value', function() {
            http.flush();
            expect(subject.id).toBe(1);
        });

        it('should define name', function() {
            http.flush();
            expect(subject.name).toBeDefined();
        });

        it('name should have value', function() {
            http.flush();
            expect(subject.name).toBe('create todo');
        });


        it('should define description', function() {
            http.flush();
            expect(subject.description).toBeDefined();
        });

        it('description should have value', function() {
            http.flush();
            expect(subject.description).toBe('description goes here');
        });

        it('should define assignedTo', function() {
            http.flush();
            expect(subject.assignedTo).toBeDefined();
        });

        it('assignedTo should have value', function() {
            http.flush();
            expect(subject.assignedTo).toBe('me@cards.com');
        });

        it('should define labels', function() {
            http.flush();
            expect(subject.labels).toBeDefined();
        });

        it('labels should not be empty', function() {
            http.flush();
            expect(subject.labels.length).toBe(1);
        });

        it('labels should contain a value', function() {
            http.flush();
            expect(subject.labels[0]).toBe('bug');
        });

        it('should invoke card GET in API', function() {
            http.expectGET('http://localhost/cards/1');  
            http.flush();
        });

    });
})();
