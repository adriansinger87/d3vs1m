<template>
    <div style="display: flex; align-items:center;">
        <div
            class="objFileItem"
            style="display: flex;  align-items:center; width: 100%; margin-right: 10px"
        >
            <img src="../../images/objIcon.png" style="width: 50px" />
            <div style="font-size: 25px; color: white">  {{objFile.fileName}}</div>
        </div>
        <a
            class="btn-floating waves-effect waves-light btn-small"
            v-on:click="runSimulation(objFile)"
            style="margin-right: 20px"
        >
            <i class="material-icons">play_arrow</i>
        </a>
    </div>
</template>

<script>
import utils from '../../services/loadObjFile'
export default {
    props: ['objFile'],
    methods: {
        runSimulation: function (objFile) {
            utils.loadObjFile("http://"+objFile.link);
        }
    }
};
</script>

<style scoped>
.objFileItem:hover { 
    border: 2px solid #006064;
}
</style>