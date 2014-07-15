(function(angular) {
    'use strict';

    describe('Service: Session', function () {

        beforeEach(module('cardsApp'));

        var subject;

        beforeEach(inject(function (_Session_, $window) {
            subject = _Session_;
            delete $window.sessionStorage.user;
        }));

        it('should not be null', function() {
            expect(subject).not.toBeNull();
        });

        it('should define isAuthenticated', function() {
            expect(subject.isAuthenticated).toEqual(jasmine.any(Function));
        });

        it('should isAuthenticated initialized to false', function() {
            expect(subject.isAuthenticated()).toBe(false);
        });

        it('should define userName', function() {
            expect(subject.getCurrentUser).toEqual(jasmine.any(Function));
        });

        it('should userName initialize to empty', function() {
            expect(subject.getCurrentUser()).toBe(null);
        });

        it('should define destroy', function() {
            expect(subject.destroy).toEqual(jasmine.any(Function));
        });

        it('should not be authenticated when destroyed', function() {
            subject.create('me@cards.com');
            subject.destroy();

            expect(subject.isAuthenticated()).toBe(false);
        });

    });
})(angular)
