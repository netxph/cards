
(function(cards, $, ko) {
  cards.Class.LabelsViewModel = function() {
    var self;
    self = this;
    self.rootUrl = $("meta[name=cards-baseurl]").attr("content");
    self.labels = ko.observableArray([]);
    self.onReady = function() {
      ko.applyBindings(self);
      self.initialize();
      self.refresh();
    };
    self.refresh = function() {
      $.getJSON(self.rootUrl + "api/labels").done(function(data) {
        self.labels(data);
        self.colorize();
      }).fail(function() {
        self.showError("Santa can't figured out what happened, can you try it again?");
      });
    };
    self.colorizeLabel = function(label) {
      var color, labelElement;
      labelElement = $(label);
      color = labelElement.data("color");
      labelElement.css("background-color", color);
    };
    self.colorize = function() {
      var label, labels, _i, _len;
      labels = $("#labels-big span");
      for (_i = 0, _len = labels.length; _i < _len; _i++) {
        label = labels[_i];
        self.colorizeLabel(label);
      }
    };
    self.showError = function(message) {
      $("#error-modal").show();
      $("#error-modal span").text(message);
    };
    self.initialize = function() {
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
    return self;
  };
  cards.createObject("LabelsViewModel");
})(cards, jQuery, ko);
