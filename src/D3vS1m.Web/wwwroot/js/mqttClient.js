$(function () {

    var host = "broker.hivemq.com"; // borker url
    var port = "8000";              // websocket port

    // Create a client instance
    client = new Paho.Client(host, Number(port), "d3vs1m-clientId");

    // set callback handlers
    client.onConnectionLost = onConnectionLost;
    client.onMessageArrived = onMessageArrived;

    // connect the client
    client.connect({ onSuccess: onConnect });


    // called when the client connects
    function onConnect() {
        // Once a connection has been made, make a subscription and send a message.
        console.log("onConnect");
        client.subscribe("d3vs1m/console");

        // send messages like this
        //message = new Paho.Message("hello d3vs1m");   // message
        //message.destinationName = "d3vs1m";           // topic
        //client.send(message);
    }

    // called when the client loses its connection
    function onConnectionLost(responseObject) {
        if (responseObject.errorCode !== 0) {
            console.log("onConnectionLost:" + responseObject.errorMessage);
        }
    }

    // called when a message arrives
    function onMessageArrived(message) {
        console.log("onMessageArrived:" + message.payloadString);
    }

});