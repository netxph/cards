(function(angular) {
    var cardMock = angular.module('cardMock', []);

    cardMock.value('cardData', {
        areaID: 1,
        id: 1,
        name: 'create todo #bug',
        description: 'description goes here',
        assignedTo: 'me@cards.com'
    });
})(angular);
