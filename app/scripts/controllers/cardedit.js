(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('CardEditCtrl', [
        '$rootScope',
        '$scope', 
        '$location', 
        '$routeParams', 
        'Areas', 
        'Cards', 
        'CardHelper',
        function($rootScope, $scope, $location, $routeParams, Areas, Cards, CardHelper) {
        var self = this;

        self.getCard = function () {
            return cardResource.get({id: 1});
        };

        self.init = function() {
            var cardId = $routeParams.id;

            $scope.label = '';

            $rootScope.$broadcast('ajax_start');
            Areas.getAll().$promise.then(function(result) {
                $scope.areas = result;
                return Cards.get(cardId).$promise;
            }).then(function(result) {
                result.labels = CardHelper.getLabels(result.name);

                $scope.card = result;
            }).finally(function() {
                $rootScope.$broadcast('ajax_end');
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

            $rootScope.$broadcast('ajax_start');
            Cards.edit(cardId, $scope.card).$promise.then(function() {
                $rootScope.$broadcast('ajax_end');
                $location.path('/');
            });

        };

        $scope.deleteCard = function() {
            var cardId = $routeParams.id;

            $rootScope.$broadcast('ajax_start');
            Cards.delete(cardId).$promise.then(function() {
                $rootScope.$broadcast('ajax_end');
                $location.path('/');
            });
        };

        self.init();
    }]);
})(angular);
