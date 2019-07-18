<template>
<div>
  <div v-for="arg in argList" v-bind:key="arg.guid" class="col s12 m12 l6">
    <argument v-bind:arg="arg"></argument> 
  </div>
</div>
</template>

<script>
import Argument from '../templates/argument'
import RepositoryFactory from '../../services/RepositoryFactory'
const argumentsRepository = RepositoryFactory.get('arguments')

export default {
  components: {
    'argument': Argument 
  },
  data () {
    return {
      isLoading: false,
      argList: []
    }
  },
  created() {
    this.fetch();
  },
  methods: {
    async fetch() {
      this.isLoading = true;
      const {data} = await argumentsRepository.get(); 
      this.isLoading = false;
      this.argList = data;
    }   
  }
}
</script>

<style>

</style>
