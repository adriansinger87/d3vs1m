<template>
  <div id="app" class="container-fluid">
    <app-header></app-header>
    <div style="flex: 1">
        <router-view></router-view>
    </div>
    <app-footer></app-footer>
  </div>
</template>

<script>
import homepage from "./pages/home";
import navbar from "./app-header";
import footer from "./app-footer";
export default {
  components: {
    homepage: homepage,
    "app-header": navbar,
    "app-footer": footer,
  },
  data() {
    return {};
  },
  mounted() {
  }
};
</script>

<style scoped lang="css">
#app {
  height: 100%;
  display: flex;
  flex-direction: column;
}
</style>
