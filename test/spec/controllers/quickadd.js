(function(angular) {
    'use strict';

    describe('Controller: QuickAddCtrl - Add', function() {
        var scope, controller, http;

        beforeEach(module('cardsApp'));
        beforeEach(inject(function($controller, $rootScope, $httpBackend) {
            http = $httpBackend;
            scope = $rootScope.$new();
            scope.area = {
                id: 1,
                cards: []
            };

            controller = $controller('QuickAddCtrl', {
                $scope: scope
            });

            scope.card.name = 'This is a new card #new';
        }));

        afterEach(function() {
            http.verifyNoOutstandingRequest();
            http.verifyNoOutstandingExpectation();
        });

        it('should define QuickAddCtrl.addCard()', function() {
            expect(scope.addCard).toEqual(jasmine.any(Function)); 
        });

        it('should add to area.cards', function() {

            http.expectPOST('http://localhost/cards').respond(200);
            scope.addCard(scope.card);
            http.flush();

            expect(scope.area.cards[0].name).toBe('This is a new card #new'); 
        });

        it('should area.cards.labels[0] has value', function() {

            http.expectPOST('http://localhost/cards').respond(200);
            scope.addCard(scope.card);
            http.flush();

            expect(scope.area.cards[0].labels[0]).toBe('new');  
        });

        it('should blank out card', function() {

            http.expectPOST('http://localhost/cards').respond(200);
            scope.addCard(scope.card);
            http.flush();

            expect(scope.card.name).toBe('');
        });

    });

    describe('Controller: QuickAddCtrl', function () {

        // load the controller's module
        beforeEach(module('cardsApp'));

        var QuickaddCtrl,
        scope;

        // Initialize the controller and a mock scope
        beforeEach(inject(function ($controller, $rootScope) {
            scope = $rootScope.$new();
            scope.area = {
                id: 1
            };

            QuickaddCtrl = $controller('QuickAddCtrl', {
                $scope: scope
            });
        }));

        it('should define state', function() {
            expect(scope.state).toBeDefined();
        });

        it('should define state.isOpen', function() {
            expect(scope.state.isOpen).toBeDefined();
        });

        it('should isOpen is false', function() {
            expect(scope.state.isOpen).toBeFalsy();
        });

        it('should define showBox()', function() {
            expect(scope.showBox).toEqual(jasmine.any(Function));
        });

        it('should set open to true', function() {
            scope.showBox();
            expect(scope.state.isOpen).toBeTruthy();
        });

        it('should define hideBox()', function() {
            expect(scope.hideBox).toEqual(jasmine.any(Function));
        });

        it('should set open to false', function() {
            scope.showBox();
            scope.hideBox();
            expect(scope.state.isOpen).toBeFalsy();
        });

        it('should define card', function() {
            expect(scope.card).toBeDefined();
        });

        it('should define card.name', function() {
            expect(scope.card.name).toBeDefined();
        });

        it('should define card.description', function() {
            expect(scope.card.description).toBeDefined();
        });

        it('should define card.areaID', function() {
            expect(scope.card.areaID).toBeDefined();
        });

        it('should card.areaID has value', function() {
            expect(scope.card.areaID).toBe(1);
        });

    });
})(angular);
