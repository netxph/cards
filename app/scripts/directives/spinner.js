(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.directive('cdSpinner', function() {
        return {
            restrict: 'A',
        link: function ($scope, element) {
            element.addClass('cd-hide');

            $scope.$on('ajax_start', function() {
                return element.removeClass('cd-hide');
            });

            $scope.$on('ajax_end', function () {
                return element.addClass('cd-hide');
            });
        }
        };
    });
})(angular);
