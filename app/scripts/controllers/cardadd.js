(function(angular) {

    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('CardAddCtrl', [
        '$rootScope',
        '$scope', 
        '$location', 
        'Cards', 
        'Areas', 
        'CardHelper',
        function($rootScope, $scope, $location, Cards, Areas, CardHelper) {

        var self = this;

        self.init = function() {
           
            $rootScope.$broadcast('ajax_start');
            Areas.getAll().$promise.then(function(result) {
                $scope.areas = result;
            }).finally(function() {
                $rootScope.$broadcast('ajax_end');
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
            $rootScope.$broadcast('ajax_start');
            Cards.add($scope.card).$promise.then(function() {
                $rootScope.$broadcast('ajax_end');
                $location.path('/');
            }); 
        };

        self.init();
    }]);
})(angular);
