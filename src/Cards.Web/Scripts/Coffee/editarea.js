
(function(window, cards, ko, $) {
  cards.Class.AreaViewModel = function() {
    var self;
    self = this;
    self.rootUrl = $("meta[name=cards-baseurl]").attr("content");
    self.area = {};
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
    self.deleteArea = function() {
      var area;
      area = ko.mapping.toJS(self.area);
      $.ajax({
        url: self.rootUrl + "api/areas/" + area.ID,
        type: "DELETE"
      }).done(function() {
        window.location.href = self.rootUrl + "areas";
      }).fail(function() {
        self.showError("Santa can't figured out what happened, can you try it again");
      });
    };
    self.updateArea = function() {
      var area;
      area = ko.mapping.toJS(self.area);
      $.ajax({
        url: self.rootUrl + "api/areas/" + area.ID,
        type: "PUT",
        data: area
      }).done(function() {
        window.location.href = self.rootUrl + "areas";
      }).fail(function() {
        self.showError("Santa can't figured out what happened, can you try it again");
      });
    };
    self.getArea = function() {
      var id;
      id = $(".edit-section").data("areaid");
      $.getJSON(self.rootUrl + 'api/areas/' + id).done(function(data) {
        self.area = ko.mapping.fromJS(data);
        ko.applyBindings(self);
      }).fail(function() {
        self.showError("Santa can't figured out what happened, can you try it again");
      });
    };
    self.onReady = function() {
      self.initControls();
      self.getArea();
    };
    return self;
  };
  cards.createObject("AreaViewModel");
})(window, cards, ko, jQuery);
