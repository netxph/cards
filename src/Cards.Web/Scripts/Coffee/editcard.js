
(function(cards, $, ko) {
  cards.Class.CardViewModel = function() {
    var self;
    self = this;
    self.rootUrl = "../../";
    self.card = {};
    self.updateCard = function() {
      var card;
      card = ko.mapping.toJS(self.card);
      $.ajax({
        url: self.rootUrl + "api/cards/" + card.ID,
        type: "PUT",
        data: card
      }).done(function() {
        return window.location.href = self.rootUrl + "areas";
      });
    };
    self.getCard = function() {
      var id;
      id = $(".edit-section").data("cardid");
      $.getJSON(self.rootUrl + 'api/cards/' + id, function(data) {
        self.card = ko.mapping.fromJS(data);
        ko.applyBindings(self);
      });
    };
    self.onReady = function() {
      self.getCard();
    };
    return self;
  };
  cards.createObject("CardViewModel");
})(cards, jQuery, ko);
