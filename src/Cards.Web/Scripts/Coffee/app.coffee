# CoffeeScript
(($, ko) ->
    
    cards = window.cards = (->
        
        #reset class
        self = {}
        self.Class = {}
        
        self.events = 
            "onReady" : []

        self.runEvent = (eventName) ->
            for event in self.events[eventName]
                event()
            return

        self.createObject = (objectType) ->
            obj = new cards.Class[objectType]()

            for event of self.events
                self.events[event].push obj[event]

            return obj    

        return self
    )()

    $(document).ready ->
        cards.runEvent("onReady")
        return

    return
) jQuery, ko
