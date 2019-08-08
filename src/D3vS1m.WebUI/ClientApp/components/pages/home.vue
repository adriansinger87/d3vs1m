<template>
  <div style="height: 100%" class="contain">
    <div class="row" style="flex: 1">
      <div class="container"> 
        <div v-for="arg in allArguments" v-bind:key="arg.guid" class="col s12 m12 l6">
          <argument v-bind:arg="arg"></argument>
        </div>
      </div>
    </div>
    <modal-console></modal-console>
  </div>
</template>

<script>
// info: Component Import
import Argument from "../templates/argument";
import modalConsole from "../templates/modal/console";

// info: REPOSITORY IMPORT
import RepositoryFactory from "../../services/RepositoryFactory";
const argumentsRepository = RepositoryFactory.get("arguments");


// info: Service import
import mqttClient from "../../services/mqttClient";


// Vuex 
import { mapGetters, mapActions } from "vuex";

export default {
  components: {
    argument: Argument,
    "modal-console": modalConsole
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
  computed: mapGetters(['allArguments']),
  methods: {
    ...mapActions(['GET_ARG']),
    async fetch() {
      this.isLoading = true;
      this.GET_ARG()
      this.isLoading = false;
    }
  }
};
</script>

<style>
  .contain {
    display: flex;
    flex-direction: column;
  }
</style>
