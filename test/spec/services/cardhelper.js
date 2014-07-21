(function(angular) {
    'use strict';

    describe('Service: CardHelper', function () {

        // load the service's module
        beforeEach(module('cardsApp'));

        // instantiate service
        var CardHelper;
        beforeEach(inject(function (_CardHelper_) {
            CardHelper = _CardHelper_;
        }));

        it('should define CardHelper', function () {
            expect(!!CardHelper).toBe(true);
        });

        it('should define CardHelper.GetLabels()', function() {
            expect(CardHelper.getLabels).toEqual(jasmine.any(Function));
        });

        it('should GetLabels() return empty', function() {
            var labels = CardHelper.getLabels();
            expect(labels.length).toBe(0);
        });

        it('should GetLabels() return a value', function() {
            var labels = CardHelper.getLabels('test #test');
            expect(labels.length).toBe(1);
        });

        it('should GetLabels() first item has value', function() {
            var labels = CardHelper.getLabels('test #test');
            expect(labels[0]).toBe('test');
        });

        it('should GetLabels() return 2 values', function() {
            var labels = CardHelper.getLabels('test #test the #other'); 
            expect(labels.length).toBe(2);
        });

        it('should GetLabels() second item has value', function() {
            var labels = CardHelper.getLabels('test #test the #other');
            expect(labels[1]).toBe('other');
        });

    });
})(angular);
