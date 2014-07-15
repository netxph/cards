(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('SessionBarCtrl', [
        '$scope',
        '$location',
        '$http',
        'AppSettings',
        'Session',
        function ($scope, $location, $http, AppSettings, Session) {
            var uri = AppSettings.serviceBaseUrl + 'session'; 

            $scope.isAuthenticated = function() {
                return Session.isAuthenticated();
            };

            $scope.getCurrentUser = function() {
                return Session.getCurrentUser();
            };

            $scope.logout = function() {

                $http.delete(uri)
                    .success(function() {
                        Session.destroy(); 
                        $location.path('/session/new');
                    })
            };

        }]);
})(angular);
