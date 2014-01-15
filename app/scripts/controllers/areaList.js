'use strict'

var cardsApp = angular.module('cardsApp');

cardsApp.controller('AreaListCtrl', ['$scope', '$http', function($scope, $http) {
    $scope.data = {};

    $http.jsonp('http://localhost/areas')
        .success(function(data) {
            if(data) {
                $scope.data.areas = data;
            }
        })
        .error(function(data) {
            console.error('Error fetching data:', data);
        });
}]);
