
(function(cards, $, window) {
  cards.Class.Session = function() {
    var self;
    self = this;
    self.repost = function() {
      var href;
      href = window.location.href;
      href = href.replace("/create#", "/create?");
      window.location.href = href;
    };
    self.onReady = function() {
      self.repost();
    };
  };
  cards.createObject("Session");
})(cards, jQuery, window);
