(function(angular) {
'use strict';

var cardsApp = angular.module('cardsApp')
  
cardsApp.controller('QuickAddCtrl', [
    '$scope',
    'Cards',
    'CardHelper',
    function ($scope, Cards, CardHelper) {

        function init() {
            $scope.state = {};
            $scope.state.isOpen = false;
            $scope.card = {
                areaID: $scope.area.id,
                name: '',
                description: '',
            };
        }

        $scope.showBox = function() {
            $scope.state.isOpen = true;
        };

        $scope.hideBox = function() {
            $scope.state.isOpen = false;
        };

        $scope.addCard = function(card) {
            Cards.add(card).$promise.then(function() {
                card.labels = CardHelper.getLabels(card.name);
                $scope.area.cards.push(card);
                init();
            });
        };

        init();
    }]);
})(angular);
