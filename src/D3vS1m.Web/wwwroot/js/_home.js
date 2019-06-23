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
            appVue.getArguments();
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
                currentGuid: null,
                arguments: null,
                arg: null
            },
            methods: {
                getArguments: getArguments,
                getArgument: getArgument,
                updateArgument: updateArgument
            }
        });
    }

    function getArguments () {

        $.ajax({
            url: "/api/Arguments",
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
            url: "/api/Arguments/" + id,
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
            url: "/api/Arguments/" + id,
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

});