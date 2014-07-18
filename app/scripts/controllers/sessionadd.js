(function(angular) {
'use strict';

var cardsApp = angular.module('cardsApp');

cardsApp.controller('SessionAddCtrl', [
    '$rootScope',
    '$scope',
    '$http',
    '$location',
    'Session',
    'AppSettings',
    function ($rootScope, $scope, $http, $location, Session, AppSettings) {

      var uri = AppSettings.serviceBaseUrl + 'session';

      $scope.login = function(session) {

        $rootScope.$broadcast('ajax_start');
        $http.post(uri, session)
            .success(function(data) {
                Session.create(data);
                $rootScope.$broadcast('ajax_end');

                $location.path('/');
            })
            .error(function() {
                Session.destroy();
                $rootScope.$broadcast('ajax_end');
            });
      };

  }]);
})(angular);
