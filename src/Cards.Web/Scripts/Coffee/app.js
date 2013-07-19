
(function($) {
  var cards;
  cards = window.cards = (function() {
    var self;
    self = {};
    self.Class = {};
    self.events = {
      "onReady": []
    };
    self.runEvent = function(eventName) {
      var event, _i, _len, _ref;
      _ref = self.events[eventName];
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        event = _ref[_i];
        event();
      }
    };
    self.createObject = function(objectType) {
      var event, obj;
      obj = new cards.Class[objectType]();
      for (event in self.events) {
        self.events[event].push(obj[event]);
      }
      return obj;
    };
    return self;
  })();
  $(document).ready(function() {
    cards.runEvent("onReady");
  });
})(jQuery);
