(function() {
    'use strict';

    describe('Controller: AreaEditCtrl - Get Area', function() {

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

    describe('Controller: AreaEditCtrl - Update Area', function() {

        var scope, controller, area, http;

        beforeEach(module('cardsApp', 'areaMock'));
        beforeEach(inject(function ($controller, $rootScope, $httpBackend, $routeParams, areaData) {

            $routeParams.id = 1;

            http = $httpBackend;
            
            http.whenGET('http://localhost/areas/1').respond(areaData);
            http.whenPUT('http://localhost/areas/1').respond(200);

            scope = $rootScope.$new();
            controller = $controller('AreaEditCtrl', {
                $scope: scope
            });

            area = scope.data.area;

        }));

        afterEach(function() {
            http.verifyNoOutstandingExpectation();
        });

        it('should define updateArea()', function() {
            expect(scope.editArea).toBeDefined();
        });

        it('updateArea should be a function', function() {
            expect(scope.editArea).toEqual(jasmine.any(Function));
        });

        it('updateArea should not return nil', function() {
            var subject = scope.editArea();
            http.flush();
            
            expect(subject).not.toBeNull();
        });

        it('should perform POST in api', function() {
            http.expectPUT('http://localhost/areas/1', scope.data.area);

            var subject = scope.editArea();
            http.flush();
        });

    });
})();
