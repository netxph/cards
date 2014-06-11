(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.directive('cdMenu', ['AppSettings', function(AppSettings) {
        return {
            restrict: 'E',
            templateUrl: AppSettings.menuTemplate
        };
    }]);

})(angular);
