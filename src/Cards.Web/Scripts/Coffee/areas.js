
(function(Cards, $, ko) {
  var AreaViewModel;
  AreaViewModel = (function() {
    var self;

    self = AreaViewModel.prototype;

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

    AreaViewModel.prototype.bindControls = function() {
      $("#new-area-button").on("click", function() {
        $("#new-area").fadeToggle();
      });
      $("#new-area button").on("click", function() {
        var card;
        card = {};
        card.Name = $("#area-name").val();
        $.post("/api/areas", card, function(data) {
          self.refresh();
        });
      });
    };

    AreaViewModel.prototype.init = function() {
      $("#new-area").hide();
      self.refresh();
      self.bindControls();
    };

    function AreaViewModel() {
      self.init();
    }

    return AreaViewModel;

  })();
  Cards.ViewModel = AreaViewModel;
})(window.Cards = window.Cards || {}, jQuery, ko);
