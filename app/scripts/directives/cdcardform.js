(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp')

    cardsApp.directive('cdCardForm', ['AppSettings', function (AppSettings) {
        return {
            restrict: 'E',
            templateUrl: AppSettings.cardFormTemplate
        };
    }]);
})(angular);
