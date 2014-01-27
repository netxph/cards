(function() {
    'use strict';

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

    describe('Controller: CardAddCtrl - AddMethod', function() {
      
        var scope, subject, controller, card, http;

        beforeEach(module('cardsApp'));
        beforeEach(inject(function ($controller, $rootScope, $httpBackend) {

            http = $httpBackend;
            http.whenPOST('http://localhost/cards').respond({});

            scope = $rootScope.$new();

            controller = $controller('CardAddCtrl', {
                $scope: scope
            });

            card = scope.data.card;
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
        });

        it('should have addCard as function', function() {
            expect(scope.addCard).toEqual(jasmine.any(Function))
        });

        it('should not return nil', function() {
            var subject = scope.addCard();
            expect(subject).not.toBeNull();

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
