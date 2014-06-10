(function(angular) {
    'use strict';

    angular.module('cardsApp')
        .constant('AppSettings', { 

            serviceBaseUrl: 'http://localhost:9002/' ,
            cardTemplate: 'views/card-partial.html'

        });

})(angular);
