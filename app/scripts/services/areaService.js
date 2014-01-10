'use strict'

var cardsApp = angular.module('cardsApp');

cardsApp.factory('Areas', function($resource) {
    return $resource('http://localhost/areas', {});
});
