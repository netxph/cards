var __indexOf = [].indexOf || function(item) { for (var i = 0, l = this.length; i < l; i++) { if (i in this && this[i] === item) return i; } return -1; };

(function(cards, window, $, ko) {
  cards.Class.AreasViewModel = function() {
    var self;
    self = this;
    self.rootUrl = $("meta[name=cards-baseurl]").attr("content");
    self.newArea = ko.observable("");
    self.filterString = ko.observable("");
    self.areas = ko.observableArray([]);
    self.filterCards = function() {
      self.refresh();
    };
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
    self.getNoAgeAreas = function() {
      var noAgeAreas;
      noAgeAreas = $("#areas").data("noage");
      return noAgeAreas.split(";");
    };
    self.hideBubbles = function() {
      var bubble, bubbles, noAgeAreas, _i, _len;
      noAgeAreas = self.getNoAgeAreas();
      bubbles = $(".card-bubble");
      for (_i = 0, _len = bubbles.length; _i < _len; _i++) {
        bubble = bubbles[_i];
        self.hideBubble(bubble, noAgeAreas);
      }
    };
    self.hideBubble = function(bubble, noAgeAreas) {
      var area, bubbleElement;
      bubbleElement = $(bubble);
      area = $(bubble).closest("article").find("header h1").text();
      if (__indexOf.call(noAgeAreas, area) >= 0) {
        bubbleElement.hide();
      }
    };
    self.colorizeCard = function(card, aged) {
      var area, cardElement, noAgeAreas, staleAge;
      cardElement = $(card);
      staleAge = cardElement.data("stale");
      noAgeAreas = self.getNoAgeAreas();
      area = cardElement.closest("article").find("header h1").text();
      if (staleAge > aged && __indexOf.call(noAgeAreas, area) < 0) {
        cardElement.addClass('card-aged');
      }
    };
    self.colorizeLabel = function(label) {
      var color, labelElement;
      labelElement = $(label);
      color = labelElement.data("color");
      labelElement.css("background-color", color);
    };
    self.colorize = function() {
      var aged, card, label, labels, _i, _j, _len, _len1;
      labels = $("#labels span");
      for (_i = 0, _len = labels.length; _i < _len; _i++) {
        label = labels[_i];
        self.colorizeLabel(label);
      }
      aged = $("#cards").data('aged');
      cards = $("#cards li");
      for (_j = 0, _len1 = cards.length; _j < _len1; _j++) {
        card = cards[_j];
        self.colorizeCard(card, aged);
      }
    };
    self.initControls = function() {
      self.resize();
      $().dragScroll();
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
          card.Name = $(cardElement).find("a").text();
          $.ajax({
            url: self.rootUrl + "api/cards/" + card.ID,
            type: "PUT",
            data: card
          }).done(function() {
            var target;
            target = areaElement.find("#cards");
            cardElement.removeClass('card-aged');
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
      }).on("click", "article footer a", function(event) {
        var currentArea;
        event.preventDefault();
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
      $.getJSON(self.rootUrl + "api/areas?CardLabel=" + self.filterString()).done(function(data) {
        self.areas(data);
        self.resize();
        self.colorize();
        self.hideBubbles();
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
      $.post(self.rootUrl + "api/cards", card).done(function() {
        self.refresh();
      }).fail(function() {
        self.showError("Santa can't figured out what happened, can you try it again?");
      });
    };
    self.addArea = function() {
      var area;
      area = {};
      area.Name = self.newArea();
      $.post(self.rootUrl + "api/areas", area).done(function() {
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
