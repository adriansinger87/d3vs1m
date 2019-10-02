<template>
  <div class="progress preLoad">
      <div class="determinate" :style="{ width }"></div>
  </div>
</template>

<script>
export default {
    props: ['width'],
    data() {
       return {
       } 
    }
};
</script>

<style scoped>
.preLoad {
    height: 4px;
    margin: 0;
}
</style>