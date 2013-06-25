# CoffeeScript
((Cards, $, ko) ->
    
    class AreaViewModel

        self = this.prototype

        newArea: ko.observable("")

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

        addCard: ->
            areaId = this.ID
            name = $("*[data-areaId=6]").find("textarea").val()

            card = {}
            card.AreaID = areaId
            card.Name = name

            $.post "/api/cards", card, (data) ->
                self.refresh()
                return

            return

        addArea: ->
            area = {}
            area.Name = self.newArea()

            
            $.post "/api/areas", area, (data) ->
               self.refresh()
               return

            $("#new-area").fadeToggle()
            self.newArea("")
            return

        showArea: ->
            $("#new-area").fadeToggle();
            return

        init: ->
            $("#new-area").hide()

            self.refresh()
            return
        
        constructor: ->
            self.init()        

    Cards.ViewModel = AreaViewModel
    return
) window.Cards = window.Cards || {}, jQuery, ko