(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('CardEditCtrl', ['$scope', '$routeParams', 'Cards', function($scope, $routeParams, Cards) {
        var self = this;

        self.getCard = function () {
            return cardResource.get({id: 1});
        };

        self.init = function() {
            var cardId = $routeParams.id;

            $scope.data = {};
            $scope.data.card = Cards.get(cardId);
        };

        $scope.editCard = function() {
            var cardId = $routeParams.id;

            Cards.edit(cardId, $scope.data.card);

            return $scope.data.card;
        };

        self.init();
    }]);
})(angular);
