(function() {
    'use strict';

    describe('Service: State', function () {

        var subject;

        beforeEach(module('cardsApp'));

        beforeEach(inject(function (_State_) {
            subject = _State_;
        }));

        it('should service is defined', function () {
            expect(subject).toBeDefined();
        });

        it('should user name is defined', function() {
            expect(subject.userName).toBeDefined();
        });

        it('should user name is empty', function() {
            expect(subject.userName).toBe('');
        });

    });
})();
