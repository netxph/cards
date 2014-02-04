(function(angular) {
'use strict';

var areaMock = angular.module('areaMock', []);

areaMock.value('areasData', [
            {
                id: 1, 
                name: 'Backlog',
                cards: [
                {
                    name: 'Create a design template for cards',
                    description: 'description goes here',
                    labels: ['Bug'],
                }]
            }]);

areaMock.value('areaData', {
    id: 1,
    name: 'Backlog',
    cards: []
});

})(angular);
