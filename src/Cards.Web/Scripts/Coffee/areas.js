
(function(Cards, $, ko) {
  var AreaViewModel;
  AreaViewModel = (function() {
    var self;

    self = AreaViewModel.prototype;

    AreaViewModel.prototype.newArea = ko.observable("");

    AreaViewModel.prototype.areas = ko.observableArray([]);

    AreaViewModel.prototype.resize = function() {
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

    AreaViewModel.prototype.initAreaControls = function() {
      self.resize();
      $("#areas article").on("dragover", function(event) {
        event.preventDefault();
      });
      $("#areas article").on("drop", function(event) {
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
      });
      $("#areas li").on("dragstart", function(event) {
        var areaId, cardId;
        areaId = $(event.target).closest("#areas article").data("areaid");
        cardId = $(event.target).data("cardid");
        event.originalEvent.dataTransfer.setData("AreaID", areaId);
        event.originalEvent.dataTransfer.setData("CardID", cardId);
      });
      $("#areas article footer div").hide();
      $("#areas article footer a").on("click", function() {
        var article, articles, currentArea, _fn;
        articles = $("#areas article");
        _fn = function(article) {
          $(article).find("div").hide();
        };
        for (article in articles) {
          _fn(article);
        }
        currentArea = $(this).parent();
        currentArea.find("div").fadeToggle();
        currentArea.find("textarea").focus();
      });
      $("#areas textarea").on("keypress", function(event) {
        if (event.which === 13) {
          return false;
        }
        return true;
      });
      $("#areas textarea").on("keyup", function(event) {
        if (event.which === 13) {
          event.preventDefault();
          $(this).closest("#areas article").find("button").click();
        }
      });
      $("#areas textarea").on("focusout", function(event) {
        $(this).closest("article").find("div").fadeOut();
      });
    };

    AreaViewModel.prototype.refresh = function() {
      $.getJSON("api/areas").done(function(data) {
        self.areas(data);
        self.initAreaControls();
      }).fail(function() {
        self.showError("Santa can't figured out what happened, can you try it again?");
      });
    };

    AreaViewModel.prototype.showError = function(message) {
      $("#error-modal").show();
      $("#error-modal span").text(message);
    };

    AreaViewModel.prototype.addCard = function(item, event) {
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

    AreaViewModel.prototype.addArea = function() {
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

    AreaViewModel.prototype.showArea = function() {
      $("#new-area").fadeToggle();
      $("#new-area input[type=text]").focus();
    };

    AreaViewModel.prototype.init = function() {
      $("#new-area").hide();
      $("#error-modal").hide();
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
      self.refresh();
    };

    function AreaViewModel() {
      self.init();
    }

    return AreaViewModel;

  })();
  Cards.ViewModel = AreaViewModel;
})(window.Cards = window.Cards || {}, jQuery, ko);
