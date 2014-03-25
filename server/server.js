var cards;
cards = global.cards = {};

(function(cards) {
    'use strict';

    cards.Server = function() {
        var self = this;
        var loggedIn = false;

        var provider = require('./provider.js');
        provider.seed();

        function restrict(request, response, next) {
            if(loggedIn) {
                next();
            } else {
                response.send('Access denied!', 401);
            }
        }

        function initRoutes(app) {

            app.post('/session', function(request, response) {
                console.log('POST: /session');
                var session = request.body;

                if(session.password == 'P@ssw0rd!1' && session.userId == 'me@cards.com') {
                    loggedIn = true;

                    console.log('AUTH: ' + session.userId);
                    response.send(session.userId);
                } else {
                    response.send('Authentication failed!', 403);
                }
            });

            app.get('/areas', restrict, function(request, response) {
                console.log('GET: /areas');
                response.send(provider.getAreas());
            });

            app.get('/cards/:id', restrict, function(request, response) {
                var id = request.params.id;
                console.log('GET: /cards/' + id);

                var card = provider.getCard(id);
                response.send(card);
            });

            app.get('/areas/:id', restrict, function(request, response) {
                var id = request.params.id;
                console.log('GET: /areas/' + id);

                var area = provider.getArea(id); 
                response.send(area);
            });

            app.put('/areas/:id', restrict, function(request, response) {
                var id = request.params.id;
                var area = request.body;
                console.log('PUT: /areas/' + id); 

                area = provider.editArea(id, area);
                response.send(area); 
            });

            app.put('/cards/:id', restrict, function(request, response) {

                var id = request.params.id;
                var card = request.body;

                console.log('PUT: /cards/' + id); 

                card = provider.editCard(id, card); 

                response.send(card);
            });

            app.post('/areas', restrict, function(request, response) {
                console.log('POST: /areas');

                var area = request.body;
                console.log('DATA: ' + area);

                area = provider.addArea(area); 

                response.send(area);
            });

            app.post('/cards', restrict, function(request, response) {
                console.log('POST: /cards');

                var card = request.body;
                console.log('DATA: ' + card);

                card = provider.addCard(card); 

                response.send(card);
            });
        }

        self.listen = function(port) {
            var express = require('express');
            var app = express();

            app.use(express.cookieParser());

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

