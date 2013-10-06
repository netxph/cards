# CoffeeScript
((cards, $, window) ->
    cards.Class.Session = ->
        self = this

        self.repost = ->
            href = window.location.href
            href = href.replace("/create#", "/create?")
            window.location.href = href

            return

        self.onReady = ->
            self.repost()
            return

        return

    cards.createObject("Session")
    return
) cards, jQuery, window 