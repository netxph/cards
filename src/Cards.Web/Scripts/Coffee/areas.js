
(function(Cards, $, ko) {
  var AreaViewModel;
  AreaViewModel = (function() {

    function AreaViewModel() {}

    AreaViewModel.prototype.areas = [];

    AreaViewModel.prototype.initViewModel = function() {
      $.getJSON("/api/areas", function(data) {
        this.areas = data;
        ko.applyBindings(this);
        $("article footer div").hide();
        $("article footer a").on("click", function() {
          $(this).parent().find("div").fadeToggle();
        });
      });
    };

    AreaViewModel.prototype.init = function() {
      this.initViewModel();
      $("#new-area").hide();
      $("#new-area-button").on("click", function() {
        $("#new-area").fadeToggle();
      });
      $("#new-area button").on("click", function() {
        var card, cardJson;
        card = {};
        card.Name = $("#area-name").val();
        cardJson = JSON.stringify(card);
        $.post("/api/areas", card, function(data) {
          this.initViewModel();
          console.log(JSON.stringify(data));
        });
      });
    };

    return AreaViewModel;

  })();
  Cards.ViewModel = new AreaViewModel();
})(window.Cards = window.Cards || {}, jQuery, ko);
