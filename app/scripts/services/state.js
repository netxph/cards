(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');
    
    cardsApp.service('State', function State() {
        var self = this;

        self.userName = '';
    });

})(angular);
