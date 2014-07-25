(function(cards) {
    'use strict';

    cards.Provider = function() {
        var self = this;
        var areas = {};
        var cards = {};
        var currentCardId = 5;
        var currentAreaId = 5;

        self.seed = function() {
            var data = require('./seed.json');

            for(var i = 0;i < data.length;i++) {
                
                var cardData = data[i].cards;
                data[i].cards = [];

                areas[data[i].id] = data[i];

                for (var j = 0; j < cardData.length; j++) {
                    cards[cardData[j].id] = cardData[j];
                };

            }
        };

        self.getAreas = function() {
            var areaKeys = Object.keys(areas);
            var cardKeys = Object.keys(cards);
            var result = [];

            for(var i = 0; i < areaKeys.length; i++) {
                var area = areas[areaKeys[i]];
                area.cards = [];

                for(var j = 0; j < cardKeys.length; j++) {
                    var card = cards[cardKeys[j]];
                    
                    if(area.id == card.areaID) {
                        area.cards.push(card);
                    }
                }

                result.push(area);
            }

            return result;
        }

        self.getCard = function(id) {
            var card = cards[id];

            return card;
        }

        self.deleteCard = function(id) {
            delete cards[id];
        }

        self.getArea = function(id) {
            var area = areas[id];
            var cardKeys = Object.keys(cards); 

            for(var i = 0; i < cardKeys.length; i++) {
                var card = cards[cardKeys[i]];

                if(area.id == card.areaID) {
                    area.cards.push(card);
                }
            }

            return area;
        }

        self.editArea = function(id, area) {
            var result = areas[id];
            result.name = area.name;

            return result;
        }

        self.editCard = function(id, card) {
            var result = cards[id];

            result.areaID = card.areaID;
            result.name = card.name;
            result.description = card.description;
            result.assignedTo = card.assignedTo;
            result.labels = card.labels;

            return result;
        }

        self.addArea = function(area) {
            area.id = currentAreaId;
            areas[area.id] = area;

            currentAreaId++;

            return area;
        }

        self.addCard = function(card) {
            card.id = currentCardId;
            cards[card.id] = card;

            currentCardId++;

            return card;
        }

    };

    var provider = new cards.Provider();
    module.exports = provider;

})(cards);
