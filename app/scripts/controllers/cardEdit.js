(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('CardEditCtrl', ['$scope', '$routeParams', 'Areas', 'Cards', function($scope, $routeParams, Areas, Cards) {
        var self = this;

        self.getCard = function () {
            return cardResource.get({id: 1});
        };

        self.init = function() {
            var cardId = $routeParams.id;

            $scope.data = {};

            Cards.get(cardId).$promise.then(function(result) {
                $scope.data.card = result;
            });

            Areas.getAll().$promise.then(function(result) {
                $scope.data.areas = result;
            });
        };

        $scope.editCard = function() {
            var cardId = $routeParams.id;

            Cards.edit(cardId, $scope.data.card);

            return $scope.data.card;
        };

        self.init();
    }]);
})(angular);
