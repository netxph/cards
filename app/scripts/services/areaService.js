'use strict';

var cardsApp = angular.module('cardsApp');

cardsApp.factory('Areas', function($http) {
    return {
        getAll: function() {
            return $http.jsonp('http://localhost/areas');
        },
    };
});
