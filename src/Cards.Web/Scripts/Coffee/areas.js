
(function(Cards, $, ko) {
  var AreaViewModel;
  AreaViewModel = (function() {
    var self;

    self = AreaViewModel.prototype;

    AreaViewModel.prototype.newArea = ko.observable("");

    AreaViewModel.prototype.areas = ko.observableArray([]);

    AreaViewModel.prototype.initAreaControls = function() {
      $("article footer div").hide();
      $("article footer a").on("click", function() {
        $(this).parent().find("div").fadeToggle();
      });
    };

    AreaViewModel.prototype.refresh = function() {
      $.getJSON("/api/areas", function(data) {
        self.areas(data);
        self.initAreaControls();
      });
    };

    AreaViewModel.prototype.addCard = function() {
      var areaId, card, name;
      areaId = this.ID;
      name = $("*[data-areaId=6]").find("textarea").val();
      card = {};
      card.AreaID = areaId;
      card.Name = name;
      $.post("/api/cards", card, function(data) {
        self.refresh();
      });
    };

    AreaViewModel.prototype.addArea = function() {
      var area;
      area = {};
      area.Name = self.newArea();
      $.post("/api/areas", area, function(data) {
        self.refresh();
      });
      $("#new-area").fadeToggle();
      self.newArea("");
    };

    AreaViewModel.prototype.showArea = function() {
      $("#new-area").fadeToggle();
    };

    AreaViewModel.prototype.init = function() {
      $("#new-area").hide();
      self.refresh();
    };

    function AreaViewModel() {
      self.init();
    }

    return AreaViewModel;

  })();
  Cards.ViewModel = AreaViewModel;
})(window.Cards = window.Cards || {}, jQuery, ko);
