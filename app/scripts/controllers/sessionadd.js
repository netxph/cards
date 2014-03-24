(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('SessionAddCtrl', ['$scope', function ($scope) {
        $scope.data = {};

        $scope.data.session = {
            userId: '',
            password: ''
        };

    }]);
})(angular);
