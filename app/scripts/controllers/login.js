(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp')
    
    cardsApp.controller('LoginCtrl', ['$scope', '$location', 'State', 'Session', function ($scope, $location, State, Session) {
        $scope.getUserName = function () {
            return State.userName;
        };

        $scope.signOut = function () {
            Session.delete().$promise.then(function() {
                State.userName = '';
                $location.path('/session/new');
            });
        };
    }]);
})(angular);
