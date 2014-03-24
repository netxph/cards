'use strict';

angular.module('cardsApp', [
        'ngCookies',
        'ngResource',
        'ngSanitize',
        'ngRoute'
        ])
.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
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
});
