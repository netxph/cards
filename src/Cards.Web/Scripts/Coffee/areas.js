
(function(Cards, $, ko) {
  var AreaViewModel;
  AreaViewModel = (function() {
    var self;

    self = AreaViewModel.prototype;

    AreaViewModel.prototype.newArea = ko.observable("");

    AreaViewModel.prototype.areas = ko.observableArray([]);

    AreaViewModel.prototype.initAreaControls = function() {
      $("#areas article").on("dragover", function(event) {
        event.preventDefault();
      });
      $("#areas article").on("drop", function(event) {
        var areaElement, areaId, card, cardElement, cardId;
        event.preventDefault();
        cardId = event.originalEvent.dataTransfer.getData("CardID", cardId);
        cardElement = $("*[data-cardid=" + cardId + "]");
        areaElement = $(event.target).closest("#areas article");
        areaId = $(areaElement).data("areaid");
        card = {};
        card.ID = cardId;
        card.AreaID = areaId;
        card.Name = $(cardElement).text();
        $.ajax({
          url: "api/cards/" + card.ID,
          type: "PUT",
          data: card,
          success: function() {
            var target;
            target = areaElement.find("ul");
            target.append(cardElement);
          }
        });
      });
      $("#areas li").on("dragstart", function(event) {
        var cardId;
        cardId = $(event.target).data("cardid");
        event.originalEvent.dataTransfer.setData("CardID", cardId);
      });
      $("article footer div").hide();
      $("article footer a").on("click", function() {
        $(this).parent().find("div").fadeToggle();
      });
    };

    AreaViewModel.prototype.refresh = function() {
      $.getJSON("api/areas", function(data) {
        self.areas(data);
        self.initAreaControls();
      });
    };

    AreaViewModel.prototype.addCard = function() {
      var areaId, card, name;
      areaId = this.ID;
      name = $("*[data-areaid=" + areaId + "]").find("textarea").val();
      card = {};
      card.AreaID = areaId;
      card.Name = name;
      $.post("api/cards", card, function(data) {
        self.refresh();
      });
    };

    AreaViewModel.prototype.addArea = function() {
      var area;
      area = {};
      area.Name = self.newArea();
      $.post("api/areas", area, function(data) {
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
      $("body").on({
        ajaxStart: function() {
          $(this).addClass("loading");
        },
        ajaxStop: function() {
          $(this).removeClass("loading");
        }
      });
      self.refresh();
    };

    function AreaViewModel() {
      self.init();
    }

    return AreaViewModel;

  })();
  Cards.ViewModel = AreaViewModel;
})(window.Cards = window.Cards || {}, jQuery, ko);
