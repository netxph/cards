(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp')
    
    cardsApp.directive('cdLogon', ['State', function (State) {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                var signIn = '<a href="#/session/new" class="navbar-link link-unstyled">Not Signed In</a>';
                var signOut = '<a href="#/session/delete" class="navbar-link link-unstyled">Sign Out</a>'; 
                element.html(signIn);

                scope.$on('auth_changed', function() {
                    var userName = scope.getUserName();

                    if(userName == '') {
                        element.html(signIn);
                    } else {
                        element.html('Hello ' + userName + '. ' + signOut);
                    }
                });

                return element;
            },
            controller: 'LoginCtrl'
        };
    }]);
})(angular);
