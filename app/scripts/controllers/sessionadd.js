(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('SessionAddCtrl', ['$scope', '$location', '$rootScope', 'AppSettings', 'Session', 'State', function ($scope, $location, $rootScope, AppSettings, Session, State) {

        $scope.data = {};

        $scope.data.session = {
            userId: '',
            password: ''
        };

        $scope.login = function () {
            Session.add($scope.data.session).$promise.then(function () {
                State.userName = $scope.data.session.userId;
                $rootScope.$broadcast('auth_changed');
                $location.path('/areas');
            }, function () {
                $location.path('/');
            });
        };

    }]);
})(angular);
