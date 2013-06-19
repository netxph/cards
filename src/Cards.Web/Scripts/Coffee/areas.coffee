# CoffeeScript
((Cards, $, ko) ->
    
    class AreaViewModel
        self = this
        areas: []
        init: ->
            $.getJSON "/api/areas", (data) ->
                self.areas = data
                ko.applyBindings self

                $("article footer div").hide()
                $("article footer a").on "click", ->
                    $(this).parent().find("div").fadeToggle();
                    return
                return
            return

    Cards.ViewModel = new AreaViewModel()
    return
) window.Cards = window.Cards || {}, jQuery, ko