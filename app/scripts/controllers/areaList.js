'use strict';

var cardsApp = angular.module('cardsApp');

cardsApp.controller('AreaListCtrl', ['$scope', '$http', 'Areas', function($scope, $http, Areas) {
    $scope.data = {};

    Areas.getAll().success(function(data) {
        $scope.data.areas = data;
    });
}]);
