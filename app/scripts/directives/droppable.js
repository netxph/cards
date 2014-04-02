(function(angular) {
    'use strict';

    var cardsApp = angular.module('cardsApp');

    cardsApp.directive('cdDroppable', function () {
        return {
            scope: {
                drop: '&',
            },
            link: function(scope, element) {
            // again we need the native object
                var el = element[0];

                el.addEventListener(
                    'dragover',
                    function(e) {
                        e.dataTransfer.dropEffect = 'move';
                    // allows us to drop
                        if (e.preventDefault) e.preventDefault();
                        this.classList.add('droppable-item');
                        return false;
                    },
                   false
                );

                el.addEventListener(
                    'dragenter',
                    function(e) {
                        this.classList.add('droppable-item');
                        return false;
                    },
                    false
                );

                el.addEventListener(
                    'dragleave',
                    function(e) {
                        this.classList.remove('droppable-item');
                        return false;
                    },
                    false
                );

                el.addEventListener(
                    'drop',
                    function(e) {
                        // Stops some browsers from redirecting.
                        if (e.stopPropagation) e.stopPropagation();

                        this.classList.remove('droppable-item');

                        var item = document.getElementById(e.dataTransfer.getData('Text'));

                        var areaId = this.attributes["data-areaid"].value;
                        var cardId = item.attributes["data-cardid"].value;

                        this.appendChild(item);
                        // call the passed drop function
                        scope.$apply(function(scope) {
                            var fn = scope.drop();
                            if ('undefined' !== typeof fn) {            
                                fn(cardId, areaId);
                            }
                        });

                        return false;
                    },
                    false
                );
            }
        }
    });
})(angular);
