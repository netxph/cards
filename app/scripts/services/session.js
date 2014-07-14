(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.factory('Session', [
        function () {
            var userName = null;

            return {
                create: function(user) {
                    userName = user;
                },
                isAuthenticated: function() {
                    return userName != null;
                },
                getCurrentUser: function() {
                    return userName;
                }
            };
    }]);
})(angular);
