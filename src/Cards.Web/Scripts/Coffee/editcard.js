(function() {

  (function(cards, $, ko) {
    cards.Class.CardViewModel = function() {
      var self;
      self = this;
      self.rootUrl = $("meta[name=cards-baseurl]").attr("content");
      self.card = {};
      self.showError = function(message) {
        $("#error-modal").show();
        $("#error-modal span").text(message);
      };
      self.initControls = function() {
        $("#error-modal").live("click", function(event) {
          $(this).fadeOut();
        });
        $("body").on({
          ajaxStart: function() {
            $("#error-modal").hide();
            $(this).addClass("loading");
          },
          ajaxStop: function() {
            $(this).removeClass("loading");
          }
        });
      };
      self.deleteCard = function() {
        var card;
        card = ko.mapping.toJS(self.card);
        $.ajax({
          url: self.rootUrl + "api/cards/" + card.ID,
          type: "DELETE"
        }).done(function() {
          window.location.href = self.rootUrl + "areas";
        }).fail(function() {
          self.showError("Santa can't figured out what happened, can you try it again");
        });
      };
      self.updateCard = function() {
        var card;
        card = ko.mapping.toJS(self.card);
        $.ajax({
          url: self.rootUrl + "api/cards/" + card.ID,
          type: "PUT",
          data: card
        }).done(function() {
          window.location.href = self.rootUrl + "areas";
        }).fail(function() {
          self.showError("Santa can't figured out what happened, can you try it again");
        });
      };
      self.getCard = function() {
        var id;
        id = $(".edit-section").data("cardid");
        $.getJSON(self.rootUrl + 'api/cards/' + id).done(function(data) {
          self.card = ko.mapping.fromJS(data);
          self.card.DueDateText = ko.computed({
            read: function() {
              return moment(self.card.DueDateUtc()).format('L');
            },
            write: function(value) {
              self.card.DueDateUtc(value);
            }
          });
          ko.applyBindings(self);
        }).fail(function() {
          self.showError("Santa can't figured out what happened, can you try it again");
        });
      };
      self.onReady = function() {
        self.initControls();
        self.getCard();
      };
      return self;
    };
    cards.createObject("CardViewModel");
  })(cards, jQuery, ko);

}).call(this);
