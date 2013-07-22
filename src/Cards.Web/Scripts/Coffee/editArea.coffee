# CoffeeScript
((window, cards, ko, $) ->
    cards.Class.AreaViewModel = ->
        self = this

        self.rootUrl = "../../"

        self.area = {}

        self.updateArea = ->
            area = ko.mapping.toJS(self.area)

            $.ajax(
                url: self.rootUrl + "api/areas/" + area.ID,
                type: "PUT",
                data: area)
                .done ->
                    window.location.href = self.rootUrl + "areas"        
            
            return

        self.getArea = ->
            id = $(".edit-section").data("areaid")
            
            $.getJSON self.rootUrl + 'api/areas/' + id, (data) ->
                self.area = ko.mapping.fromJS(data)
                ko.applyBindings self
                return

            return

        self.onReady = ->
            self.getArea()
            
            return

        return self

    cards.createObject "AreaViewModel"
    return
) window, cards, ko, jQuery