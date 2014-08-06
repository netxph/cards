(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.factory('Session', [
        '$window',
        '$location',
        '$http',
        'AppSettings',
        function ($window, $location, $http, AppSettings) {
            var sessionUri = AppSettings.serviceBaseUrl + 'session';

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
                },
                newSession: function(session) {
                    return $http.post(sessionUri, session);
                }
            };
    }]);
})(angular);
