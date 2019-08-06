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

function connectMQTT(id) {
    //TODO: Handle Valid input?
    client = new paho.Client(host, Number(port), "d3vs1m-browser-dfsadf")
    client.onConnectionLost = onConnectionLost;
    client.onMessageArrived = onMessageArrived;
    client.connect({
        //useSSL: true,
        cleanSession: true,
        reconnect: true,
        onSuccess: function () {
            var msg = "<span>Mqtt connected</span>";
            M.toast({ html: msg, classes: 'toast-success',  displayLength: 4000})
            subscribe(id)
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


function subscribe(id) {
    //$("#console-progress").show();
    console.log("subscribe")
    guid = id == undefined ? "" : id;
    console.log("Mqtt subscribing with guid:" + guid);
    console.log(baseTopic + "/" + guid + "/" + consoleTopic)
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
    
    console.log(message.payloadString)
    var consoleContent = document.getElementById("console-content")
    consoleContent.innerHTML += "<br />"
    consoleContent.innerHTML +=  message.payloadString
    //TODO: need to close the socket
}

export default { connectMQTT, disconnectMQTT, subscribe }