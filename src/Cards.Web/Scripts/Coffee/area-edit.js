
(function(window, cards, ko, $) {
  cards.Class.AreaViewModel = function() {
    var self;
    self = this;
    self.rootUrl = "../../";
    self.area = {};
    self.updateArea = function() {
      var area;
      area = ko.mapping.toJS(self.area);
      $.ajax({
        url: self.rootUrl + "api/areas/" + area.ID,
        type: "PUT",
        data: area
      }).done(function() {
        return window.location.href = self.rootUrl + "areas";
      });
    };
    self.getArea = function() {
      var id;
      id = $("#area-edit").data("areaid");
      $.getJSON(self.rootUrl + 'api/areas/' + id, function(data) {
        self.area = ko.mapping.fromJS(data);
        ko.applyBindings(self);
      });
    };
    self.onReady = function() {
      self.getArea();
    };
    return self;
  };
  cards.createObject("AreaViewModel");
})(window, cards, ko, jQuery);
