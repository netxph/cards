'use strict'

var cardsApp = angular.module('cardsApp');

cardsApp.factory('areaService', function() {
    return {
        getAreas: function() {
            return [
            {
                name: "Backlog",
                cards: [
                {
                    name: "Create a design template for cards",
                    description: "description goes here",
                    labels: ["Bug"]
                }]
            }];
        },
    };
});
