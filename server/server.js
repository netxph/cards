var cards;
cards = global.cards = {};

(function(cards) {
    'use strict';

    cards.Server = function(express, app, data) {
        var self = this;

        self.seed = function() {
            data.push({
                id: 1,
                name: 'Backlog',
                cards: [{
                    id: 1,
                    areaId: 1,
                    name: 'Create a design template for cards',
                    description: 'description goes here',
                    labels: ['Bug'],
                }]
            });
        };

        self.init = function() {
            app.use(express.json());
            app.use(express.urlencoded());
            app.use(function(request, response, next) {
                response.header('Access-Control-Allow-Origin', ['*'])
                response.header('Access-Control-Allow-Methods', 'GET,PUT,POST,DELETE');
                response.header('Access-Control-Allow-Headers', 'Content-Type');

                next();
            });

            app.get('/areas', function(request, response) {
                console.log('/areas GET: invoked.');
                response.send(data);
            });

            app.post('/areas', function(request, response) {
                console.log('/areas POST: invoked.');

                var area = request.body;
                console.log('DATA: ' + area);

                area.id = cards.currentAreaId;

                data.push(area);
                console.log('area added with ID: ' + cards.currentAreaId); 

                cards.currentAreaId++;

                response.send(area);
            });

            app.post('/cards', function(request, response) {
                console.log('/cards POST: invoked.');

                var card = request.body;
                console.log('DATA: ' + card);

                for (var i = 0; i < data.length; i++) {

                    if(data[i].id == card.areaId) {
                        card.id = cards.currentCardId;
                        data[i].cards.push(card);    

                        console.log('card added to area with ID:' + cards.currentCardId);

                        cards.currentCardId++;
                    }
                };

                response.send(card);
            });

        };

    };

    if(!cards.data) {
        var express;
        var app;

        express = require('express');
        app = module.exports = express();

        var data = cards.data = [];
        cards.currentAreaId = 2;
        cards.currentCardId = 2;

        var server = new cards.Server(express, app, data);
        server.seed();
        server.init();
    }

})(cards);

