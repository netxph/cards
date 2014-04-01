(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp')
    
    cardsApp.directive('cdLogon', ['State', function (State) {
        return {
            restrict: 'A',
            templateUrl: 'views/login-partial.html',             
            controller: 'LoginCtrl'
        }
    }]);
})(angular);

