<template>
  <div class="col s12 m12 l6">
    <argument></argument> 
  </div>
</template>

<script>
import Argument from '../templates/argument'
import axios from 'axios';

export default {
  components: {
    'argument': Argument 
  },
  data () {
    return {}
  },
  created() {
    axios.get('http://localhost:51410/api/arguments')
    .then(response => {
      console.log(response)
    })
    .catch(e => {
      this.errors.push(e)
    })
  }
}
</script>

<style>

</style>
