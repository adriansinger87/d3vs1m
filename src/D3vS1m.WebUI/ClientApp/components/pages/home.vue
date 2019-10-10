<template>
  <div style="height: 100%" class="contain">
    <div class="row" style="flex: 1">
      <div class="container"> 
        <div v-for="arg in allArguments" v-bind:key="arg.guid" class="col s12 m12 l6">
          <argument v-bind:arg="arg"></argument>
        </div>
      </div>
    </div>
    <side-nav></side-nav>
    <modal-console></modal-console>
    <modal-editor></modal-editor>
  </div>
</template>

<script>
// info: Component Import
import Argument from "../templates/argument";
import modalConsole from "../templates/modal/console";
import modalCodeEditor from '../templates/modal/code-editor';
import sideNav from '../templates/modal/side-nav';

// info: REPOSITORY IMPORT
import RepositoryFactory from "../../services/RepositoryFactory";
const argumentsRepository = RepositoryFactory.get("arguments");


// info: Service import
import mqttClient from "../../services/mqttClient";
import utils from '../../services/ultis'

// Vuex 
import { mapGetters , mapActions } from "vuex";

export default {
  components: {
    argument: Argument,
    "modal-console": modalConsole, 
    "modal-editor" : modalCodeEditor,
    "side-nav" : sideNav
  },
  mounted() {
    mqttClient.connectMQTT()
    
  },
  data() {
    return {
      isLoading: false,
    };
  },
  created() {
    this.fetch();
    
  }, 
  updated() {
    utils.initModal();
  },
  computed: mapGetters(['allArguments']),
  methods: {
    ...mapActions(['GET_ARG']),
    async fetch() {
      this.isLoading = true;
      this.GET_ARG()
      this.isLoading = false;

  
    },
  
  }
};
</script>

<style>
  .contain {
    display: flex;
    flex-direction: column;
  }
</style>
