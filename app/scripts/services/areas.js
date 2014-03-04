(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.factory('Areas', function($resource, AppSettings) {
        var areasResource = $resource(AppSettings.serviceBaseUrl + 'areas');
        var areaResource = $resource(AppSettings.serviceBaseUrl + 'areas/:id', null, {
            'update': { method: 'PUT' }
        });

        return {
            getAll: function() {
                return areasResource.query();
            },
            add: function(area) {
                return areasResource.save(area);
            },
            get: function(areaId) {
                return areaResource.get({id: areaId});
            },
            edit: function(areaId, area) {
                return areaResource.update({id: areaId}, area);
            }
        };
    });
})(angular);

