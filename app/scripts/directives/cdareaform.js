(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.directive('cdAreaForm', [
        function () {
            return {
                restrict: 'E',
                templateUrl: 'views/areaform-partial.html'
            }
        }
    ]);
})(angular);
