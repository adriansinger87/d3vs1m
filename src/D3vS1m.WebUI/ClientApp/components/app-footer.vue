<template>
  <footer id="footer" class="page-footer scrollspy">
    <div class="container grey-text text-lighten-2 hide-on-small-only">
      <div class="row">
        <div class="col s12 m12 l8">
          <ul class="footer-links">
            <li>
              <h5>Adrian Singer</h5>
            </li>
            <li>
              <div class="valign-wrapper">
                <a href="http://adrian-singer.de/" target="_blank">
                  <i class="mdi mdi-web left"></i>
                </a>
                Webseite
              </div>
            </li>
            <li>
              <div class="valign-wrapper">
                <a href="https://github.com/adriansinger87" target="_blank">
                  <i class="mdi mdi-github-circle left"></i>
                </a>
                GitHub
              </div>
            </li>
            <li>
              <div class="valign-wrapper">
                <a href="mailto:adriansinger87@gmail.com?subject=@ViewBag.AppTitle">
                  <i class="mdi mdi-gmail left"></i>
                </a>
                private Email
              </div>
            </li>
          </ul>
        </div>

        <div class="col s12 m12 l4">
          <h5>Technologies</h5>
                <div class="chip">
                    <img src="../images/html5.jpg" />
                    <a href="https://www.w3.org/TR/html5/" target="_blank">HTML 5</a>
                </div>
                <div class="chip">
                    <img src="../images/materialize.jpg" />
                    <a href="http://materializecss.com/" target="_blank">MaterializeCSS</a>
                </div>
                <div class="chip">
                    <img src="../images/vuejs-logo.png" />
                    <a href="https://vuejs.org/" target="_blank">Vue.js</a>
                </div>
                <div class="chip">
                    <img src="../images/moment.jpg" />
                    <a href="https://momentjs.com/" target="_blank">Moment.js</a>
                </div>
                <div class="chip">
                    <img src="../images/visual-studio.jpg" />
                    <a href="http://www.asp.net/mvc" target="_blank">ASP.NET Core</a>
                </div> 
        </div>
      </div>
    </div>
    <div class="footer-copyright">
      <div class="container grey-text text-lighten-2">
        <div class="row">
          <div class="col s8"> {{ AppInfo.AppName }} © 2019 Adrian Singer</div>
          <div class="col s4 right-align">Version: {{ AppInfo.AppVersion }}</div>
        </div>
      </div>
    </div>
  </footer>
</template>
<script>
import appInfo from '../appInfo.json'

export default {
  data() {
    return {
      AppInfo: appInfo
    };
  },
  methods: {}
};
</script>

<style scoped lang='css'>

.page-footer {
    background-color: #212121;
}

footer a {
    color: dimgrey;
}

.chip {
    background-color: rgba(0, 0, 0, 0.20);
}

</style>
