'use strict';

/**
 * @ngdoc directive
 * @name cardsApp.directive:card
 * @description
 * # card
 */
angular.module('cardsApp')
  .directive('card', function () {
    return {
      template: '<div></div>',
      restrict: 'E',
      link: function postLink(scope, element, attrs) {
        element.text('this is the card directive');
      }
    };
  });
