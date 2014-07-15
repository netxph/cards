(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.factory('Session', [
        '$window',
        function ($window) {
            return {
                create: function(userName) {
                    $window.sessionStorage.user = userName;
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
                }
            };
    }]);
})(angular);
