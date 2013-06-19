# CoffeeScript
((Cards, $, ko) ->

    $(document).ready(Cards.ViewModel.init()) if Cards.ViewModel? && Cards.ViewModel.init?
    return
    
) window.Cards = window.Cards || {}, jQuery, ko