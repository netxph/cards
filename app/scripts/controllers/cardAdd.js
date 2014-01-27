(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');


    cardsApp.controller('CardAddCtrl', ['$scope', 'Cards', function($scope, Cards) {
        $scope.data = {};

        $scope.data.card = {
            name: '',
            description: '',
            assignedTo: '',
            labels: []
        };

        $scope.addCard = function() {
            Cards.save($scope.data.card);

            return $scope.data.card;
        };
    }]);
})(angular);
