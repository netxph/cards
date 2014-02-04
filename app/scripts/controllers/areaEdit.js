(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('AreaEditCtrl', ['$scope', '$resource', '$routeParams', 'AppSettings', 'Areas', function($scope, $resource, $routeParams, AppSettings, Areas) {
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
            var areaResource = $resource('http://localhost/areas/:id', null, {
                'update': { method: 'PUT' }
            });

            areaResource.update({id: $routeParams.id}, $scope.data.area); 

            return $scope.data.area;
        };
        
        self.init();
    }]);
})(angular);
