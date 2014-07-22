(function(angular) {
    'use strict';

    describe('Controller: SearchCtrl', function () {

        beforeEach(module('cardsApp'));

        var SearchCtrl,
        scope,
        root;

        beforeEach(inject(function ($controller, $rootScope) {
            root = $rootScope;
            scope = $rootScope.$new();
            SearchCtrl = $controller('SearchCtrl', {
                $scope: scope
            });
        }));

        it('should define controller', function() {
            expect(!!SearchCtrl).toBeTruthy();
        });

        it('should define scope.text', function() {
            expect(scope.search.text).toBeDefined();
        });

        it('should scope.text is empty', function() {
            expect(scope.search.text).toBe('');
        });

        it('should define scope.performSearch', function() {
            expect(scope.performSearch).toEqual(jasmine.any(Function));
        });

        it('should emit onSearch', function() {
            spyOn(root, '$emit');

            scope.performSearch('test');

            expect(root.$emit).toHaveBeenCalledWith('onFilterCards', 'test');
        });

    });
})(angular);
