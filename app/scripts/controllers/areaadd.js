(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('AreaAddCtrl', [
        '$scope', 
        '$location', 
        'Areas', 
        'Rest',
        function($scope, $location, Areas, Rest) {

            $scope.area = {
                name: '',
                cards: []
            };

            $scope.addArea = function() {

                Rest.invoke(Areas.add($scope.area).$promise, function() {
                    $location.path('/');
                });
            };

        }]);
})(angular);
