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
})();
