# CoffeeScript
((cards, $, window) ->
    cards.Class.Session = ->
        self = this

        self.rootUrl = $("meta[name=cards-baseurl]").attr("content")

        self.repost = ->
            query = window.location.hash.replace('#', '?')

            if query != ''
                path = window.location.origin + self.rootUrl + "session/create" + query
            else
                path = window.location.origin + self.rootUrl
                
            window.location.replace(path)
            return

        self.onReady = ->
            self.repost()
            return

        return

    cards.createObject("Session")
    return
) cards, jQuery, window 