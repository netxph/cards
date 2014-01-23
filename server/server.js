var cards;
cards = global.cards = {};

(function(cards) {
  'use strict';

  cards.Server = function(app, data) {
    var self = this;

    self.seed = function() {
      data.push({
            name: 'Backlog',
          cards: [
        {
          name: 'Create a design template for cards',
          description: 'description goes here',
          labels: ['Bug'],
        }]
          });
    };

    self.init = function() {
      app.use(function(request, response, next) {
        response.header('Access-Control-Allow-Origin', ['*'])
        response.header('Access-Control-Allow-Methods', 'GET,PUT,POST,DELETE');
        response.header('Access-Control-Allow-Headers', 'Content-Type');

        next();
      });

      app.get('/areas', function(request, response) {
        response.send(data);
      });
    };

  };

  if(!cards.data) {
    var express;
    var app;

    express = require('express');
    app = module.exports = express();

    var data = cards.data = [];
    var server = new cards.Server(app, data);
    server.seed();
    server.init();
  }
  
})(cards);

