
(function(Cards, $, ko) {
  if ((Cards.ViewModel != null) && (Cards.ViewModel.init != null)) {
    $(document).ready(Cards.ViewModel.init());
  }
})(window.Cards = window.Cards || {}, jQuery, ko);
