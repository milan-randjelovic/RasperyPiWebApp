function TurnONSwitch(switchButton) {

    //window.location = "/Switches/TurnON/" + switchObj.id;

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
            console.log("ON")
        },
        error: function (status) {
            console.log("Error")
        }
    }
    );
}

function TurnOFFSwitch(switchButton) {

    //window.location = "/Switches/TurnOFF/" + switchObj.id;

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
            console.log("OFF")
        },
        error: function (status) {
            console.log("Error")
        }
    }
    );
}
