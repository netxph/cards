(function(angular) {
    'use strict';

    /**
     * @ngdoc directive
     * @name cardsApp.directive:cdQuickadd
     * @description
     * # cdQuickadd
     */
    var cardsApp = angular.module('cardsApp');
    cardsApp.directive('cdQuickadd', function () {
        return {
            restrict: 'E',
            controller: 'QuickAddCtrl',
            templateUrl: 'views/quickadd-partial.html',
            link: function postLink(scope, element, attrs) {
            }
        };
    });

    cardsApp.directive('cdFocus', function() {
        return {
            restrict: 'A',
            scope: {
                isFocus: "=cdFocus"
            },
            link: function($scope, $element, attrs) {
                $scope.$watch('isFocus', function(current, previous) {
                    if(current === true) {
                        console.log($element[0]);
                        $element[0].focus();
                    }
                });
            }
        };
    });
})(angular);
