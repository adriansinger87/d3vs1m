$(function () {

    // -- fields

    var client;
    var host = "broker.hivemq.com"; // borker url
    var port = "8000";              // websocket port
    var guid = "";

    // - -procedure

    $(document).ready(function () {

        // connect the client on event base
        connectMqtt();
    });

    // -- functions

    // TODO @ AS: provide a session bound guid for personal topics

    function connectMqtt(event, id) {
        var guid = id == undefined ? "" : "-" + id;
        client = new Paho.Client(host, Number(port), "d3vs1m-browser" + guid);
        client.onConnectionLost = onConnectionLost;
        client.onMessageArrived = onMessageArrived;
        client.connect({
            cleanSession: true,
            reconnect: true,
            onSuccess: function () {
                console.log("onConnected() with guid:" + guid);
                client.subscribe("d3vs1m/" + guid + "/console");
                client.subscribe("d3vs1m/" + guid + "/disconnect");
            },
            onFailure: function () {
                console.error("mqtt connection failed to host: " + host + " port: " + port);
            }
        });
    }

    function disconnectMqtt() {
        client.disconnect();
    }
    
    function onConnectionLost(responseObject) {
        // called when the client loses its connection
        if (responseObject.errorCode !== 0) {
            console.log("onConnectionLost:" + responseObject.errorMessage);
        }
    }
        
    function onMessageArrived(message) {
        // called when a message arrives
        var html = $("#console-content").html() + "<br />";
        $("#console-content").html(html + message.payloadString);
        $("#console-modal").scrollTop($("#console-content")[0].clientHeight);
    } 

});