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
      <a href="#!" class="white-text">Second Link</a>
    </li>
    <li>
      <div class="divider"></div>
    </li>
    <li>
      <a class="subheader white-text">Subheader</a>
    </li>
    <li>
      <a class="waves-effect white-text" href="#!">Third Link With Waves</a>
    </li>
  </ul>
</template>

<script>
export default {

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