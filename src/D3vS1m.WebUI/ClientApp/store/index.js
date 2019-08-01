import Vue from 'vue'
import Vuex from 'vuex'
//import arguments from "./modules/arguments";
import args from './modules/arguments'
Vue.use(Vuex)

export default new Vuex.Store({
    
  state: {},
  getters : {},
  mutations: {},
  actions:{},
  modules: {
    //arguments
    args
  }
})
