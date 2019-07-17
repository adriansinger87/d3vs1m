<template>
  <div id="app" class="container-fluid">
    <app-header></app-header>
    <div class="row">
      <router-view></router-view>
    </div>
    <app-footer></app-footer>
  </div>
</template>

<script>
import homepage from "./pages/home-page";
import navbar from './app-header'
import footer from "./app-footer";
export default {
  components: {
    homepage: homepage,
    "app-header": navbar,
    "app-footer": footer
  },
  data() {
    return {};
  },
  mounted() {}
};
</script>