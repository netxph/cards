'use strict';

var express = require('express');
var app = module.exports = express();

app.use(function(request, response, next) {
    response.header('Access-Control-Allow-Origin', ['*'])
    response.header('Access-Control-Allow-Methods', 'GET,PUT,POST,DELETE');
    response.header('Access-Control-Allow-Headers', 'Content-Type');

    next();
});

app.get('/areas', function(request, response) {
    response.send([
        {
            name: 'Backlog',
            cards: [
            {
                name: 'Create a design template for cards',
                description: 'description goes here',
                labels: ['Bug'],
            }]
        }]);
});
