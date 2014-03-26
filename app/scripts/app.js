(function(angular) {
    'use strict';

    var cardsApp =  angular.module('cardsApp', ['ngCookies', 'ngResource', 'ngSanitize', 'ngRoute']);

    cardsApp.config(['$routeProvider', '$httpProvider', function ($routeProvider, $httpProvider) {
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

        $httpProvider.responseInterceptors.push('SpinnerInterceptor');
        $httpProvider.defaults.transformRequest.push(function(data, headersGetter) {
            $('.loading').show();
            return data;
        });

    }]);

    cardsApp.factory('SpinnerInterceptor', ['$q', '$window', function($q, $window) {
        return function(promise) {
            return promise.then(function(response) {
                $('.loading').hide();
                return response;
            }, function(response) {
                $('.loading').hide();
                return $q.reject(response);
            });
        };
    }]);

})(angular);
