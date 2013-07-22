# CoffeeScript
((cards, $, ko) ->
    
    cards.Class.CardViewModel = ->
        self = this

        self.rootUrl = "../../"

        self.card = {}

        self.showError = (message) ->
            $("#error-modal").show()
            $("#error-modal span").text message
            return

        self.initControls = ->
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

        self.updateCard = ->
            card = ko.mapping.toJS(self.card)

            $.ajax(
                url: self.rootUrl + "api/cards/" + card.ID,
                type: "PUT",
                data: card)
                .done ->
                    window.location.href = self.rootUrl + "areas"        
            
            return

        self.getCard = ->
            id = $(".edit-section").data("cardid")
            
            $.getJSON(self.rootUrl + 'api/cards/' + id)
                .done (data) ->
                    self.card = ko.mapping.fromJS(data)
                    ko.applyBindings self
                    return
                .fail ->
                    self.showError "Santa can't figured out what happened, can you try it again"
                    return

            return

        self.onReady = ->
            self.initControls()
            self.getCard()
            
            return

        return self
    
    cards.createObject("CardViewModel")
    return
) cards, jQuery, ko