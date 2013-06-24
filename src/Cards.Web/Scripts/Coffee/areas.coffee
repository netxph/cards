# CoffeeScript
((Cards, $, ko) ->
    
    class AreaViewModel
        areas: []
        initViewModel: ->
            $.getJSON "/api/areas", (data) ->
                this.areas = data
                ko.applyBindings this

                $("article footer div").hide()
                $("article footer a").on "click", ->
                    $(this).parent().find("div").fadeToggle()
                    return

                return
            return
        init: ->
            this.initViewModel()

            $("#new-area").hide()
            $("#new-area-button").on "click", ->
                $("#new-area").fadeToggle();
                return

            $("#new-area button").on "click", ->
                card = {}
                card.Name = $("#area-name").val()

                cardJson = JSON.stringify card

                $.post "/api/areas", card, (data) ->
                    this.initViewModel()
                    console.log JSON.stringify(data)
                    return

                return

            return

    Cards.ViewModel = new AreaViewModel()
    return
) window.Cards = window.Cards || {}, jQuery, ko