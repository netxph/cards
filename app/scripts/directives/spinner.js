(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.directive('cdSpinner', function() {
        return {
            restrict: 'A',
        link: function ($scope, element) {
            $scope.$on('ajax_start', function() {
                return element.addClass('loading-show');
            });

            $scope.$on('ajax_end', function () {
                return element.removeClass('loading-show');
            });
        }
        };
    });
})(angular);
