(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('SessionBarCtrl', [
        '$scope',
        '$location',
        '$http',
        'AppSettings',
        'Session',
        'Rest',
        function ($scope, $location, $http, AppSettings, Session, Rest) {
            var uri = AppSettings.serviceBaseUrl + 'session'; 

            $scope.isAuthenticated = function() {
                return Session.isAuthenticated();
            };

            $scope.getCurrentUser = function() {
                return Session.getCurrentUser();
            };

            $scope.logout = function() {

                Rest.invoke($http.delete(uri),function() {
                    Session.destroy(); 
                    $location.path('/session/new');
                });
            };

        }]);
})(angular);
