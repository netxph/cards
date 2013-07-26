# CoffeeScript
((window, cards, ko, $) ->
    cards.Class.AreaViewModel = ->
        self = this

        self.rootUrl = $("meta[name=cards-baseurl]").attr("content") +'/'

        self.area = {}

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

        self.deleteArea = ->
            area = ko.mapping.toJS(self.area)

            $.ajax(
                url: self.rootUrl + "api/areas/" + area.ID,
                type: "DELETE")
                .done ->
                    window.location.href = self.rootUrl + "areas"        
                    return
                .fail ->
                    self.showError "Santa can't figured out what happened, can you try it again"
                    return

            return

        self.updateArea = ->
            area = ko.mapping.toJS(self.area)

            $.ajax(
                url: self.rootUrl + "api/areas/" + area.ID,
                type: "PUT",
                data: area)
                .done ->
                    window.location.href = self.rootUrl + "areas"        
                    return
                .fail ->
                    self.showError "Santa can't figured out what happened, can you try it again"
                    return
            return

        self.getArea = ->
            id = $(".edit-section").data("areaid")
            
            $.getJSON(self.rootUrl + 'api/areas/' + id) 
                .done (data) ->
                    self.area = ko.mapping.fromJS(data)
                    ko.applyBindings self
                    
                    return
                .fail ->
                    self.showError "Santa can't figured out what happened, can you try it again"
                    return

            return

        self.onReady = ->
            self.initControls()
            self.getArea()
            
            return

        return self

    cards.createObject "AreaViewModel"
    return
) window, cards, ko, jQuery