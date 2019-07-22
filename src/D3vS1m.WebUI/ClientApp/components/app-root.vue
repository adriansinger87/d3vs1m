<template>
  <div id="app" class="container-fluid" style="height: 100%; display: flex;
	flex-direction: column;">
    <app-header></app-header>
    <div class="row" style="flex: 1" > 
      <div class="container">
        <router-view></router-view>
      </div>
    </div>
    <modal-console></modal-console>
    <app-footer></app-footer>
  </div>
</template>

<script>
import modalConsole from './templates/modal/console'
import homepage from "./pages/home";
import navbar from './app-header'
import footer from "./app-footer";
export default {
  components: {
    homepage: homepage,
    "app-header": navbar,
    "app-footer": footer,
    "modal-console": modalConsole,
  },
  data() {
    return {};
  },
  mounted() {}
};
</script>