(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');


    cardsApp.controller('CardAddCtrl', ['$scope', 'Areas', function($scope) {
        $scope.data = {};

        $scope.data.card = {
            name: '',
            description: '',
            assignedTo: '',
            labels: []
        };
    }]);
})(angular);
