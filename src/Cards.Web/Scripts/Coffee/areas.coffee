# CoffeeScript
((cards, window, $, ko) ->
    
    cards.Class.AreasViewModel = ->

        self = this

        self.rootUrl = $("meta[name=cards-baseurl]").attr("content")

        self.newArea = ko.observable("")
        self.areas = ko.observableArray([])

        self.resize = ->
            windowWidth = $(window).width()

            areaCount = $("#areas article").length
            width = ($("#areas article").outerWidth() + 12) * areaCount

            if windowWidth < width
                $("body").width(width)
            else
                $("body").width(windowWidth)

            return

        self.initControls = ->
            self.resize()

            $("#areas")
                .on "dragover", "article", (event) ->
                    event.preventDefault()
                    return
                .on "drop", "article", (event) ->
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
                        card.Name = $(cardElement).find("a").text()
                
                        $.ajax(
                            url: self.rootUrl + "api/cards/" + card.ID,
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
                .on "dragstart", "article", (event) ->
                    areaId = $(event.target).closest("#areas article").data("areaid")
                    cardId = $(event.target).closest("li").data("cardid")
                    event.originalEvent.dataTransfer.setData("AreaID", areaId)
                    event.originalEvent.dataTransfer.setData("CardID", cardId)
                    return
                .on "click", "article footer a", ->
                    currentArea = $(this).parent().find("div")

                    if !currentArea.is(":visible")
                        currentArea.fadeToggle()
                        currentArea.find("textarea").focus()
                
                    return
                .on "keypress", "textarea", (event) ->
                    if event.which == 13
                        return false
                
                    return true
                .on "keyup", "textarea", (event) ->
                    if event.which == 13
                        event.preventDefault()
                        $(this).closest("#areas article").find("button").click()

                    return
                .on "focusout", "textarea", (event) ->
                    $(this).closest("article").find("div").fadeOut()
                    return


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
                        

            return

        self.refresh = ->
            $.getJSON(self.rootUrl + "api/areas")
                .done (data) ->
                    self.areas(data)
                    self.resize()
                    
                    return
                .fail ->
                    self.showError "Santa can't figured out what happened, can you try it again?"
                    return

            return

        self.showError = (message) ->
            $("#error-modal").show()
            $("#error-modal span").text message
            return

        self.addCard = ->
            areaId = this.ID
            name = $("*[data-areaid=" + areaId + "]").find("textarea").val()

            card = {}
            card.AreaID = areaId
            card.Name = name

            $.post(self.rootUrl + "api/cards", card)
                .done ->
                    self.refresh()
                    return
                .fail ->
                    self.showError "Santa can't figured out what happened, can you try it again?"
                    return

            return

        self.addArea = ->
            area = {}
            area.Name = self.newArea()

            
            $.post(self.rootUrl + "api/areas", area)
                .done ->
                    self.refresh()
                    return
                .fail ->
                    self.showError "Santa can't figured out what happened, can you try it again?"
                    return

            $("#new-area").fadeToggle()
            self.newArea("")
            return

        self.showArea = ->
            $("#new-area").fadeToggle()
            $("#new-area input[type=text]").focus()

            return

        self.onReady = ->
            ko.applyBindings self

            self.initControls()


            self.refresh()
            return
        return self

    cards.createObject "AreasViewModel"
    return
    
) window.cards, window, jQuery, ko