(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp')
    
    cardsApp.controller('LoginCtrl', ['$scope', 'State', function ($scope, State) {
        $scope.getUserName = function () {
            return State.userName;
        };
    }]);
})(angular);
