'use strict';

angular.module('cardsApp')
  .directive('cardItem', function () {
    return {
      restrict: 'E',
      templateUrl: 'views/card-partial.html'
    };
  });
