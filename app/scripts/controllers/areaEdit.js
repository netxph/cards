(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('AreaEditCtrl', ['$scope', '$routeParams', 'AppSettings', 'Areas', function($scope, $routeParams, AppSettings, Areas) {
        var self = this;

        self.getArea = function(areaId) {
            return Areas.get(areaId);
        };

        self.init = function() {
            var areaId = $routeParams.id;
            $scope.data = {};

            $scope.data.area = self.getArea(areaId);
        };

        $scope.editArea = function () {
            var areaId = $routeParams.id;

            Areas.edit(areaId, $scope.data.area);

            return $scope.data.area;
        };
        
        self.init();
    }]);
})(angular);
