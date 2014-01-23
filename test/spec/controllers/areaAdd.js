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

            subject = scope.data.area;
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
        var subject, controller, scope, area;

        beforeEach(module('cardsApp'));
        beforeEach(inject(function ($controller, $rootScope) {

            scope = $rootScope.$new();
            controller = $controller('AreaAddCtrl', {
                $scope: scope
            });

            area = scope.data.area;
            area.name = "Todo";

            subject = scope.addArea();
        }));

        it('should not return nil', function() {
            expect(subject).not.toBeNull();
        });

        it('should area name is todo', function() {
            expect(subject.name).toBe('Todo');
        });

    });
})();
