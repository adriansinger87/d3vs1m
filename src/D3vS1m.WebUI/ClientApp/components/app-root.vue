<template>
  <div id="app" class="container-fluid">
    <app-header></app-header>
    <div class="row">
      <div class="container">
        <router-view></router-view>
      </div>
    </div>
    <app-footer></app-footer>
  </div>
</template>

<script>
import homepage from "./pages/home";
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