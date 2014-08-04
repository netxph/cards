(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.factory('Session', [
        '$window',
        '$location',
        function ($window, $location) {
            return {
                create: function(session) {
                    $window.sessionStorage.user = session.name;
                },
                destroy: function() {
                    delete $window.sessionStorage.user;
                },
                isAuthenticated: function() {
                    var userName = $window.sessionStorage.user;
                    return userName != null;
                },
                getCurrentUser: function() {
                    return $window.sessionStorage.user || null;
                },
                checkAuthentication: function() {
                    if(!$window.sessionStorage.user) {
                        $location.path('/session/new');
                    };
                }
            };
    }]);
})(angular);
