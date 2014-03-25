(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('AreaAddCtrl', ['$scope', '$location', 'Areas', function($scope, $location, Areas) {
        $scope.data = {};

        $scope.data.area = {
            name: '',
            cards: []
        };

        $scope.addArea = function() {
            Areas.add($scope.data.area).$promise.then(function() {
                $location.path('/');
            }, function (error) {
                if(error.status == 401) {
                    $location.path('/session/new');
                }
            });
        };

    }]);
})(angular);
