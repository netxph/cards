'use strict';

var cardsApp = angular.module('cardsApp');

cardsApp.factory('Areas', function($resource, AppSettings) {
    var areasUrl = AppSettings.serviceBaseUrl + 'areas';

    return $resource(areasUrl);
});
