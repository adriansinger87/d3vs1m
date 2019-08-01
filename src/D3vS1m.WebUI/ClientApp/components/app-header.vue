<template>
  <nav>
    <div class="nav-wrapper container">
      <ul class="left">
        <li>
          <a id="side-nav-btn" data-activates="slide-out" class="pointer left" style="width: 55px;">
            <i class="mdi mdi-menu"></i>
          </a>
        </li>
      </ul>

      <a class="brand-logo center">
        <img class="header-logo left" src="../images/d3vs1m_logo.svg" />
        <span class="hide-on-small-only">D3V S1M</span>
      </a>

      <ul class="right">
        <li>
          <a v-on:click="startSimulation" class="waves-effect waves-light">
            <i class="mdi mdi-play right"></i> Start
          </a>
        </li>
        <li>
          <a id="console-link" class="modal-trigger waves-effect waves-light" href="#console-modal">
            <i class="mdi mdi-console left"></i> Console
          </a>
        </li>
      </ul>
    </div>
  </nav>
</template>

<script>

import RepositoryFactory from "../services/RepositoryFactory";
const  simulationRepository = RepositoryFactory.get("simulation")
export default {
  data() {
    return {};
  },
  mounted() {
    
  },
  methods: {
    startSimulation: async function(event) {
      //todo: will create a global function to use and call all MODAL in the page
      var elems= document.querySelector('.modal'); 
      var instances = M.Modal.init(elems);
      instances.open()

      //todo: call startSimulation
      const { data } = await simulationRepository.get()
      console.log(data)
      const simluationData = simulationRepository.runSimulation(data.guid, JSON.stringify(this.$store.getters.allArguments))
      console.log(this.$store.getters.allArguments)
      //console.log(simluationData)
    },
  }
};
</script>

<style scoped lang='css'>
nav {
  background-color: #006064;
}
.header-logo {
    margin-top: 10px;
    margin-right: 10px;
}


</style>
