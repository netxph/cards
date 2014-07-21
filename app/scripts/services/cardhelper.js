(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp')
    
    cardsApp.factory('CardHelper', [function () {
        var hashMatcher = /#([A-Za-z0-9]+)/gi;

        return {
            getLabels: function (text) {
                var labels = [];

                if(!!text) {
                    var match;
                    while((match = hashMatcher.exec(text)) !== null) {
                        labels.push(match[1]); //second one with no hash symbol
                    }
                }

                return labels;
            }
        }
    }]);

})(angular);
