(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.factory('Areas', function($resource, AppSettings) {
        return {
            getAreas: function() {
                var url = AppSettings.serviceBaseUrl + 'areas';
                return $resource(url);
            },
            getArea: function() {
                var url = AppSettings.serviceBaseUrl + 'areas/:id';
                return $resource(url, { id: '@id' });
            }
        };
    });
})(angular);
