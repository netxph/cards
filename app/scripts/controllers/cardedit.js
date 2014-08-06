(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('CardEditCtrl', [
        '$scope', 
        '$location', 
        '$routeParams', 
        'Areas', 
        'Cards', 
        'CardHelper',
        'Rest',
        function($scope, $location, $routeParams, Areas, Cards, CardHelper, Rest) {
        var self = this;

        self.getCard = function () {
            return cardResource.get({id: 1});
        };

        self.init = function() {
            var cardId = $routeParams.id;

            $scope.label = '';

            Rest.invoke(Areas.getAll().$promise, function(result) {
                $scope.areas = result;

                Rest.invoke(Cards.get(cardId).$promise, function(result) {
                    result.labels = CardHelper.getLabels(result.name);

                    $scope.card = result;
                });
            });

        };

        $scope.getLabels = function(text) {
            return CardHelper.getLabels(text);
        };

        $scope.addLabel = function() {
            $scope.card.labels.push($scope.label);
            $scope.label = '';
        }

        $scope.editCard = function() {
            var cardId = $routeParams.id;

            Rest.invoke(Cards.edit(cardId, $scope.card).$promise, function() {
                $location.path('/');
            });

        };

        $scope.deleteCard = function() {
            var cardId = $routeParams.id;

            Rest.invoke(Cards.delete(cardId).$promise, function() {
                $location.path('/');
            });
        };

        self.init();
        }]);
})(angular);
