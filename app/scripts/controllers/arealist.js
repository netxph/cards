(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('AreaListCtrl', ['$scope', '$http', '$location', 'Areas', function($scope, $http, $location, Areas) {
        $scope.data = {};

        $scope.init = function() {
            Areas.getAll().$promise.then(function (result) {
                $scope.data.areas = result;
            });
        };

        $scope.moveCard = function(cardId, areaId) {
        };

        $scope.init();

    }]);
})(angular);
