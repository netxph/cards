'use strict'

var cardsApp = angular.module('cardsApp');

cardsApp.controller('AreaListCtrl', ['$scope', 'areaService', function($scope, areaService) {
    $scope.areas = areaService.getAreas(); 
}]);
