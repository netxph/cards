
(function(cards, window, $, ko) {
  cards.Class.AreasViewModel = function() {
    var self;
    self = this;
    self.newArea = ko.observable("");
    self.areas = ko.observableArray([]);
    self.resize = function() {
      var areaCount, width, windowWidth;
      windowWidth = $(window).width();
      areaCount = $("#areas article").length;
      width = ($("#areas article").outerWidth() + 12) * areaCount;
      if (windowWidth < width) {
        $("body").width(width);
      } else {
        $("body").width(windowWidth);
      }
    };
    self.initControls = function() {
      self.resize();
      $("#areas").on("dragover", "article", function(event) {
        event.preventDefault();
      }).on("drop", "article", function(event) {
        var areaElement, areaId, card, cardElement, cardId, sourceAreaId;
        event.preventDefault();
        sourceAreaId = parseInt(event.originalEvent.dataTransfer.getData("AreaID"));
        cardId = event.originalEvent.dataTransfer.getData("CardID");
        cardElement = $("*[data-cardid=" + cardId + "]");
        areaElement = $(event.target).closest("#areas article");
        areaId = $(areaElement).data("areaid");
        if (sourceAreaId !== areaId) {
          card = {};
          card.ID = cardId;
          card.AreaID = areaId;
          card.Name = $(cardElement).text();
          $.ajax({
            url: "api/cards/" + card.ID,
            type: "PUT",
            data: card
          }).done(function() {
            var target;
            target = areaElement.find("ul");
            target.append(cardElement);
          }).fail(function() {
            self.showError("Santa can't figured out what happened, can you try it again?");
          });
        }
      }).on("dragstart", "article", function(event) {
        var areaId, cardId;
        areaId = $(event.target).closest("#areas article").data("areaid");
        cardId = $(event.target).closest("li").data("cardid");
        event.originalEvent.dataTransfer.setData("AreaID", areaId);
        event.originalEvent.dataTransfer.setData("CardID", cardId);
      }).on("click", "article footer a", function() {
        var currentArea;
        currentArea = $(this).parent().find("div");
        if (!currentArea.is(":visible")) {
          currentArea.fadeToggle();
          currentArea.find("textarea").focus();
        }
      }).on("keypress", "textarea", function(event) {
        if (event.which === 13) {
          return false;
        }
        return true;
      }).on("keyup", "textarea", function(event) {
        if (event.which === 13) {
          event.preventDefault();
          $(this).closest("#areas article").find("button").click();
        }
      }).on("focusout", "textarea", function(event) {
        $(this).closest("article").find("div").fadeOut();
      });
      $("#new-area input[type=text]").on("keyup", function(event) {
        event.preventDefault();
        if (event.which === 13) {
          $("#new-area button").click();
        }
      });
      $(window).resize(function(event) {
        self.resize();
      });
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
    self.refresh = function() {
      $.getJSON("api/areas").done(function(data) {
        self.areas(data);
        self.resize();
      }).fail(function() {
        self.showError("Santa can't figured out what happened, can you try it again?");
      });
    };
    self.showError = function(message) {
      $("#error-modal").show();
      $("#error-modal span").text(message);
    };
    self.addCard = function() {
      var areaId, card, name;
      areaId = this.ID;
      name = $("*[data-areaid=" + areaId + "]").find("textarea").val();
      card = {};
      card.AreaID = areaId;
      card.Name = name;
      $.post("api/cards", card).done(function() {
        self.refresh();
      }).fail(function() {
        self.showError("Santa can't figured out what happened, can you try it again?");
      });
    };
    self.addArea = function() {
      var area;
      area = {};
      area.Name = self.newArea();
      $.post("api/areas", area).done(function() {
        self.refresh();
      }).fail(function() {
        self.showError("Santa can't figured out what happened, can you try it again?");
      });
      $("#new-area").fadeToggle();
      self.newArea("");
    };
    self.showArea = function() {
      $("#new-area").fadeToggle();
      $("#new-area input[type=text]").focus();
    };
    self.onReady = function() {
      ko.applyBindings(self);
      self.initControls();
      self.refresh();
    };
    return self;
  };
  cards.createObject("AreasViewModel");
})(window.cards, window, jQuery, ko);
