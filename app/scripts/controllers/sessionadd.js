(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('SessionAddCtrl', ['$scope', '$location', 'AppSettings', 'Session', function ($scope, $location, AppSettings, Session) {

        $scope.data = {};

        $scope.data.session = {
            userId: '',
            password: ''
        };

        $scope.login = function () {
            Session.add($scope.data.session).$promise.then(function () {
                $location.path('/areas');
            }, function () {
                $location.path('/');
            });
        };

    }]);
})(angular);
