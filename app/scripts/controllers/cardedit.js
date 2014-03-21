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

            $scope.data = {};

            $scope.data.label = '';

            Cards.get(cardId).$promise.then(function(result) {
                $scope.data.card = result;
            });

            Areas.getAll().$promise.then(function(result) {
                $scope.data.areas = result;
            });
        };

        $scope.addLabel = function() {
            $scope.data.card.labels.push($scope.data.label);
            $scope.data.label = '';
        }

        $scope.editCard = function() {
            var cardId = $routeParams.id;

            Cards.edit(cardId, $scope.data.card).$promise.then(function() {
                $location.path('/');
            }, function(error) {
                console.log(error);
            });
        };

        self.init();
    }]);
})(angular);
