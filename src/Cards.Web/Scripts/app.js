(function (Cards, $, ko, undefined) {
    
    if (Cards.ViewModel) {
        $(document).ready(Cards.ViewModel.init());
    }

}(window.Cards = window.Cards || {}, jQuery, ko))