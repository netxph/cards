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
        var subject, controller, scope, $httpBackend;

        beforeEach(module('cardsApp'));
        beforeEach(inject(function ($controller, $rootScope) {

            scope = $rootScope.$new();
            controller = $controller('AreaAddCtrl', {
                $scope: scope
            });

            subject = scope.data.area;
        }));
    });
})();
