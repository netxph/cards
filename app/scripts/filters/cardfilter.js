(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');
    cardsApp.filter('cardFilter', [function () {
        return function (input, params) {
            var cards = input;
            var results = [];

            for (var i = 0;i < cards.length; i++) {
               var card = cards[i]; 
               var labels = card.labels.join().toLowerCase();
               var searchLabel = params.label.toLowerCase();
               
               if(searchLabel === '' || labels.indexOf(searchLabel) > -1){
                   results.push(card);
               }
            }

            return results;
        };
    }]);
})(angular);
