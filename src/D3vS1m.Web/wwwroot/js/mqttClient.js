$(function () {

    // -- fields
    
    const host = "broker.hivemq.com";    // borker url
    const port = "8000";                 // websocket port

    const baseTopic = "d3vs1m";
    const consoleTopic = "console";
    const disconnectTopic = "disconnect";

    var client;
    var guid = "";

    // - -procedure

    $(document).ready(function () {

        $(document).safeBind(SUBSCRIBE_MQTT, subscribe);
        $(document).safeBind(UNSUBSCRIBE_MQTT, unsubscribe);
        connectMqtt();
    });

    // -- functions

    function connectMqtt() {
        client = new Paho.Client(host, Number(port), "d3vs1m-browser"); // TODO @ AS: add session or something to distinguish every client
        client.onConnectionLost = onConnectionLost;
        client.onMessageArrived = onMessageArrived;
        client.connect({
            cleanSession: true,
            reconnect: true,
            onSuccess: function () {
                var msg = "Mqtt connected";
                console.log(msg);
                showSuccessToast(msg);
            },
            onFailure: function () {
                var msg = "Mqtt connection failed to host: " + host + " port: " + port
                console.error(msg);
                showErrorToast(msg);
            }
        });
    }

    function disconnectMqtt() {
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

});