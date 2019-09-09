<template>
  <div id="console-modal" class="modal bottom-sheet z-depth-2">
    <div id="console-content" class="modal-content">>_</div>
    <div id="console-progress" v-show="isLoading" class="progress">
      <div class="indeterminate"></div>
    </div>
    <div class="modal-footer">
      <a id="clear-console-link"  v-on:click="clearConsole" class="waves-effect waves-red btn-flat left">
        <i class="mdi mdi-playlist-remove left"></i> Clear
      </a>
      <a id="copy-console-link" v-on:click="copyConsole" class="waves-effect waves-light btn-flat left">
        <i class="mdi mdi-clipboard-text-outline left"></i> Copy
      </a>
      <a href="#!" class="modal-close waves-effect waves-light btn-flat">
        <i class="mdi mdi-close left"></i>Close
      </a>
    </div>
  </div>
</template>

<script>
// info: IMPORT library
import $ from 'jquery'
import utils from '../../../services/ultis'


import ArgumentEventBus from '../../../services/EventBus/ArgumentEventBus'

export default {
  data() {
    return {
      isLoading: false
    };
  },
  mounted() {
      ArgumentEventBus.$on('isLoading', (value) => {
        console.log(value)
        this.isLoading = value
      })
  },
  methods: {
    clearConsole() {
      $("#console-content").html(">_");
    },
    copyConsole() {
      utils.copyTextToClipboard($("#console-content").html());
    }
  }
};
</script>

<style scoped lang='css'>
.btn-flat {
  color: white;
}

#console-progress {
  margin: 0px;
}

.vertical-separator {
  width: 1px;
  height: 20px;
  display: inline-block;
  border: solid 1px rgba(160, 160, 160, 0.2);
}

#console-content {
  max-height: 400px; 
  overflow-y: scroll;
}

#console-modal > .modal-content {
  background: black;
  font-family: monospace;
  font-size: 1.2em;
  color: white;
}

.modal .modal-footer {
  background-color: #37474f;
}
</style>
