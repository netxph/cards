(function(angular) {
    'use strict';

    describe('Filter: cardFilter', function () {

        // load the filter's module
        beforeEach(module('cardsApp'));

        // initialize a new instance of the filter before each test
        var cardFilter;
        beforeEach(inject(function ($filter) {
            cardFilter = $filter('cardFilter');
        }));

        it('should return input when empty', function () {
            var cards = [{ labels: [ 'Bug' ] }];
            var search = { label: '' };

            expect(cardFilter(cards, search).length).toBe(1);
        });

        it('should return input when not match', function() {
            var cards = [{ labels: [ 'Bug' ] }];
            var search = { label: 'X' };

            expect(cardFilter(cards, search).length).toBe(0);
        });

        it('should return input when match', function() {
            var cards = [{ labels: [ 'Bug' ] }];
            var search = { label: 'Bug' };

            expect(cardFilter(cards, search).length).toBe(1);
        });

        it('should return input when not match case', function() {
            var cards = [{ labels: [ 'Bug' ] }];
            var search = { label: 'bug' };

            expect(cardFilter(cards, search).length).toBe(1);
        });

        it('should return input when partial', function() {
            var cards = [{ labels: [ 'Bug' ] }];
            var search = { label: 'bu' };

            expect(cardFilter(cards, search).length).toBe(1);
        });

        it('should return input when from many labels', function() {
            var cards = [{ labels: [ 'Feature', 'Bug', 'Test' ] }];
            var search = { label: 'bu' };

            expect(cardFilter(cards, search).length).toBe(1);
        });


    });
})(angular);
