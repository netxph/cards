(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('AreaEditCtrl', ['$scope', '$resource', '$routeParams', 'AppSettings', function($scope, $resource, $routeParams, AppSettings) {
        var self = this;

        self.getArea = function(areaId) {
            var area = $resource(AppSettings.serviceBaseUrl + 'areas/:id', { id: '@id' });

            return area.get({ id: areaId });
        };

        self.init = function() {
            var areaId = $routeParams.id;
            $scope.data = {};

            $scope.data.area = self.getArea(areaId);
        };
        
        self.init();
    }]);
})(angular);
