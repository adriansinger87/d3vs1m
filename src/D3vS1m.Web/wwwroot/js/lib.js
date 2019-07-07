
var RUN_SIMULATION = "run-simulation-event";

var SUBSCRIBE_MQTT = "subscribe-mqtt-event";
var UNSUBSCRIBE_MQTT = "unsubscribe-mqtt-event";

var CONNECT_MQTT = "connect-mqtt-event";
var MQTT_CONNECTED = "mqtt-connected-event";
var DISCONNECT_MQTT = "disconnect-mqtt-event";
var MQTT_DISCONNECTED = "mqtt-disconnected-event";

function showToast(data) {

    var d = data;
    if (d === undefined) d = {};
    if (d.html === undefined) d.html = "empty toast";

    M.toast(d);
}

function showSuccessToast(text) {
    var d = {
        html: text,
        classes: 'toast-success',
        displayLength: 4000
    };
    M.toast(d);
}

function showErrorToast(text) {
    var d = {
        html: text,
        classes: 'toast-error',
        displayLength: 6000
    };
    M.toast(d);
}

function copyTextToClipboard(text) {

    var myText = text.replaceAll("<br>", "\r\n").replaceAll("<br />", "\r\n");

    if (!navigator.clipboard) {
        fallbackCopyTextToClipboard(myText);
        return;
    }
    navigator.clipboard.writeText(myText).then(
        function () {
            showToast({ html: "Copied" });
        },
        function (err) {
            var msg = "Could not copy text: " + err;
            console.error(msg);
            showErrorToast(msg);
        }
    );

    // -- 

    function fallbackCopyTextToClipboard(text) {
        var textArea = document.createElement("textarea");
        textArea.value = text;
        document.body.appendChild(textArea);
        textArea.focus();
        textArea.select();

        try {
            var successful = document.execCommand("copy");
            var msg = successful ? "successful" : "unsuccessful";
            console.log("Fallback: Copying text command was " + msg);
        } catch (err) {
            console.error("Fallback: Oops, unable to copy", err);
        }

        document.body.removeChild(textArea);
    }
}