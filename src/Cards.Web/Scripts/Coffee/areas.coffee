# CoffeeScript
((Cards, $, ko) ->
    
    class AreaViewModel

        self = this.prototype

        newArea: ko.observable("")

        areas: ko.observableArray([])

        resize: ->
            windowWidth = $(window).width()

            areaCount = $("#areas article").length
            width = ($("#areas article").outerWidth() + 12) * areaCount

            if windowWidth < width
                $("body").width(width)
            else
                $("body").width(windowWidth)

            return

        initAreaControls: ->
            self.resize()

            #transfer these items in jquery live

            $("#areas article").on "dragover", (event) ->
                event.preventDefault()
                return

            $("#areas article").on "drop", (event) ->
                event.preventDefault()
                sourceAreaId = parseInt(event.originalEvent.dataTransfer.getData("AreaID"))
                cardId = event.originalEvent.dataTransfer.getData("CardID")
                cardElement =  $("*[data-cardid=" + cardId + "]")
                areaElement = $(event.target).closest("#areas article")
                areaId = $(areaElement).data("areaid")

                if sourceAreaId != areaId
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
                areaId = $(event.target).closest("#areas article").data("areaid")
                cardId = $(event.target).data("cardid")
                event.originalEvent.dataTransfer.setData("AreaID", areaId)
                event.originalEvent.dataTransfer.setData("CardID", cardId)
                return

            $("#areas article footer div").hide()
            $("#areas article footer a").on "click", ->
                articles = $("#areas article")
                for article of articles then do (article) ->
                    $(article).find("div").hide()
                    return

                currentArea = $(this).parent()
                currentArea.find("div").fadeToggle()
                currentArea.find("textarea").focus()
                
                return

            $("#areas textarea").on "keypress", (event) ->
                if event.which == 13
                    return false
                
                return true

            $("#areas textarea").on "keyup", (event) ->
                if event.which == 13
                    event.preventDefault()
                    $(this).closest("#areas article").find("button").click()

                return
                
            $("#areas textarea").on "focusout", (event) ->
                $(this).closest("article").find("div").fadeOut()
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

        addCard: (item, event) ->
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
            $("#new-area input[type=text]").focus()

            return

        init: ->
            $("#new-area").hide()
            $("#error-modal").hide()
            
            $("#new-area input[type=text]").on "keyup", (event) ->
                event.preventDefault()
                if event.which == 13
                    $("#new-area button").click()
                return

            $(window).resize (event) ->
                self.resize()
                return

            $("#error-modal").live "click", (event) ->
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