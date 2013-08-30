# CoffeeScript
((cards, $, ko) -> 
    
    cards.Class.LabelsViewModel = ->
        self = this

        # Properties
        self.rootUrl = $("meta[name=cards-baseurl]").attr("content")
        self.labels = ko.observableArray([])
        self.newLabel = 
            Name: ko.observable("")
            Color: ko.observable("")

        # Methods
        self.onReady = ->
            ko.applyBindings self
            self.initialize()
            self.refresh()
            return
        
        self.refresh = ->
            $.getJSON(self.rootUrl + "api/labels")
                .done (data) ->
                    self.labels(data)
                    self.colorize()                                                    
                    return
                .fail ->
                    self.showError "Santa can't figured out what happened, can you try it again?"
                    return

            return

        self.addLabel = ->
            label = ko.toJS(self.newLabel)

            $.post(self.rootUrl + "api/labels", label)
                .done ->
                    self.newLabel.Name("");
                    self.newLabel.Color("");
                    self.refresh()
                    return
                .fail ->
                    self.showError "Santa can't figured out what happened, can you try it again?"
                    return
            return

        self.deleteLabel = ->
            label = this

            $.ajax(
                url: self.rootUrl + "api/labels/"
                data: label
                type: "DELETE")
                .done ->
                    self.refresh()
                    return
                .fail ->
                    self.showError "Santa can't figured out what happened, can you try it again"
                    return
            return

        self.colorizeLabel = (label) ->
            labelElement = $(label)

            color = labelElement.data("color")
            labelElement.css("background-color", color)
            return        
        
        self.colorize = ->
            labels = $("#labels-big span")

            self.colorizeLabel label for label in labels
            return    

        self.showError = (message) ->
            $("#error-modal").show()
            $("#error-modal span").text message
            return
        
        self.initialize = ->
            $("body").on 
                ajaxStart: ->
                    $("#error-modal").hide()

                    $(this).addClass "loading"
                    return
                ajaxStop: ->
                    $(this).removeClass "loading"
                    return
            return

        return self

    cards.createObject "LabelsViewModel"
    return

) cards, jQuery, ko