<template>
    <nav>
        <div class="nav-wrapper container">
            <ul class="left">
                <li>
                    <a v-on:click="openSideNav" id="side-nav-btn" data-activates="slide-out" class="pointer left" style="width: 55px;">
                        <i class="mdi mdi-menu"></i>
                    </a>
                </li>
            </ul>

            <a class="brand-logo center" href="#">
                <router-link :to="{ name: 'home' }">
                    <img class="header-logo left" src="../images/d3vs1m_logo.svg" />
                    <span class="hide-on-small-only">D3V S1M</span>
                </router-link>
            </a>

            <ul class="right">
                <li>
                    <a v-on:click="startSimulation" class="waves-effect waves-light" href="#console-modal">
                        <i class="mdi mdi-play right"></i> Start
                    </a>
                </li>
                <li>
                    <a id="console-link" class="modal-trigger waves-effect waves-light" href="#console-modal">
                        <i class="mdi mdi-console left"></i> Console
                    </a>
                </li>
                <li>
                    <router-link active-class="router-link-active-3d" :to="{ name: 'scene' }"><i class="mdi mdi-cube-outline left"></i> 3D Scene </router-link>
                </li>
            </ul>
        </div>
    </nav>
</template>

<script>

    import RepositoryFactory from "../services/RepositoryFactory";
    const simulationRepository = RepositoryFactory.get("simulation")

    // info: Service import
    import mqttClient from "../services/mqttClient"

    import utils from '../services/ultis'

    export default {
        data() {
            return {};
        },
        mounted() {
        },
        methods: {
            startSimulation: async function (event) {
                // TODO: will create a global function to use and call all MODAL in the page
                // TODO: check why instance.open is failing, because open is undefined 
                var instance = utils.getModalInstance("console-modal")

                instance.open()

                const simulationRes = await simulationRepository.get();

                mqttClient.subscribe(simulationRes.data.guid)


                const { data } = await simulationRepository.runSimulation(this.$store.getters.allArguments)

            },
            openSideNav: function () {
                //var instance = utils.getModalInstance("slide-out")
                var instance = utils.initSideNav()
                instance.open()
            }
        }
    };
</script>

<style scoped lang='css'>
    nav {
        background-color: #006064;
    }

    .header-logo {
        margin-top: 10px;
        margin-right: 10px;
    }

    .router-link-active-3d {
        background-color: rgb(0, 86, 90)
    }
</style>
