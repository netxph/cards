(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('AreaEditCtrl', ['$scope', '$location', '$routeParams', 'AppSettings', 'Areas', function($scope, $location, $routeParams, AppSettings, Areas) {
        var self = this;

        self.init = function() {
            var areaId = $routeParams.id;

            Areas.get(areaId).$promise.then(function(result) {
                $scope.area = result;
            });
        };

        $scope.editArea = function () {
            var areaId = $routeParams.id;
            Areas.edit(areaId, $scope.area).$promise.then(function() {
                $location.path('/');           
            });
        };
        
        self.init();
    }]);
})(angular);
