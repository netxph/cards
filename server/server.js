var cards;
cards = global.cards = {};

(function(cards) {
    'use strict';

    cards.Server = function() {
        var self = this;
        var data = [];

        var express = require('express');
        var app = module.exports = express();

        self.prototype.seed = function() {
            var seed = require('./seed.json');

            data.push(seed);
        };

        self.prototype.init = function() {

            app.use(express.json());
            app.use(express.urlencoded());
            app.use(function(request, response, next) {
                response.header('Access-Control-Allow-Origin', ['*'])
                response.header('Access-Control-Allow-Methods', 'GET,PUT,POST,DELETE');
                response.header('Access-Control-Allow-Headers', 'Content-Type');

                next();
            });

            initRoutes(app); 
        };

        function initRoutes(app) {
            app.get('/areas', function(request, response) {
                console.log('/areas GET: invoked.');
                response.send(data);
            });

            app.get('/cards/:id', function(request, response) {
                var id = request.params.id;
                console.log('/cards/' + id + ' GET: invoked.');

                for(var i = 0; i < data.length; i++) {
                    for(var j = 0; j < data[i].cards.length; j++) {
                        var card = data[i].cards[j];

                        if(card.id == id) {
                            response.send(card);
                            return;
                        }
                    }
                };

                response.send({});
            });

            app.get('/areas/:id', function(request, response) {
                var id = request.params.id;
                console.log('/areas/' + id + ' GET: invoked.');

                for (var i = 0; i < data.length; i++) {
                    if(data[i].id == id) {
                        response.send(data[i]);
                        return;
                    }
                };

                response.send({});
            });

            app.put('/areas/:id', function(request, response) {
                var id = request.params.id;
                var area = request.body;
                console.log('/areas/' + id + ' PUT: invoked.'); 

                 for (var i = 0; i <data.length; i++) {
                     if(data[i].id == id) {
                         data[i].name = area.name;

                         response.send(data[i]);
                         return;
                     };
                 };

            });

            app.put('/cards/:id', function(request, response) {
                var id = request.params.id;
                var item = request.body;

                console.log('/cards/' + id + ' PUT: invoked.'); 

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
        }


        self.init();
        return self;

    };


})(cards);

