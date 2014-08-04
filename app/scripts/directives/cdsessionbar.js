'use strict';

/**
 * @ngdoc directive
 * @name cardsApp.directive:cdSessionBar
 * @description
 * # cdSessionBar
 */
angular.module('cardsApp')
  .directive('cdSessionBar',['AppSettings', function (AppSettings) {
    return {
      templateUrl: AppSettings.sessionTemplate,
      restrict: 'A',
      controller: 'SessionBarCtrl'
    };
  }]);
