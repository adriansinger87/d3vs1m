$(function () {

    // -- fields

    var apiPath = PATH + "api/";
    var appVue;
    var editor;

    // -- precedure

    initActions();
    initVue();
    initAce();

    console.info("_home.js done");

    // -- functions

    function initActions() {

        $(document).ready(function () {
            appVue.getArguments();

            $("#start-sim-link").safeBind("click", startSimulation);

            $("#copy-console-link").safeBind("click", copyConsole);

            $(document).safeBind(RUN_SIMULATION, runSimulation);
        });
    }

    function initAce() {
        editor = ace.edit("ace-editor");
        editor.setTheme("ace/theme/dracula");
        editor.session.setMode("ace/mode/json");;
    }

    function initVue() {
        appVue = new Vue({
            el: '#app-vue',
            data: {
                runtimeArg : null,  // the loaded runtime arg before the simulation
                currentGuid: null,  // guid of current simulator argument
                arguments: null,    // list of all arguments objects
                arg: null           // the last loaded simulator argument
            },
            methods: {
                getArguments: getArguments,
                getArgument: getArgument,
                updateArgument: updateArgument
            }
        });
    }

    // starts the server side prcedure, but the actual async run will startet when RUN_SIMULATION will be fired
    // this depends on the ready connection of the mqtt async message pipeline
    function startSimulation() {

        $.ajax({
            url: apiPath + "Simulation",
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            datatype: 'json',
            async: true,
            error: function (result) {
                console.error(result);
            },
            success: function (result) {
                console.debug(result);
                appVue.runtimeArg = result;
                $(document).trigger(SUBSCRIBE_MQTT, appVue.runtimeArg.guid);
                runSimulation();
            }
        });       
    }

    function runSimulation() {
        $.ajax({
            url: apiPath + "simulation/run/" + appVue.runtimeArg.guid,
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            datatype: 'json',
            async: true,
            error: function (result) {
                console.error(result);
            },
            success: function (result) {
                console.info(result);
            }
        });

        // clear console
        $("#console-content").html("");
        $('#console-modal').modal('open');
    }

    function getArguments () {
        $.ajax({
            url: apiPath + "Arguments",
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            datatype: 'json',
            async: true,
            error: function (result) {
                console.error(result);
            },
            success: function (result) {
                console.info(result);
                appVue.arguments = result;
            }
        });
    }

    function getArgument(id) {

        appVue.currentGuid = id;
        $.ajax({
            url: apiPath + "Arguments/" + id,
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            datatype: 'json',
            async: true,
            error: function (result) {
                console.error(result);
            },
            success: function (result) {    
                console.debug(result);
                appVue.arg = result;
                var str = JSON.stringify(appVue.arg, null, 2);
                editor.setValue(str);
                editor.clearSelection();

                $("#code-modal").modal('open');
            }
        });
    }

    function updateArgument(id) {

        var code = editor.getValue();

        $.ajax({
            url: apiPath + "Arguments/" + id,
            type: 'PUT',
            contentType: "application/json; charset=utf-8",
            datatype: 'json',
            data: JSON.stringify(code),
            async: true,
            error: function (result) {
                console.error(result);
            },
            success: function (result) {
                console.info(result);
            }
        });
    }

    function copyConsole() {

        copyTextToClipboard($("#console-content").html());
    }
});