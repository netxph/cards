(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('SessionAddCtrl', [
        '$scope',
        '$http',
        '$location',
        'Session',
        'Rest',
        function ($scope, $http, $location, Session, Rest) {
            $scope.login = function(session) {

                Rest.invoke(Session.newSession(session), function(result) {
                    Session.create(result.data);
                    $location.path('/');
                }, function() {
                    Session.destroy();
                });

            };

        }]);
})(angular);
