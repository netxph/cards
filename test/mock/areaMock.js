'use strict';

var areas = angular.module('areaMock', [])
    .value('areasData', [
            {
                name: 'Backlog',
                cards: [
                {
                    name: 'Create a design template for cards',
                    description: 'description goes here',
                    labels: ['Bug'],
                }]
            }]);
