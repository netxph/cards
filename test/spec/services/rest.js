(function(angular) {
    'use strict';

    describe('Service: Rest', function () {

        // load the service's module
        beforeEach(module('cardsApp'));

        // instantiate service
        var Rest;
        beforeEach(inject(function (_Rest_) {
            Rest = _Rest_;
        }));

        it('should be defined', function () {
            expect(!!Rest).toBe(true);
        });

    });
})(angular);
