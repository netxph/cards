(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('AreaListCtrl', ['$scope', '$http', 'Areas', function($scope, $http, Areas) {
        $scope.data = {};

        Areas.query(function(data) {
            $scope.data.areas = data;
        });
    }]);
})(angular);
