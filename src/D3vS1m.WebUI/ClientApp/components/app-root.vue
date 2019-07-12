<template>
    <div id="app" class="container-fluid">
        <div class="row">
            <div class="col-sm-9">
                <router-view></router-view>
            </div>
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
      }
    }
</script>

<style>
</style>
