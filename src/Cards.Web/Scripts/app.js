$(document).ready(function () {
    function AreasViewModel() {
        this.areas =
    [
    {
        "ID": 1,
        "Name": "Backlog",
        "Cards": [
            {
                "ID": 1,
                "Name": "Create UI Mockup"
            },
            {
                "ID": 2,
                "Name": "Build Database"
            }
        ]
    },
    {
        "ID": 2,
        "Name": "Todo",
        "Cards": [
            {
                "ID": 3,
                "Name": "Create Kanban Board"
            }
        ]
    },
    {
        "ID": 3,
        "Name": "Doing",
        "Cards": [
            {
                "ID": 4,
                "Name": "Create REST API"
            }
        ]
    },
    {
        "ID": 4,
        "Name": "Done",
        "Cards": [
            {
                "ID": 5,
                "Name": "Write javascript"
            }
        ]
    }
    ];
    };

    ko.applyBindings(new AreasViewModel());

    $("article footer div").hide();

    $("article footer a").on("click", function () {
        $(this).parent().find("div").slideToggle();
    });
});