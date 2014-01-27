(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.factory('Cards', function($resource, AppSettings) {
        var cardsUrl = AppSettings.serviceBaseUrl + 'cards';

        return $resource(cardsUrl);
    });
})(angular);
