(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.factory('Areas', function($resource, AppSettings) {
        var areasResource = $resource(AppSettings.serviceBaseUrl + 'areas');
        var areaResource = $resource(AppSettings.serviceBaseUrl + 'areas/:id');

        return {
            getAll: function() {
                return areasResource.query();
            },
            add: function(area) {
                return areasResource.save(area);
            },
            get: function(areaId) {
                return areaResource.get({id: areaId});
            }
        };
    });
})(angular);
