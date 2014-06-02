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

            subject = scope.data.label;
        }));

        it('should define label', function() {
            expect(subject).toBeDefined();
        });

        it('should label is empty', function() {
            expect(subject).toBe('');
        });

    });

    describe('Controller: CardEditCtrl - Add Label', function() {

        var subject, scope, controller, http;

        beforeEach(module('cardsApp', 'cardMock'));
        beforeEach(inject(function($controller, $rootScope, $routeParams, $httpBackend, AppSettings, cardData) {

            $routeParams.id = 1;

            http = $httpBackend;
            AppSettings.serviceBaseUrl = 'http://localhost/';
            http.whenGET('http://localhost/cards/1').respond(cardData);
            http.whenGET('http://localhost/areas').respond([]);

            scope = $rootScope.$new();

            controller = $controller('CardEditCtrl', {
                $scope: scope
            });

            http.flush();

            scope.data.label = 'Feature';
            subject = scope.addLabel();
        }));

        afterEach(function() {
            http.verifyNoOutstandingExpectation();
            http.verifyNoOutstandingRequest();
        });

        it('should define addLabel', function() {
            expect(scope.addLabel).toBeDefined();
        });

        it('should addLabel is function', function() {
            expect(scope.addLabel).toEqual(jasmine.any(Function));
        });

        it('should labels contain item', function() {
            expect(scope.data.card.labels[1]).toBe('Feature');
        });

        it('should label empty after add', function() {
            expect(scope.data.label).toBe('');
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
            subject = scope.data.areas;
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
            http.expectPUT('http://localhost/cards/1', scope.data.card);
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
            subject = scope.data.card;
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
            expect(subject.name).toBe('create todo');
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
