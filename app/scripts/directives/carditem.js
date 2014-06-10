'use strict';

angular.module('cardsApp')
  .directive('cardItem', ['AppSettings', function (AppSettings) {
    return {
      restrict: 'E',
      templateUrl: AppSettings.cardTemplate
    };
  }]);
