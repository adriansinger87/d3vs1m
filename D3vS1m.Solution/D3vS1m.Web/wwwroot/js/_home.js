$(function () {

    // -- fields

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
            appVue.getSimulators();
        });
    }

    function initAce() {
        editor = ace.edit("ace-editor");
        //ace.config.set("workerPath", "lib/ace/")
        editor.setTheme("ace/theme/dracula");
        editor.session.setMode("ace/mode/json");;
    }

    function initVue() {
        appVue = new Vue({
            el: '#app-vue',
            data: {
                message: 'Hello',
                simulators: null,
                arguments: null
            },
            methods: {
                getSimulators: getSimulators,
                getArguments: getArguments
            }
        });
    }

    function getSimulators() {

        $.ajax({
            url: "/api/v2/Simulators",
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            datatype: 'json',
            async: true,
            error: function (result) {
                console.error(result);
            },
            success: function (result) {
                console.info(result);
                appVue.simulators = result;
            }
        });
    }

    function getArguments(id) {

        $.ajax({
            url: "/api/Simulators/args?id=" + id,
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            datatype: 'json',
            async: true,
            error: function (result) {
                console.error(result);
            },
            success: function (result) {    
                console.debug(result);
                appVue.arguments = result;
                var str = JSON.stringify(appVue.arguments, null, 2);
                editor.setValue(str);
                editor.clearSelection();

                $("#code-modal").modal('open');
            }
        });
    }

});