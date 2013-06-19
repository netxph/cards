
(function(Cards, $, ko) {
  var AreaViewModel;
  AreaViewModel = (function() {
    var self;

    function AreaViewModel() {}

    self = AreaViewModel;

    AreaViewModel.prototype.areas = [];

    AreaViewModel.prototype.init = function() {
      $.getJSON("/api/areas", function(data) {
        self.areas = data;
        ko.applyBindings(self);
        $("article footer div").hide();
        $("article footer a").on("click", function() {
          $(this).parent().find("div").fadeToggle();
        });
      });
    };

    return AreaViewModel;

  })();
  Cards.ViewModel = new AreaViewModel();
})(window.Cards = window.Cards || {}, jQuery, ko);
