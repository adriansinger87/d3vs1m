<template>
  <div id="console-modal" class="bottom-sheet z-depth-2">
    <div id="console-content" class="modal-content">>_</div>
    <div id="console-progress" class="progress">
      <div class="indeterminate"></div>
    </div>
    <div class="modal-footer" style="color: white">
      <a id="clear-console-link" href="#!" class="waves-effect waves-red btn-flat left">
        <i class="mdi mdi-playlist-remove left"></i> Clear
      </a>
      <a id="copy-console-link" href="#!" class="waves-effect waves-light btn-flat left">
        <i class="mdi mdi-clipboard-text-outline left"></i> Copy
      </a>
      <a v-on:click="closeModal" class="waves-effect waves-light btn-flat">
        <i class="mdi mdi-close left"></i>Close
      </a>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {};
  },
  methods: {
    showModal: function() {
      // Logic to show
    },
    closeModal: function() {
      // Logic to close
    }
  }
};
</script>

<style scoped lang='css'>

.btn-flat {
  color: white;
}



.code-footer {
  position: absolute;
  bottom: 0px;
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
