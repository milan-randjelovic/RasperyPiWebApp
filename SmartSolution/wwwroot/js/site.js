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

    if (window.location.pathname == "/Sensors") {
        RefreshSensors();
    }
});

//Turn on switch click
function TurnONSwitch(switchButton) {

    $.ajax({
        url: "/Switches/SwitchesTurnON/",
        data: { id: switchButton.id },
        dataType: "text",
        success: function (data, status) {
            var switchid = JSON.parse(data).id;
            var switchObject = $("#" + switchid);
            switchObject.removeClass('orange');
            switchObject.addClass('green');
            var button = switchObject.children("#" + switchid);
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

//Turn off switch click
function TurnOFFSwitch(switchButton) {

    $.ajax({
        url: "/Switches/SwitchesTurnOFF/",
        data: { id: switchButton.id },
        dataType: "text",
        success: function (data, status) {
            var switchid = JSON.parse(data).id;
            var switchObject = $("#" + switchid);
            switchObject.removeClass('green');
            switchObject.addClass('orange');
            var button = switchObject.children("#" + switchid);
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

//refresh sensors data
function RefreshSensors() {
    $.ajax({
        url: "/Sensors/SensorsValues",
        success: function (data, status) {
            var sensors = JSON.parse(data);
            for (i = 0; i < sensors.length; i++) {
                var sensor = sensors[i];
                var sensorObject = $('[data-sensorid=' + sensor.id + ']')[0];
                sensorObject.innerHTML = sensor.value;
            }
            setTimeout(function () { RefreshSensors(); }, 1000);
        },
        error: function (status) {
            console.log("Error");
        }
    }
    );
}