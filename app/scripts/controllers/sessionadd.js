(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('SessionAddCtrl', ['$scope', '$location', '$rootScope', 'AppSettings', 'Session', function ($scope, $location, $rootScope, AppSettings, Session) {

        $scope.data = {};

        $scope.data.session = {
            userId: '',
            password: ''
        };

        $scope.login = function () {
            Session.add($scope.data.session).$promise.then(function () {
                $rootScope.$broadcast('auth_changed');
                $location.path('/areas');
            }, function () {
                $location.path('/');
            });
        };

    }]);
})(angular);
