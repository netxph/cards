(function(angular) {
    'use strict';

    var cardsApp =  angular.module('cardsApp', ['ngCookies', 'ngResource', 'ngSanitize', 'ngRoute']);

    cardsApp.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        $routeProvider
        .when('/', {
            templateUrl: 'views/area-list.html',
            controller: 'AreaListCtrl'
        })
        .when('/session/new', {
            templateUrl: 'views/session-add.html',
            controller: 'SessionAddCtrl'
        })
        .when('/areas', {
            templateUrl: 'views/area-list.html',
            controller: 'AreaListCtrl'
        })
        .when('/areas/new', {
            templateUrl: 'views/area-add.html',
            controller: 'AreaAddCtrl'
        })
        .when('/areas/:id', {
            templateUrl: 'views/area-edit.html',
            controller: 'AreaEditCtrl'
        })
        .when('/cards/new', {
            templateUrl: 'views/card-add.html',
            controller: 'CardAddCtrl'
        })
        .when('/cards/:id', {
            templateUrl: 'views/card-edit.html',
            controller: 'CardEditCtrl'
        })
        .otherwise({
            redirectTo: '/'
        });

    }]);

    cardsApp.run(['$window', '$rootScope', '$location', 'Session', function($window, $rootScope, $location, Session) {
        
        //intercept route changes and check if user is authenticated
        $rootScope.$on('$routeChangeStart', function(event) {
            event.preventDefault();

            Session.checkAuthentication();
        });
    }]);

    cardsApp.directive('cdNodrag', function() {
        return function(scope, element, attr) {

            element.on('dragstart', function() {
                return false;
            });
        }
    });

})(angular);
