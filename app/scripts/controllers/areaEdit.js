(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('AreaEditCtrl', ['$scope', '$resource', '$routeParams', 'AppSettings', 'Areas', function($scope, $resource, $routeParams, AppSettings, Areas) {
        var self = this;

        self.getArea = function(areaId) {
            return Areas.getArea().get({id: areaId});
        };

        self.init = function() {
            var areaId = $routeParams.id;
            $scope.data = {};

            $scope.data.area = self.getArea(areaId);
        };
        
        self.init();
    }]);
})(angular);
