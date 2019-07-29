<template>
  <div id="app" class="container-fluid">
    <app-header></app-header>
    <pre-loader v-if="isPreLoad"></pre-loader>
    <pro-loader v-if="isProLoad" v-bind:width="percentage"></pro-loader>
    <div style="flex: 1">
        <router-view></router-view>
    </div>
    <app-footer></app-footer>
  </div>
</template>

<script>
import homepage from "./pages/home";
import preloader from './templates/preload'
import progressLoader from './templates/progress'
import navbar from "./app-header";
import footer from "./app-footer";
export default {
  components: {
    homepage: homepage,
    "app-header": navbar,
    "app-footer": footer,
    "pre-loader": preloader,
    "pro-loader": progressLoader
  },
  data() {
    return {
      isPreLoad: false,
      isProLoad: false,
      percentage: "73%"
    };
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
