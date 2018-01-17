$(document).ready(function () {
    $('[data-toggle="popover"]').popover();

    //show popover
    $('[data-toggle="popover"]').on('shown.bs.popover', function (object) {

        //paint popup
        var popover = object.currentTarget.nextSibling;
        var cssClass = object.currentTarget.classList[1];
        $(popover).addClass(cssClass);
        for (i = 0; i < popover.childNodes.length; i++) {
            $(popover.childNodes[i]).addClass(cssClass);
        }
        $('.popover-content').html("Loading data...");

        //get data for pin
        var pinId = object.currentTarget.id;
        $.ajax({
            url: "/Home/GetPinData/",
            data: { pinCode: pinId },
            dataType: "text",
            success: function (data, status) {
                if (data != "null") {
                    $('.popover-content').html(data);
                }
                else {
                    $('.popover-content').html("No data available");
                }
                console.log("Success");
            },
            error: function (status) {
                console.log("Error");
            }
        }
        );
    })
});

function TurnONSwitch(switchButton) {

    $.ajax({
        url: "/Switches/TurnONAsync/",
        data: { id: switchButton.id },
        dataType: "text",
        success: function (data, status) {
            var switchObject = $("#" + data);
            switchObject.removeClass('orange');
            switchObject.addClass('green');
            var button = switchObject.children("#" + data);
            button.html("OFF");
            button.attr("onclick", "TurnOFFSwitch(this)");
            console.log("ON");
        },
        error: function (status) {
            console.log("Error");
        }
    }
    );
}

function TurnOFFSwitch(switchButton) {

    $.ajax({
        url: "/Switches/TurnOFFAsync/",
        data: { id: switchButton.id },
        dataType: "text",
        success: function (data, status) {
            var switchObject = $("#" + data);
            switchObject.removeClass('green');
            switchObject.addClass('orange');
            var button = switchObject.children("#" + data);
            button.html("ON");
            button.attr("onclick", "TurnONSwitch(this)");
            console.log("OFF");
        },
        error: function (status) {
            console.log("Error");
        }
    }
    );
}