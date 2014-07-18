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
        function($rootScope, $scope, $location, $routeParams, Areas, Cards) {
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
                $scope.card = result;
            }).finally(function() {
                $rootScope.$broadcast('ajax_end');
            });
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

        self.init();
    }]);
})(angular);
