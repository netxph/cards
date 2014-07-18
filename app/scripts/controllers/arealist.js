(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('AreaListCtrl', [
        '$rootScope',
        '$scope', 
        '$http', 
        '$location', 
        'Areas', 
        'Cards', 
        function($rootScope, $scope, $http, $location, Areas, Cards) {

        $scope.init = function() {
            $rootScope.$broadcast('ajax_start');
            Areas.getAll().$promise.then(function (result) {
                $scope.areas = result;
            }).finally(function() {
                $rootScope.$broadcast('ajax_end');
            });
        };

        $scope.moveCard = function(cardId, areaId) {
            $rootScope.$broadcast('ajax_start');
            Cards.move(cardId, areaId).$promise.then(function (result) {
                $rootScope.$broadcast('ajax_end');
            });
        };

        $scope.init();

    }]);
})(angular);
