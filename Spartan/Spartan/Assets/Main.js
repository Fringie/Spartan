$(document).ready(function() {
    Styling();
    Api();
});

var Styling = function () {
    // Handle colour change of button that acts like a toggle
    $(".search-area button").on('click', function (e) {
        e.preventDefault();
        if (!$(this).hasClass("focused")) {
            $(".focused").removeClass("focused");
            $(this).addClass("focused");
        }
    });
}

var Api = function() {
    // Get all records on page load
    $.getJSON("api/equipment/GetAll")
        .done(function (data) {
            $.each(data, function (key, item) {
                var insertEntry = '<li><div class="row"><div class="col-md-10"><div class="item-number-value">' +
                    item.ItemNumber +
                    '</div><div class="unit-number-value">' +
                    item.UnitNumber +
                    '</div><div class="description-value">' +
                    item.Description +
                    '</div></div><div class="col-md-2"><div class="chevron"><i class="fas fa-chevron-right fa-3x"></i></div></div></div></li>';

                $(insertEntry).appendTo("#results");
            });
        });

    // When user enters a search
    $("#search-input-submit").on('click',
    function () {
        $("#results").children().remove();
        // Figure out what the http request is
        var htmlElementId = $(".search-area").find(".focused").attr('id');
        var apiUri = "api/equipment";
        if (htmlElementId === "unit-no") { 
            apiUri += "/GetAllByUnitNumber"
        } else {
            apiUri += "/GetAllByItemNumber";
        }
        // get the search param from the input box
        apiUri += "/" + $("#search-input").val();
        // Display the results
        $.getJSON(apiUri)
            .done(function (data) {
                $.each(data, function (key, item) {
                    var insertEntry = '<li><div class="row"><div class="col-md-10"><div class="item-number-value">' +
                        item.ItemNumber +
                        '</div><div class="unit-number-value">' +
                        item.UnitNumber +
                        '</div><div class="description-value">' +
                        item.Description +
                        '</div></div><div class="col-md-2"><div class="chevron"><i class="fas fa-chevron-right fa-3x"></i></div></div></div></li>';

                    $(insertEntry).appendTo("#results");
                });
            });
    });

}