var cards;
cards = global.cards = {};

(function(cards) {
    'use strict';

    cards.Server = function() {
        var self = this;
        var data = [];

        function seed() {
            var areas = require('./seed.json');

            data = areas;
        };

        function getAreas() {
            return data;
        }

        function getCard(id) {
            for(var i = 0; i < data.length; i++) {
                for(var j = 0; j < data[i].cards.length; j++) {
                    var card = data[i].cards[j];

                    if(card.id == id) {
                        return card;
                    }
                }
            };

            return {};
        }

        function getArea(id) {
            for (var i = 0; i < data.length; i++) {
                if(data[i].id == id) {
                    return data[i];
                }
            };

            return {};
        }

        function editArea(id, area) {
            for (var i = 0; i <data.length; i++) {
                if(data[i].id == id) {
                    data[i].name = area.name;

                    return data[i];
                };
            };

            return {};
        }

        function initRoutes(app) {
            app.get('/areas', function(request, response) {
                console.log('GET: /areas');
                response.send(getAreas());
            });

            app.get('/cards/:id', function(request, response) {
                var id = request.params.id;
                console.log('GET: /cards/' + id);

                var card = getCard(id);
                response.send(card);
            });

            app.get('/areas/:id', function(request, response) {
                var id = request.params.id;
                console.log('GET: /areas/' + id);

                var area = getArea(id); 
                response.send(area);
            });

            app.put('/areas/:id', function(request, response) {
                var id = request.params.id;
                var area = request.body;
                console.log('PUT: /areas/' + id); 

                 
            });

            app.put('/cards/:id', function(request, response) {
                var id = request.params.id;
                var item = request.body;

                console.log('PUT: /cards/' + id); 

                for(var i = 0; i < data.length; i++) {
                    for(var j = 0; j < data[i].cards.length; j++) {
                        var card = data[i].cards[j];

                        if(card.id == id) {
                            card.name = item.name;
                            card.description = item.description;
                            card.assignedTo = item.assignedTo;
                            card.labels = item.labels;

                            response.send(card);
                            return;
                        }
                    }
                };
            });

            app.post('/areas', function(request, response) {
                console.log('POST: /areas');

                var area = request.body;
                console.log('DATA: ' + area);

                area.id = cards.currentAreaId;

                data.push(area);
                console.log('area added with ID: ' + cards.currentAreaId); 

                cards.currentAreaId++;

                response.send(area);
            });

            app.post('/cards', function(request, response) {
                console.log('POST: /cards');

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
        }

        self.listen = function(port) {
            seed();
            var express = require('express');
            var app = express();

            app.use(express.json());
            app.use(express.urlencoded());
            app.use(function(request, response, next) {
                response.header('Access-Control-Allow-Origin', ['*'])
                response.header('Access-Control-Allow-Methods', 'GET,PUT,POST,DELETE');
                response.header('Access-Control-Allow-Headers', 'Content-Type');

                next();
            });

            initRoutes(app); 

            console.log("Cards API started.");
            app.listen(port);
        };

        return self;
    };

    var server = new cards.Server();
    module.exports = server;

})(cards);

