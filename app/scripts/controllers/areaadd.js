(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('AreaAddCtrl', ['$scope', '$location', 'Areas', function($scope, $location, Areas) {
        
        $scope.area = {
            name: '',
            cards: []
        };

        $scope.addArea = function() {
            Areas.add($scope.area).$promise.then(function() {
                $location.path('/');
            });
        };

    }]);
})(angular);
