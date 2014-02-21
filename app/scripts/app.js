'use strict'

var cardsApp = angular.module('cardsApp',['ngRoute', 'ngResource']);

cardsApp.config(
    function($routeProvider) {
        $routeProvider
            .when('/', {
                templateUrl: 'views/area-list.html',
                controller: 'AreaListCtrl'
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
    }
);

//configurations
cardsApp.constant('AppSettings', { 'serviceBaseUrl': 'http://localhost:9002/' });

require([
    //controllers
    'scripts/controllers/areaList.js',
    'scripts/controllers/areaAdd.js',
    'scripts/controllers/areaEdit.js',
    'scripts/controllers/cardAdd.js',
    'scripts/controllers/cardEdit.js',

    //services
    'scripts/services/areaService.js',
    'scripts/services/cardService.js'
],
function() {
    angular.bootstrap(document, ['cardsApp']);
});
