(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('AreaEditCtrl', [
        '$rootScope',
        '$scope', 
        '$location', 
        '$routeParams', 
        'AppSettings', 
        'Areas', function($rootScope, $scope, $location, $routeParams, AppSettings, Areas) {
        var self = this;

        self.init = function() {
            var areaId = $routeParams.id;

            $rootScope.$broadcast('ajax_start');
            Areas.get(areaId).$promise.then(function(result) {
                $scope.area = result;
            }).finally(function() {
                $rootScope.$broadcast('ajax_end');
            });
        };

        $scope.editArea = function () {
            var areaId = $routeParams.id;

            $rootScope.$broadcast('ajax_start');
            Areas.edit(areaId, $scope.area).$promise.then(function() {
                $rootScope.$broadcast('ajax_end');
                $location.path('/');           
            });
        };
        
        self.init();
    }]);
})(angular);
