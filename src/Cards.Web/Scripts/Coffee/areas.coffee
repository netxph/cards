# CoffeeScript
((Cards, $, ko) ->
    
    class AreaViewModel

        self = this.prototype

        areas: ko.observableArray([])

        initAreaControls: ->
            $("article footer div").hide()
            $("article footer a").on "click", ->
                $(this).parent().find("div").fadeToggle()
                return
            return

        refresh: ->
            $.getJSON "/api/areas", (data) ->
                self.areas(data)
                self.initAreaControls()
                return
            return

        bindControls: ->
            $("#new-area-button").on "click", ->
                $("#new-area").fadeToggle();
                return

            $("#new-area button").on "click", ->
                card = {}
                card.Name = $("#area-name").val()

                $.post "/api/areas", card, (data) ->
                    self.refresh()
                    return

                return
            return

        init: ->
            $("#new-area").hide()

            self.refresh()
            self.bindControls()
            return
        
        constructor: ->
            self.init()        

    Cards.ViewModel = AreaViewModel
    return
) window.Cards = window.Cards || {}, jQuery, ko