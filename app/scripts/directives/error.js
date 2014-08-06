(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.directive('cdError', function() {
        return {
            restrict: 'A',
            link: function($rootScope, element) {
                element.addClass('cd-hide');

                $rootScope.$on('ajax_error', function() {
                    return element.removeClass('cd-hide');
                });

                $rootScope.$on('ajax_success', function() {
                    return element.addClass('cd-hide');
                });
            }
        };
    });
})(angular);
