(function(angular) {
    'use strict';

    angular.module('cardsApp')
    .directive('cdCard', ['AppSettings', function (AppSettings) {
        return {
            restrict: 'E',
        templateUrl: AppSettings.cardTemplate
        };
    }]);
})(angular);
