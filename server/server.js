'use strict';

(function() {
  var express, app, server;

  express = require('express');
  app = module.exports = express();

  server = {
    data: [],
    seed: function() {
      server.data.push({
            name: 'Backlog',
          cards: [
        {
          name: 'Create a design template for cards',
          description: 'description goes here',
          labels: ['Bug'],
        }]
          });
    },
    start: function() {
      server.seed();

      app.use(function(request, response, next) {
        response.header('Access-Control-Allow-Origin', ['*'])
        response.header('Access-Control-Allow-Methods', 'GET,PUT,POST,DELETE');
      response.header('Access-Control-Allow-Headers', 'Content-Type');

      next();
      });

      app.get('/areas', function(request, response) {
        response.send(server.data);
      });
    }
  };
  
  server.start();

})();
