
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