import paho from "paho-mqtt";
import utils from "../services/ultis"
import $ from 'jquery'
//TODO: Put it in GLOBAL VARIBLE Setting
//TODO: if HTTPS, broker should be WSS
const host = "broker.hivemq.com"; // borker url
const port = "8000";


const baseTopic = "d3vs1m";
const consoleTopic = "console";
const disconnectTopic = "disconnect";


var client;
var guid = "";
var currentHeight = 0;

//info: EventBus
import ArgumentEventBus from '../services/EventBus/ArgumentEventBus'

function connectMQTT(id) {
    //TODO: Handle Valid input?
    client = new paho.Client(host, Number(port), "d3vs1m-browser-" + utils.guid())
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
    var mesSplit = message.topic.split('/')
    var currentTopic = mesSplit[mesSplit.length -1]
    var html = $("#console-content").html() + "<br />";

    console.log(message)
    if (currentTopic == consoleTopic) {
        ArgumentEventBus.$emit('isLoading', true)
        $("#console-content").html(html + message.payloadString);
    } else if (currentTopic == disconnectTopic){
        unsubscribe() 
        currentHeight = currentHeight + $("#console-content").height()
        $("#console-content").scrollTop(currentHeight)
        ArgumentEventBus.$emit('isLoading', false)
    } else {
        // everything else
        console.log("Topic " + currentTopic + " with message " + message.payloadString);
        ArgumentEventBus.$emit('isLoading', false)
    }
}

export default { connectMQTT, disconnectMQTT, subscribe }