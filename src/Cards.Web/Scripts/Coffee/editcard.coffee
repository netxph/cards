# CoffeeScript
((cards, $, ko) ->
    
    cards.Class.CardViewModel = ->
        self = this

        self.rootUrl = "../../"

        self.card = {}

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
            id = $("#card-edit").data("cardid")
            
            $.getJSON self.rootUrl + 'api/cards/' + id, (data) ->
                self.card = ko.mapping.fromJS(data)
                ko.applyBindings self
                return

            return

        self.onReady = ->
            self.getCard()
            
            return

        return self
    
    cards.createObject("CardViewModel")
    return
) cards, jQuery, ko