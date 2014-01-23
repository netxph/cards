(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('AreaAddCtrl', ['$scope', function($scope) {
        $scope.data = {};

        $scope.data.area = {
            name: '',
            cards: []
        };

    }]);
})(angular);
