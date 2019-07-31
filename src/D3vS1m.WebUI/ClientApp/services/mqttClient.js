import paho from "paho-mqtt";

//TODO: Put it in GLOBAL VARIBLE Setting
//TODO: if HTTPS, broker should be WSS
const host = "broker.hivemq.com"; // borker url
const port = "8000";


const baseTopic = "d3vs1m";
const consoleTopic = "console";
const disconnectTopic = "disconnect";


var client;
var guid = "";

function connectMQTT() {
    //TODO: Handle Valid input?
    client = new paho.Client(host, Number(port), "d3vs1m-browser")
    client.onConnectionLost = onConnectionLost;
    client.onMessageArrived = onMessageArrived;
    client.connect({
        //useSSL: true,
        cleanSession: true,
        reconnect: true,
        onSuccess: function () {
            var msg = "<span>Mqtt connected</span>";
            M.toast({ html: msg, classes: 'toast-success',  displayLength: 4000})
        },
        onFailure: function () {
            var msg = "<span> Mqtt connection failed to host: " + host + " port: " + port + "</span>"
            M.toast({ html: msg, classes: 'toast-error',  displayLength: 4000})
        }
    });
}

function disconnectMQTT() {
    client.disconnect();
}


function subscribe(event, id) {
    $("#console-progress").show();
    guid = id == undefined ? "" : id;
    console.log("Mqtt subscribing with guid:" + guid);
    client.subscribe(baseTopic + "/" + guid + "/" + consoleTopic);
    client.subscribe(baseTopic + "/" + guid + "/" + disconnectTopic);
}


function unsubscribe() {
    $("#console-progress").hide();
    console.log("Mqtt unsubscribing with guid:" + guid);
    client.unsubscribe(baseTopic + "/" + guid + "/" + consoleTopic);
    client.unsubscribe(baseTopic + "/" + guid + "/" + disconnectTopic);
}


function onConnectionLost(responseObject) {
    // called when the client loses its connection
    if (responseObject.errorCode !== 0) {
        console.log("onConnectionLost:" + responseObject.errorMessage);
    }
}

function onMessageArrived(message) {
    // called when a message arrives
    var currentTopic = message.topic.split('/').last();

    if (currentTopic == consoleTopic) {
        // console
        var html = $("#console-content").html() + "<br />";
        $("#console-content").html(html + message.payloadString);
        $("#console-modal").scrollTop($("#console-content")[0].clientHeight);
    } else if (currentTopic == disconnectTopic) {
        // disconnect
        unsubscribe();
    } else {
        // everything else
        console.log("Topic " + currentTopic + " with message " + message.payloadString);
    }

}

export default { connectMQTT, disconnectMQTT}