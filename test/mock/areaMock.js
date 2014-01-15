'use strict';

var areas = angular.module('areaMock', [])
    .value('areasData', [
            {
                name: 'Backlog',
                cards: [{}]
            }]);
