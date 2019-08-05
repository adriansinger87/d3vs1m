<template>
 <div class="card blue-grey darken-1">
                <div class="card-content white-text">
                    <span v-if="arg.Active" class="new badge badge-active" data-badge-caption="AKTIV"></span>
                    <span class="card-title">{{ arg.Name }}</span>
                </div>
                <div class="card-action">
                    <a><i class="mdi mdi-play"></i></a>
                    <a><i class="mdi mdi-stop"></i></a>
                    <a><span class="vertical-separator"></span></a>
                    <a v-on:click="getArgument(arg.Guid)"  class="right"><i class="mdi mdi-tune"></i></a>
                </div>
            </div> 
</template>

<script>
export default {
  data() {
    return {
    };
  },
  methods: {},
  props: ['arg']
};
</script>

<style>
</style>
