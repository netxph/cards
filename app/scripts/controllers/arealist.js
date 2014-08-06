(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.controller('AreaListCtrl', [
        '$rootScope', 
        '$scope', 
        '$http', 
        '$location', 
        'Areas', 
        'Cards', 
        'CardHelper',
        'Rest',
        function($rootScope, $scope, $http, $location, Areas, Cards, CardHelper, Rest) {
            $scope.searchText = '';

            $scope.init = function() {
                Rest.invoke(Areas.getAll().$promise, function (result) {
                    var areas = result;
                    for (var i = 0; i < areas.length; i++) {
                        var area = areas[i];

                        for(var j = 0; j < area.cards.length; j++) {
                            var card = area.cards[j];

                            card.labels = CardHelper.getLabels(card.name);
                        }
                    }

                    $scope.areas = areas;
                });
            };

            $scope.moveCard = function(cardId, areaId) {
                Rest.invoke(Cards.move(cardId, areaId).$promise);
            };

            $scope.getColumns = function() {
                var count = $scope.areas.length;

                if(count > 4 && count < 7) {
                    return Math.floor(12/count);
                } else if(count > 6) {
                    return 12;
                } else {
                    return 3;
                }
            };

            $rootScope.$on('onFilterCards', function(event, text) {
                $scope.searchText = text;
            });

            $scope.init();

        }]);
})(angular);
