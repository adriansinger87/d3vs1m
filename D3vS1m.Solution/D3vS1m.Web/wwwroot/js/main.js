$(function () {

    var appVue;
    
    appVue = new Vue({
        el: '#app-vue',
        data: {
            message: 'Hello',
            simulators: null
        },
        methods: {
            getSimulators: GetSimulators
        }
    });

    // -- functions

    function GetSimulators() {

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

});