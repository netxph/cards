(function ($, ko, undefined) {
    function AreasViewModel() {
        this.Areas = [];
    };

    init = function () {
        var viewModel = new AreasViewModel();

        $.getJSON("/api/areas", function (data) {
            viewModel.Areas = data;

            ko.applyBindings(viewModel);
            $("article footer div").hide();

            $("article footer a").on("click", function () {
                $(this).parent().find("div").slideToggle();
            });
        });
    };

    $(document).ready(init());
})($, ko);