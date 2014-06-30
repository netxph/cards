(function(angular) {

    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('CardAddCtrl', ['$scope', '$location', 'Cards', 'Areas', function($scope, $location, Cards, Areas) {

        var self = this;

        self.init = function() {
            
            Areas.getAll().$promise.then(function(result) {
                $scope.areas = result;
            });
            
            //convert card into class
            $scope.card = {
                areaID: 1,
                name: '',
                description: '',
                assignedTo: '',
                labels: []
            };

            $scope.label = '';
        }

        $scope.addLabel = function() {
            $scope.card.labels.push($scope.label);
            $scope.label = '';
        };


        $scope.addCard = function() {
            Cards.add($scope.card).$promise.then(function() {
                $location.path('/');
            }); 
        };

        self.init();
    }]);
})(angular);
