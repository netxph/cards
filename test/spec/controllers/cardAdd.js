(function() {
    'use strict';

    describe('Controller: CardAddCtrl - Areas', function() {

        var subject, scope, controller, http;

        beforeEach(module('cardsApp', 'areaMock'));
        beforeEach(inject(function($controller, $rootScope, $httpBackend, areasData) {

            http = $httpBackend;

            http.expectGET('http://localhost/areas').respond(areasData);


            scope = $rootScope.$new();

            controller = $controller('CardAddCtrl', {
                $scope: scope
            });

            subject = scope.data.areas;
            http.flush();
        }));

        afterEach(function() {
            http.verifyNoOutstandingRequest();
            http.verifyNoOutstandingExpectation();
        });

        it('should define areas', function() {
            expect(subject).toBeDefined();
        });

        it('should areas not empty', function() {
            expect(subject.length).toBe(1);
        });

        it('should areas[0].id has value', function() {
            expect(subject[0].id).toBe(1);
        });

        it('should areas[0].name has value', function() {
            expect(subject[0].name).toBe('Backlog');
        });

    });

    describe('Controller: CardAddCtrl - Card', function() {

        var scope, subject, controller;

        beforeEach(module('cardsApp'));
        beforeEach(inject(function($controller, $rootScope) {
            scope = $rootScope.$new();
            controller = $controller('CardAddCtrl', {
                $scope: scope
            });

            subject = scope.data.card;

        }));

        it('should define card', function() {
            expect(subject).toBeDefined();
        });

        it('should define areaId', function() {
            expect(subject.areaId).toBeDefined();
        });

        it('card.areaId should be zero', function() {
            expect(subject.areaId).toBe(0);
        });

        it('card should define name', function()  {
            expect(subject.name).toBeDefined();
        });

        it('card.name should be empty', function () {
            expect(subject.name).toBe('');
        }); 

        it('card should define description', function() {
            expect(subject.description).toBeDefined();
        });

        it('card.description should be empty', function() {
            expect(subject.description).toBe('');
        });

        it('card should define assignedTo', function() {
            expect(subject.assignedTo).toBeDefined();
        });

        it('card.assignedTo should be empty', function() {
            expect(subject.assignedTo).toBe('');
        });

        it('card should define labels', function() {
            expect(subject.labels).toBeDefined();
        });

        it('card.labels should be empty array', function() {
            expect(subject.labels.length).toBe(0);
        });
    });

    describe('Controller: CardAddCtrl - Label', function() {

        var scope, controller, label;

        beforeEach(module('cardsApp'));
        beforeEach(inject(function ($controller, $rootScope) {
            scope = $rootScope.$new();

            controller = $controller('CardAddCtrl', {
                $scope: scope
            });

        }));

        it('should define label', function() {
            expect(scope.data.label).toBeDefined();
        });

        it('label should be empty', function() {
            expect(scope.data.label).toBe('');
        });

        it('should define addLabel', function() {
            expect(scope.addLabel).toBeDefined();
        });

        it('should have addLabel as function', function() {
            expect(scope.addLabel).toEqual(jasmine.any(Function));
        });

        it('labels count should be 1 when label is added', function() {
            scope.data.label = 'Bug'; 
            scope.addLabel();
            
            expect(scope.data.card.labels.length).toBe(1);
        });

        it('label[0] should have value', function() {
            scope.data.label = 'Bug'; 
            scope.addLabel();
            
            expect(scope.data.card.labels[0]).toBe('Bug');
        });
    });

    describe('Controller: CardAddCtrl - AddMethod', function() {
      
        var scope, subject, controller, card, http;

        beforeEach(module('cardsApp'));
        beforeEach(inject(function ($controller, $rootScope, $httpBackend) {

            http = $httpBackend;
            http.whenPOST('http://localhost/cards').respond({});
            http.whenGET('http://localhost/areas').respond([]);

            scope = $rootScope.$new();

            controller = $controller('CardAddCtrl', {
                $scope: scope
            });

            card = scope.data.card;
            card.areaId = 1;
            card.name = 'Create a test';
            card.description = 'description goes here';
            card.assignedTo = 'me@cards.com';
            card.labels = ['Bug'];
        }));

        afterEach(function() {
            http.verifyNoOutstandingExpectation();
            http.verifyNoOutstandingRequest();
        });

        it('should define addCard', function() {
            expect(scope.addCard).toBeDefined();

            http.flush();
        });

        it('should have addCard as function', function() {
            expect(scope.addCard).toEqual(jasmine.any(Function))

            http.flush();
        });

        it('should not return nil', function() {
            var subject = scope.addCard();
            expect(subject).not.toBeNull();

            http.flush();
        });

        it('card.areaId should return a valud', function() {
            var subject = scope.addCard();
            expect(subject.areaId).toBe(1);

            http.flush();
        });

        it('card.name should return a value', function() {
            var subject = scope.addCard();
            expect(subject.name).toBe('Create a test');

            http.flush();
        });

        it('card.description should return a value', function() {
            var subject = scope.addCard();
            expect(subject.description).toBe('description goes here');

            http.flush();
        });

        it('card.assignedTo should return a value', function() {
            var subject = scope.addCard();
            expect(subject.assignedTo).toBe('me@cards.com');

            http.flush();
        });

        it('card.labels should not be empty', function() {
            var subject = scope.addCard();
            expect(subject.labels.length).toBe(1);

            http.flush();
        });

        it('card.label[0] should return a value', function() {
            var subject = scope.addCard();
            expect(subject.labels[0]).toBe('Bug');

            http.flush();
        });

        it('should perform POST in api', function() {
            http.expectPOST('http://localhost/cards');
            subject = scope.addCard();

            http.flush();
        });

    });
})();
