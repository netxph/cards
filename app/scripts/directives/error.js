(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.directive('cdError', function() {
        return {
            restrict: 'A',
            link: function($scope, element) {
                element.addClass('cd-hide');

                $scope.$on('ajax_error', function() {
                    return element.removeClass('cd-hide');
                });

                $scope.$on('ajax_success', function() {
                    return element.addClass('cd-hide');
                });
            }
        };
    });
})(angular);
