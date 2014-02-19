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

        self.init();
    }]);
})(angular);
