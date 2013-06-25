# CoffeeScript
((Cards, $, ko) ->

    $(document).ready(->
        ko.applyBindings(new Cards.ViewModel())
    ) if Cards.ViewModel?
    return
    
) window.Cards = window.Cards || {}, jQuery, ko