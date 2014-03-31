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

    }]);

    cardsApp.config(['$httpProvider', function($httpProvider){
        var $http,
            interceptor = ['$q', '$injector', '$location', function($q, $injector, $location) {
                var rootScope;

                function broadcast(message) {
                    rootScope = rootScope || $injector.get('$rootScope');

                    rootScope.$broadcast(message);
                }

                function success(response) {
                    $http = $http || $injector.get('$http');
                    broadcast('ajax_success');

                    if($http.pendingRequests.length < 1) {
                        broadcast('ajax_end');
                    }

                    return response;
                };

                function error(response) {
                    $http = $http || injector.get('$http');
                    broadcast('ajax_error');

                    if($http.pendingRequests.length < 1) {
                        broadcast('ajax_end'); 
                    }

                    if(response.status == 401) {
                        $location.path('/session/new');
                    }

                    return $q.reject(response);

                };

                return function (promise) {
                    broadcast('ajax_start');

                    return promise.then(success, error);
                }
            }];

        $httpProvider.responseInterceptors.push(interceptor);
    }]);

})(angular);
