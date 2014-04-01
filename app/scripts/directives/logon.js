(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp')
    
    cardsApp.directive('cdLogon', ['State', function (State) {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                var signIn = '<a href="#/session/new">Not Signed In</a>';
                var signOut = '<a href="#/session/delete">Sign Out</a>'; 
                element.html(signIn);

                scope.$on('auth_changed', function() {
                    var userName = scope.getUserName();

                    if(userName == '') {
                        element.html(signIn);
                    } else {
                        element.html(signOut);
                    }
                });

                return element;
            },
            controller: 'LoginCtrl'
        };
    }]);
})(angular);
