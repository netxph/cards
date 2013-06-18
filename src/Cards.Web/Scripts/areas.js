(function (Cards, $, ko, undefined) {

    Cards.ViewModel = {};

    var AreasViewModel = function () {
        self = this;

        this.areas = [];
        
        this.init = function () {
            $.getJSON("/api/areas", function (data) {
                self.areas = data;
                ko.applyBindings(self);

                $("article footer div").hide();

                $("article footer a").on("click", function () {
                    $(this).parent().find("div").fadeToggle();
                });
            });
        };
    };

    Cards.ViewModel = new AreasViewModel();

}(window.Cards = window.Cards || {}, $, ko));