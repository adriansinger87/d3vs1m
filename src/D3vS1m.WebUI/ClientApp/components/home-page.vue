<template>
  <div>
    <nav-bar></nav-bar>
    <argument></argument> 
  </div>
</template>

<script>
import Navbar from './nav-bar'
import Argument from './templates/argument-template'
export default {
  components: {
    'nav-bar': Navbar,
    'argument': Argument 
  },
  data () {
    return {}
  }
}
</script>

<style>

</style>
