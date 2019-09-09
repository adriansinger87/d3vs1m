<template>
    <div id="code-modal" class="modal">
        <div class="modal-content">
            <div id="editor" class="code-editor">
            </div>
        </div>
        <div class="modal-footer code-footer">
            <a v-on:click="updateArgument(currentGuid)" class="modal-close waves-effect waves-green btn-flat">Send</a>
        </div>
    </div>
</template>

<script>
//info: lib  
import ultis from "../../../services/ultis";


import CodeEditorEventBus from "../../../services/EventBus/CodeEditorEventBus";

export default {
    mounted() {
        var editor = ultis.initAceEditor();

        CodeEditorEventBus.$on('open-code-editor', (argData) => {
            editor.setValue(JSON.stringify(argData, null, '\t'))
            editor.clearSelection(); 
        })
    }, 
    updated() {
    },
    methods: {
        updateArgument(id) {
        }
    } 
}
</script>

<style scoped>

#code-modal {
    height: 85%;
}

.code-editor {
    font-size: larger;
    position: absolute;
    display: block;
    top: 0;
    right: 0;
    bottom: 56px;
    left: 0;
}

.code-footer {
    position: absolute;
    bottom: 0px;
}

.modal .modal-footer {
    background-color: #37474f;
}
.btn-flat {
    color: white;
}





</style>