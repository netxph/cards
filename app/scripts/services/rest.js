(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.factory('Rest', ['$rootScope', function ($rootScope) {
        return {
            invoke: function (promise, success, error) {
                $rootScope.$broadcast('ajax_start');

                if(!!promise.then) {
                    promise.then(success).catch(function(data) {
                        $rootScope.$broadcast('ajax_error');
                        if(!!error) {
                            error(data);
                        }
                    }).finally(function() {
                        $rootScope.$broadcast('ajax_end');
                    });
                }
            }
        };
    }]);

})(angular);
