(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');
    cardsApp.controller('SearchCtrl', ['$rootScope', '$scope', function ($rootScope, $scope) {

        $scope.search = {};
        $scope.search.text = '';

        $scope.performSearch = function(text) {
            $rootScope.$emit('onFilterCards', 'test');
        };

    }]);
})(angular);
