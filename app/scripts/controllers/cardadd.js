(function(angular) {

    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('CardAddCtrl', [
        '$scope', 
        '$location', 
        'Cards', 
        'Areas', 
        'CardHelper',
        'Rest',
        function($scope, $location, Cards, Areas, CardHelper, Rest) {

        var self = this;

        self.init = function() {
           
            Rest.invoke(Areas.getAll().$promise, function(result) {
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

        $scope.getLabels = function(text) {
            return CardHelper.getLabels(text);
        };

        $scope.addLabel = function() {
            $scope.card.labels.push($scope.label);
            $scope.label = '';
        };

        $scope.addCard = function() {
            Rest.invoke(Cards.add($scope.card).$promise, function() {
                $location.path('/');
            }); 
        };

        self.init();
    }]);
})(angular);
