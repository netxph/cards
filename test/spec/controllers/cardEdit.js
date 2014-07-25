(function() {
    'use strict';

    describe('Controller: CardEditCtrl - Labels', function() {

        var subject, scope, controller;

        beforeEach(module('cardsApp'));
        beforeEach(inject(function($controller, $rootScope) {

            scope = $rootScope.$new();

            controller = $controller('CardEditCtrl', {
                $scope: scope
            });

            subject = scope.label;
        }));

        it('should define label', function() {
            expect(subject).toBeDefined();
        });

        it('should label is empty', function() {
            expect(subject).toBe('');
        });

    });

    describe('Controller: CardEditCtrl - DeleteCard', function() {

        var controller, scope, location, http;

        beforeEach(module('cardsApp', 'areaMock', 'cardMock'));
        beforeEach(inject(function($controller, $rootScope, $location, $httpBackend, $routeParams, areasData, cardData) {
            $routeParams.id = 1;
            scope = $rootScope.$new();
            location = $location;
            http = $httpBackend;

            controller = $controller('CardEditCtrl', {
                $scope: scope
            });

            http.expectGET('http://localhost/areas').respond(areasData);
            http.expectGET('http://localhost/cards/1').respond(cardData);
            http.flush();
        }));

        afterEach(function() {
            http.verifyNoOutstandingRequest();
            http.verifyNoOutstandingExpectation();
        });

        it('should define deleteCard()', function() {
            expect(scope.deleteCard).toEqual(jasmine.any(Function));
        });

        it('should be successful when delete', function() {
            http.expectDELETE('http://localhost/cards/1').respond(200);

            scope.deleteCard();
            http.flush();
        });

        it('should redirect to home when delete', function() {
            spyOn(location, 'path');
            http.expectDELETE('http://localhost/cards/1').respond(200);

            scope.deleteCard();
            http.flush();

            expect(location.path).toHaveBeenCalledWith('/');
        });
    });

    describe('Controller: CardEditCtrl - GetLabels', function() {

        var controller, scope;

        beforeEach(module('cardsApp'));
        beforeEach(inject(function($controller, $rootScope) {
            scope = $rootScope.$new();

            controller = $controller('CardEditCtrl', {
                $scope: scope
            });
        }));

        it('should define getLabels()', function() {
            expect(scope.getLabels).toEqual(jasmine.any(Function));
        });

        it('should return a value', function() {
            var labels = scope.getLabels('test #test the #other');
            expect(labels[0]).toBe('test');
        });
    });

    describe('Controller: CardEditCtrl - Areas', function() {

        var subject, scope, controller, http;

        beforeEach(module('cardsApp', 'areaMock'));
        beforeEach(inject(function($controller, $rootScope, $httpBackend, $routeParams, AppSettings, areasData) {

            $routeParams.id = 1;

            http = $httpBackend;

            AppSettings.serviceBaseUrl = 'http://localhost/';
            http.expectGET('http://localhost/areas').respond(areasData);
            http.whenGET('http://localhost/cards/1').respond({});

            scope = $rootScope.$new();

            controller = $controller('CardEditCtrl', {
                $scope: scope
            });

            http.flush();
            subject = scope.areas;
        }));

        afterEach(function() {
            http.verifyNoOutstandingExpectation();
        });

        it('should define areas', function() {
            expect(subject).toBeDefined();
        });

        it('should areas not empty', function() {
            expect(subject.length).toBe(1);
        });

        it('should areas[0].id should have value', function() {
            expect(subject[0].id).toBe(1);
        });

        it('should areas[0].name should have value', function() {
            expect(subject[0].name).toBe('Backlog');
        });

    });

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
        beforeEach(inject(function($controller, $rootScope, $httpBackend, $routeParams, AppSettings, cardData) {
           
            $routeParams.id = 1;
            http = $httpBackend;
            AppSettings.serviceBaseUrl = 'http://localhost/';
            http.whenGET('http://localhost/areas').respond([]);
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
            http.expectPUT('http://localhost/cards/1', scope.card);
        });

        afterEach(function() {
            http.verifyNoOutstandingRequest();
            http.verifyNoOutstandingExpectation();
        });
        
        it('should not be null', function() {
            expect(subject).not.toBeNull();
        });

    });

    describe('Controller: CardEditCtrl - Get Card', function() {
       
        var subject, scope, controller, http;

        beforeEach(module('cardsApp', 'cardMock'));
        beforeEach(inject(function ($controller, $rootScope, $httpBackend, $routeParams, AppSettings, cardData) {
            $routeParams.id = 1;
            
            scope = $rootScope.$new();

            http = $httpBackend;
            AppSettings.serviceBaseUrl = 'http://localhost/';
            http.whenGET('http://localhost/areas').respond([]);
            http.whenGET('http://localhost/cards/1').respond(cardData);
            http.expectGET('http://localhost/cards/1');  

            controller = $controller('CardEditCtrl', {
                $scope: scope
            });

            http.flush();
            subject = scope.card;
        }));

        afterEach(function() {
            http.verifyNoOutstandingExpectation();
            http.verifyNoOutstandingRequest();
        });

        it('should define card', function() {
            expect(subject).toBeDefined();
        });

        it('should define areaID', function() {
            expect(subject.areaID).toBeDefined();
        });

        it('areaID should have value', function() {
            expect(subject.areaID).toBe(1);
        });

        it('should define id', function() {
            expect(subject.id).toBeDefined();
        });

        it('id should have value', function() {
            expect(subject.id).toBe(1);
        });

        it('should define name', function() {
            expect(subject.name).toBeDefined();
        });

        it('name should have value', function() {
            expect(subject.name).toBe('create todo #bug');
        });


        it('should define description', function() {
            expect(subject.description).toBeDefined();
        });

        it('description should have value', function() {
            expect(subject.description).toBe('description goes here');
        });

        it('should define assignedTo', function() {
            expect(subject.assignedTo).toBeDefined();
        });

        it('assignedTo should have value', function() {
            expect(subject.assignedTo).toBe('me@cards.com');
        });

        it('should define labels', function() {
            expect(subject.labels).toBeDefined();
        });

        it('labels should not be empty', function() {
            expect(subject.labels.length).toBe(1);
        });

        it('labels should contain a value', function() {
            expect(subject.labels[0]).toBe('bug');
        });

    });
})();
