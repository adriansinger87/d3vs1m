<template>
    <div style="height: 100%" class="contain">
        <div class="row" style="width: 100%; flex: 1;padding: 0px !important; margin-bottom: 0px !important; "> 
            <!--File Panel-->
            <div class="col s6" style="height: 100%; padding: 0px !important; background-color: #323232"> 
                <div id="folder" style="height: 50%; overflow-y: auto">
                    <ul>
                        <li v-for="objFile in objFiles" v-bind:key="objFile.fileName">
                            <obj-file v-bind:objFile="objFile"></obj-file>
                        </li>
                    </ul>
                    <div style="width:100%; padding: 10px">
                        <a v-on:click="addNewObjFile()" style="width:100%; background-color: #006064" class="waves-effect waves-light btn">Upload</a>
                    </div>
                </div> 
                <div id="console" style="height: 50%;background-color: black; overflow-y: auto">
                    <div class="console-content">>_ Console is starting ...</div>
                </div>
            </div>
            <div class="col s6" style="height: 100%; display: inline-block; background-color: gray;padding: 0px !important"> 
                <!--Obj Screen-->
                <canvas id="c" style="width: 100%; height: 99%;"></canvas> 
            </div>
        </div>
        <side-nav></side-nav>
        <modal-console></modal-console>
    </div>
</template>

<script>

import modalConsole from "../templates/modal/console";
import sideNav from "../templates/modal/side-nav";
import objFile from "../templates/objFile";
import RepositoryFactory from '../../services/RepositoryFactory'
const objFilesRepository = RepositoryFactory.get("objFiles");

export default {
    components: {
        "modal-console": modalConsole,
        "side-nav": sideNav,
        "obj-file": objFile
    },
    data() {
        return {
            objFiles: null
        }
    },
    mounted() {
    },
    created() {
        this.getObjFiles();
    },
    methods: {
        async getObjFiles() {
            const {data} = await objFilesRepository.get()
            this.objFiles = data
        },
        addNewObjFile() {
            
        }
    }
};
</script>

<style scoped>

.vertical-center {
  margin: 0;
  position: absolute;
  top: 50%;
  -ms-transform: translateY(-50%);
  transform: translateY(-50%);
}

#console > .console-content {
  background: black;
  font-family: monospace;
  font-size: 1.2em;
  color: white;
}


.contain {
    display: flex;
    flex-direction: column;
}

#folder::-webkit-scrollbar-track
{
    -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.3);
    background-color: #F5F5F5;
    border-radius: 10px;
}

#folder::-webkit-scrollbar
{
    width: 10px;
    background-color: #F5F5F5;
}

#folder::-webkit-scrollbar-thumb
{
    background-color: #AAA;
    border-radius: 10px;
    background-image: -webkit-linear-gradient(90deg,
                                            rgba(0, 0, 0, .2) 25%,
                                            transparent 25%,
                                            transparent 50%,
                                            rgba(0, 0, 0, .2) 50%,
                                            rgba(0, 0, 0, .2) 75%,
                                            transparent 75%,
                                            transparent)
}



</style>