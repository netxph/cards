(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');


    cardsApp.controller('CardAddCtrl', ['$scope', '$resource', function($scope, $resource) {
        $scope.data = {};

        $scope.data.card = {
            name: '',
            description: '',
            assignedTo: '',
            labels: []
        };

        $scope.addCard = function() {
            var cards = $resource('http://localhost/cards'); 
            cards.save($scope.data.card);

            return $scope.data.card;
        };
    }]);
})(angular);
