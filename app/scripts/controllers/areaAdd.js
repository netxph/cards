(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('AreaAddCtrl', ['$scope', 'Areas', function($scope, Areas) {
        $scope.data = {};

        $scope.data.area = {
            name: '',
            cards: []
        };

        $scope.addArea = function() {
            Areas.save($scope.data.area);

            return $scope.data.area;
        };

    }]);
})(angular);
