<template>
    <div style="height: 100%" class="contain">
        <div class="row" style="width: 100%; flex: 1;padding: 0px !important; margin-bottom: 0px !important; "> 
            <!--File Panel-->
            <div class="col s6" style="height: 100%; padding: 0px !important; background-color: #323232"> 
                <div id="folder" style="height: 50%; overflow-y: auto">
                    <ul>
                        <!--v-for="objFile in objFiles"-->
                        <li>
                            <div style="display: flex; align-items:center;" >
                                <div class="objFileItem" style="display: flex;  align-items:center; width: 100%; margin-right: 10px">  
                                    <img src="../../images/objIcon.png" style="width: 50px"  />
                                    <!--{{objFile.fileName}}-->
                                    <div style="font-size: 25px"></div>
                                </div>
                                <a class="btn-floating waves-effect waves-light btn-small" v-on:click="runSimulation(objFile)" style="margin-right: 20px"> <i class="material-icons">play_arrow</i></a>
                            </div>
                        </li>
                    </ul>
                </div> 
                <div id="console" style="height: 50%;background-color: red; overflow-y: auto">
                </div>
            </div>
            <div class="col s6" style="height: 100%; display: inline-block; background-color: gray;padding: 0px !important"> 
                <!--Obj Screen-->
                <canvas id="c" style="width: 100%; height: 100%;"></canvas> 
            </div>
        </div>
        <side-nav></side-nav>
        <modal-console></modal-console>
    </div>
</template>

<script>
import modalConsole from "../templates/modal/console";
import sideNav from "../templates/modal/side-nav";

export default {
    components: {
        "modal-console": modalConsole,
        "side-nav": sideNav
    },
    data() {
        return {
            objFiles: null
        }
    }
};
</script>

<style scoped>
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

.objFileItem:hover { 
    border: 2px solid #006064;
}

</style>