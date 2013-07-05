# CoffeeScript
((Cards, $, ko) ->
    
    class AreaViewModel

        self = this.prototype

        newArea: ko.observable("")

        areas: ko.observableArray([])

        initAreaControls: ->
            $("#areas article").on "dragover", (event) ->
                event.preventDefault()
                return

            $("#areas article").on "drop", (event) ->
                event.preventDefault()
                cardId = event.originalEvent.dataTransfer.getData("CardID", cardId)
                cardElement =  $("*[data-cardid=" + cardId + "]")
                areaElement = $(event.target).closest("#areas article")
                areaId = $(areaElement).data("areaid")

                card = {}
                card.ID = cardId
                card.AreaID = areaId
                card.Name = $(cardElement).text()
                
                $.ajax(
                    url: "api/cards/" + card.ID,
                    type: "PUT",
                    data: card)
                    .done ->
                        target = areaElement.find("ul")
                        target.append(cardElement)        
                        return
                    .fail ->
                        self.showError "Santa can't figured out what happened, can you try it again?"
                        return
                
                return

            $("#areas li").on "dragstart", (event) ->
                cardId = $(event.target).data("cardid")
                event.originalEvent.dataTransfer.setData("CardID", cardId)
                return

            $("article footer div").hide()
            $("article footer a").on "click", ->
                articles = $("#areas article")
                for article of articles then do (article) ->
                    $(article).find("div").hide()
                    return

                $(this).parent().find("div").fadeToggle()
                return
            return

        refresh: ->
            $.getJSON("api/areas")
                .done (data) ->
                    self.areas(data)
                    self.initAreaControls()
                    return
                .fail ->
                    self.showError "Santa can't figured out what happened, can you try it again?"
                    return

            return

        showError: (message) ->
            $("#error-modal").show()
            $("#error-modal span").text message
            return

        addCard: ->
            areaId = this.ID
            name = $("*[data-areaid=" + areaId + "]").find("textarea").val()

            card = {}
            card.AreaID = areaId
            card.Name = name

            $.post("api/cards", card)
                .done ->
                    self.refresh()
                    return
                .fail ->
                    self.showError "Santa can't figured out what happened, can you try it again?"
                    return

            return

        addArea: ->
            area = {}
            area.Name = self.newArea()

            
            $.post("api/areas", area)
                .done ->
                    self.refresh()
                    return
                .fail ->
                    self.showError "Santa can't figured out what happened, can you try it again?"
                    return

            $("#new-area").fadeToggle()
            self.newArea("")
            return

        showArea: ->
            $("#new-area").fadeToggle()
            return

        init: ->
            $("#new-area").hide()
            $("#error-modal").hide()
            
            $("#error-modal").on "click", (event) ->
                $(this).fadeOut()
                return

            $("body").on 
                ajaxStart: ->
                    $("#error-modal").hide()

                    $(this).addClass "loading"
                    return
                ajaxStop: ->
                    $(this).removeClass "loading"
                    return
                        
            self.refresh()
            return
        
        constructor: ->
            self.init()        

    Cards.ViewModel = AreaViewModel
    return
) window.Cards = window.Cards || {}, jQuery, ko