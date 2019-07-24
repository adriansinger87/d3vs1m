<template>
  <div style="height: 100%" class="contain">
    <div class="row" style="flex: 1">
      <div class="container"> 
        <div v-for="arg in argList" v-bind:key="arg.guid" class="col s12 m12 l6">
          <argument v-bind:arg="arg"></argument>
        </div>
      </div>
    </div>
    <modal-console v-if="true"></modal-console>
  </div>
</template>

<script>
// info: Component Import
import Argument from "../templates/argument";
import modalConsole from "../templates/modal/console";

// info: REPOSITORY IMPORT
import RepositoryFactory from "../../services/RepositoryFactory";
const argumentsRepository = RepositoryFactory.get("arguments");

export default {
  components: {
    argument: Argument,
    "modal-console": modalConsole
  },
  data() {
    return {
      isLoading: false,
      argList: []
    };
  },
  created() {
    this.fetch();
  },
  methods: {
    async fetch() {
      this.isLoading = true;
      const { data } = await argumentsRepository.get();
      this.isLoading = false;
      this.argList = data;
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
