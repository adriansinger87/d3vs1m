<template>
    <div id="app" class="container-fluid">
        <div class="row">
                <router-view></router-view>
        </div>
    </div>
</template>

<script>
    import homepage from './home-page'
    export default {
      components: {
        homepage: homepage        
      },
      data () {
        return {}
      }, 
      mounted() {
      }
    }
</script>