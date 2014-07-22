(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.directive('cdSearch', function () {
        return {
            templateUrl: 'views/search-partial.html',
            restrict: 'E',
            controller: 'SearchCtrl'
        };
    });
})(angular);
