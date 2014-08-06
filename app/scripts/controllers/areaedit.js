(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('AreaEditCtrl', [
        '$scope', 
        '$location', 
        '$routeParams', 
        'AppSettings', 
        'Areas', 
        'Rest',
        function($scope, $location, $routeParams, AppSettings, Areas, Rest) {
            var self = this;

            self.init = function() {
                var areaId = $routeParams.id;

                Rest.invoke(Areas.get(areaId).$promise, function(result) {
                    $scope.area = result;
                });
            };

            $scope.editArea = function () {
                var areaId = $routeParams.id;

                Rest.invoke(Areas.edit(areaId, $scope.area).$promise, function() {
                    $location.path('/');           
                });
            };

            self.init();
        }]);
})(angular);
