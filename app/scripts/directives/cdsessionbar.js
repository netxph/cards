'use strict';

/**
 * @ngdoc directive
 * @name cardsApp.directive:cdSessionBar
 * @description
 * # cdSessionBar
 */
angular.module('cardsApp')
  .directive('cdSessionBar', function () {
    return {
      templateUrl: 'views/login-partial.html',
      restrict: 'A',
      controller: 'SessionBarCtrl'
    };
  })
