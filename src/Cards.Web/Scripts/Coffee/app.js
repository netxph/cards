
(function(Cards, $, ko) {
  if (Cards.ViewModel != null) {
    $(document).ready(function() {
      return ko.applyBindings(new Cards.ViewModel());
    });
  }
})(window.Cards = window.Cards || {}, jQuery, ko);
