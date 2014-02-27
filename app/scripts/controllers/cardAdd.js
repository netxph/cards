(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('CardAddCtrl', ['$scope', 'Cards', 'Areas', function($scope, Cards, Areas) {

        var self = this;

        self.init = function() {
            $scope.data = {};

            $scope.data.areas = Areas.getAll();

            //convert card into class
            $scope.data.card = {
                areaId: 0,
                name: '',
                description: '',
                assignedTo: '',
                labels: []
            };

            $scope.data.label = '';
        }

        $scope.addLabel = function() {
            $scope.data.card.labels.push($scope.data.label);
        };

        $scope.addCard = function() {
            Cards.add($scope.data.card);

            return $scope.data.card;
        };

        self.init();
    }]);
})(angular);
