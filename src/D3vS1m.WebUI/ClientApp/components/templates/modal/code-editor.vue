<template>
    <div id="code-modal" class="modal">
        <div class="modal-content">
            <div id="ace-editor" class="code-editor"></div>
        </div>
        <div class="modal-footer code-footer">
            <a v-on:click="updateArgument(currentGuid)" class="modal-close waves-effect waves-green btn-flat">Send</a>
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {};
    },
    mounted() {
        
    },
    methods: {
        updateArgument(id) {

        }
    } 
}
</script>

<style>

</style>
