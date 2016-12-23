$(document).ready(function ($) {

    $("#toggleSearchButton").click(function(e) {
        $("#simpleSearch").toggle();
        $("#extendedSearch").toggle();
        $(".searchField").val('');
        $(".searchCheckbox").prop("checked", false)
        $(this)
            .text(function(i, text) {
                return text === "Search" ? "Extended search" : "Simple Search";
            });

    });
});