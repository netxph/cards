(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('CardEditCtrl', ['$scope', '$location', '$routeParams', 'Areas', 'Cards', function($scope, $location, $routeParams, Areas, Cards) {
        var self = this;

        self.getCard = function () {
            return cardResource.get({id: 1});
        };

        self.init = function() {
            var cardId = $routeParams.id;

            $scope.label = '';

            Areas.getAll().$promise.then(function(result) {
                $scope.areas = result;
                return Cards.get(cardId).$promise;
            }).then(function(result) {
                $scope.card = result;
            });
        };

        $scope.addLabel = function() {
            $scope.card.labels.push($scope.label);
            $scope.label = '';
        }

        $scope.editCard = function() {
            var cardId = $routeParams.id;

            Cards.edit(cardId, $scope.card).$promise.then(function() {
                $location.path('/');
            });

        };

        self.init();
    }]);
})(angular);
