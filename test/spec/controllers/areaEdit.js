(function() {
    'use strict';

    describe('Controller: AreaEditCtrl - Area', function() {

        var subject, scope, controller, http;

        beforeEach(module('cardsApp'));
        beforeEach(inject(function ($controller, $rootScope, $routeParams, $httpBackend) {
            
            $routeParams.id = 1;

            http = $httpBackend;
            http.whenGET('http://localhost/areas/1').respond({
                id: 1,
                name: 'Backlog'
            });

            scope = $rootScope.$new(); 
            controller = $controller('AreaEditCtrl', {
                $scope: scope
            });

            http.flush();
            subject = scope.data.area;
        }));

        afterEach(function() {
            http.verifyNoOutstandingExpectation();
            http.verifyNoOutstandingRequest();
        });

        it('should define area', function() {
            expect(subject).toBeDefined();
        });

        it('area.id should have value', function() {
            expect(subject.id).toBe(1);
        });

        it('area.name should have value', function() {
            expect(subject.name).toBe('Backlog');
        });

    });
})();
