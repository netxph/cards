(function(angular) {

    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('CardAddCtrl', ['$scope', '$location', 'Cards', 'Areas', function($scope, $location, Cards, Areas) {

        var self = this;

        self.init = function() {
            $scope.data = {};
            
            Areas.getAll().$promise.then(function(result) {
                $scope.data.areas = result;
            });
            
            //convert card into class
            $scope.data.card = {
                areaID: 1,
                name: '',
                description: '',
                assignedTo: '',
                labels: []
            };

            $scope.data.label = '';
        }

        $scope.addLabel = function() {
            $scope.data.card.labels.push($scope.data.label);
            $scope.data.label = '';
        };


        $scope.addCard = function() {
            Cards.add($scope.data.card).$promise.then(function() {
                $location.path('/');
            }); 
        };

        self.init();
    }]);
})(angular);
