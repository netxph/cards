(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('AreaListCtrl', ['$scope', '$http', '$location', 'Areas', 'Cards', function($scope, $http, $location, Areas, Cards) {

        $scope.init = function() {
            Areas.getAll().$promise.then(function (result) {
                $scope.areas = result;
            });
        };

        $scope.moveCard = function(cardId, areaId) {
            Cards.move(cardId, areaId);
        };

        $scope.init();

    }]);
})(angular);
