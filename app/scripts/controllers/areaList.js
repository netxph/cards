'use strict'

var cardsApp = angular.module('cardsApp');

cardsApp.controller('AreaListCtrl', ['$scope', 'Areas', function($scope, Areas) {
    $scope.data = {};
    
    Areas.query(function(response) {
        $scope.data.areas = response; 
    });
}]);
