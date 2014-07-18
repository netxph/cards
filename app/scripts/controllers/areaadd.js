(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('AreaAddCtrl', [
        '$rootScope',
        '$scope', 
        '$location', 
        'Areas', 
        function($rootScope, $scope, $location, Areas) {
        
        $scope.area = {
            name: '',
            cards: []
        };

        $scope.addArea = function() {
            
            $rootScope.$broadcast('ajax_start');
            Areas.add($scope.area).$promise.then(function() {
                $rootScope.$broadcast('ajax_end');
                $location.path('/');
            }, function() {
                $rootScope.$broadcast('ajax_end');
            });
        };

    }]);
})(angular);
