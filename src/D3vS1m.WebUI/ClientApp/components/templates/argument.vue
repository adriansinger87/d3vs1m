<template>
 <div class="card blue-grey darken-1">
                <div class="card-content white-text">
                    <span v-if="arg.active" class="new badge badge-active" data-badge-caption="AKTIV"></span>
                    <span class="card-title">{{ arg.name }}</span>
                </div>
                <div class="card-action">
                    <a><i class="mdi mdi-play"></i></a>
                    <a><i class="mdi mdi-stop"></i></a>
                    <a><span class="vertical-separator"></span></a>
                    <a v-on:click="getArgument(arg.guid)"  class="right"><i class="mdi mdi-tune"></i></a>
                </div>
            </div> 
</template>

<script>

import utils  from '../../services/ultis'
import EventBus from '../../services/CodeEditorEventBus'


export default {
  data() {
    return {
    };
  },
  methods: {
    getArgument(guid) {


      //TODO: get the config from Store
    var args = this.$store.getters.allArguments

    var argumentData = args.filter(x => x.guid == guid)

    EventBus.$emit('open-code-editor', argumentData)

      //TODO: set the code-editor-value to Store 
      //info: code-editor-value can check the state? 3 types: edited-send, edited-notsend, nochange



      //info: open Modal 
      var instance = utils.getModalInstance("code-modal") 
      instance.open()

    }
  },
  props: ['arg']
};
</script>

<style>
</style>
