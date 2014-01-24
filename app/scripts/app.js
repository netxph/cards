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
            .when('/cards/new', {
                templateUrl: 'views/card-add.html',
                controller: 'CardAddCtrl'
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

    //services
    'scripts/services/areaService.js'
],
function() {
    angular.bootstrap(document, ['cardsApp']);
});
