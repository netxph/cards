(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('AreaListCtrl', ['$scope', '$http', 'Areas', function($scope, $http, Areas) {
        $scope.data = {};

        Areas.getAll().$promise.then(function (result) {
            $scope.data.areas = result;
        });
    }]);
})(angular);
