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
            var area = $scope.data.area;

            var areaApi = new Areas();
            areaApi.name = area.name;
            areaApi.cards = area.cards;

            areaApi.$save();

            return area;
        };

    }]);
})(angular);
