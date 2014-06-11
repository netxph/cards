(function() {
    'use strict';

    describe('Service: appsettings', function () {

        var subject;

        beforeEach(module('cardsApp'));
        beforeEach(inject(function(AppSettings) {
            subject = AppSettings;
        }));

        it('should settings is defined', function() {
            expect(subject).toBeDefined();
        });

        it('should define serviceBaseUrl', function() {
            expect(subject.serviceBaseUrl).toBeDefined();
        });

        it('should define cardTemplate', function() {
            expect(subject.cardTemplate).toBeDefined();
        });

        it('should define menuTemplate', function() {
            expect(subject.menuTemplate).toBeDefined();
        });

    });
})();
