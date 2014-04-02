(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp')

    cardsApp.directive('cdDraggable', function () {

        function link(scope, element) {
            var el = element[0];
            var enabled = true;

            if(scope.enabled != undefined) {
                enabled = scope.enabled == 'true';
            };

            if(enabled) {

                el.draggable = true;
    
                el.addEventListener(
                    'dragstart',
                    function(e) {
                        e.dataTransfer.effectAllowed = 'move';
                        e.dataTransfer.setData('Text', this.id);
                        this.classList.add('draggable-item');
                        return false;
                    },
                    false
                );

                el.addEventListener(
                    'dragend',
                    function(e) {
                        this.classList.remove('draggable-item');
                        return false;
                    },
                    false
                );
            } else {
                e.addEventListener(
                    'mouseDown',
                    function(e) {
                        e.preventDefault();
                    },
                    false
                );
            };
        };

        return {
            scope: {
                enabled: '=cdDraggable' 
            },
            link: link
        };
    });

})(angular);
