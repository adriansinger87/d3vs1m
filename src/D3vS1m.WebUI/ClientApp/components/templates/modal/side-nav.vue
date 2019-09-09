<template>
  <ul id="slide-out" class="sidenav">
    <li>
      <div class="user-view">
        <div class="background">
        </div>
        <a href="#user">
          <img class="circle" src="https://materializecss.com/images/yuna.jpg" />
        </a>
        <a href="#name">
          <span class="white-text name">John Doe</span>
        </a>
        <a href="#email">
          <span class="white-text email">jdandturk@gmail.com</span>
        </a>
      </div>
    </li>
    <li>
      <a class="subheader white-text" style="display: inline">Simualtion Tasks</a>
      <a class="waves-effect waves-light btn-small red" v-on:click="addArgumentTest()" style="display: inline">+</a>
    </li>
    <li>
      <div class="divider" style="background-color: gray"></div>
    </li>
    <li>
      <a class="waves-effect white-text" href="#!">Third Link With Waves</a>
    </li>
  </ul>
</template>

<script>
export default {
  mounted() {

  },
  methods: {
    addArgumentTest() {

      console.log("add button is clicked")


      //TODO: call API to generate ID

      //TODO: add to global varible  
      
      //TODO: Binding 
    }
  }
};
</script>

<style scoped>
.sidenav {
    background-color: #263238; 
}

.background {
    background-color: #006064;
}

</style>