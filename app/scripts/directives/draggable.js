(function(angular) {
'use strict';

var cardsApp = angular.module('cardsApp')

cardsApp.directive('cdDraggable', function () {
    
    return function(scope, element) {
        var el = element[0];

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
    };
});
})(angular);
