
(function(cards, $, window) {
  cards.Class.Session = function() {
    var self;
    self = this;
    self.rootUrl = $("meta[name=cards-baseurl]").attr("content");
    self.repost = function() {
      var path, query;
      query = window.location.hash.replace('#', '?');
      if (query !== '') {
        path = window.location.origin + self.rootUrl + "session/create" + query;
      } else {
        path = window.location.origin + self.rootUrl;
      }
      window.location.replace(path);
    };
    self.onReady = function() {
      self.repost();
    };
  };
  cards.createObject("Session");
})(cards, jQuery, window);
