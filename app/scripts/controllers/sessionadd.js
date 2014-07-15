(function(angular) {
'use strict';

var cardsApp = angular.module('cardsApp');

cardsApp.controller('SessionAddCtrl', [
    '$scope',
    '$http',
    '$location',
    'Session',
    'AppSettings',
    function ($scope, $http, $location, Session, AppSettings) {

      var uri = AppSettings.serviceBaseUrl + 'session';

      $scope.login = function(session) {

        $http.post(uri, session)
            .success(function(data) {
                Session.create(data);

                $location.path('/');
            })
            .error(function() {
                Session.destroy();
            });
      };

  }]);
})(angular);
