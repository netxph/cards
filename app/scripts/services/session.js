(function(angular) {
'use strict';

    var cardsApp = angular.module('cardsApp');
    
    cardsApp.factory('Session', ['$resource', 'AppSettings', function($resource, AppSettings) {
        var sessionResource = $resource(AppSettings.serviceBaseUrl + 'session');  

        return {
            add: function(session) {
                return sessionResource.save(session);
            }
        };
    }]);

})(angular);
