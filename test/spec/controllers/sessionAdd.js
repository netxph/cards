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

            subject = scope.data.session;
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

})();
