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
import CodeEditorEventBus from '../../services/EventBus/CodeEditorEventBus'

import $ from 'jquery'
import { mapGetters , mapActions } from "vuex";

export default {
  data() {
    return {
      percentage: "",
      isProLoad: false
    };
  },
  computed: mapGetters(['allArguments']),
  methods: {
    ...mapActions(['UPDATE']),
    getArgument(guid) {

      //TODO: get the config from Store
    var args = this.$store.getters.allArguments

    var argumentData = args.filter(x => x.guid == guid)

    CodeEditorEventBus.$emit('open-code-editor', argumentData)

      //info: open Modal 
      var instance = utils.getModalInstance("code-modal") 

      instance.options.onCloseStart = function() {
        //TODO: check if the value changed or not
        //info: code-editor-value can check the state? 3 types: edited-send, edited-notsend, nochange

        // compare data change 
        //console.log(argumentData)
      }

      CodeEditorEventBus.$on('updated-ArgData', (updatedArgData) => {
        // compare 
        //TODO: should be in the Utils (compare 2 objects)
        if((JSON.stringify(argumentData) != JSON.stringify(updatedArgData))) {          
          // update current 
          this.UPDATE(updatedArgData)
        }
        console.log(this.$store.getters.allArguments)
      })

      instance.open()
    }
  },
  props: ['arg']
};
</script>

<style>
</style>
