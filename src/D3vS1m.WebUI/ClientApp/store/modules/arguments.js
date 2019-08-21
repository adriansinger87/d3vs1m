// import repository 
import RepositoryFactory from "../../services/RepositoryFactory";
const argumentsRepository = RepositoryFactory.get("arguments");

const state = {
    arguments: []
}


const getters = {
    allArguments: state => state.arguments
}

const actions = {
    GET_ARG: async(context,payload) => {
        const {data} = await argumentsRepository.get()
        context.commit('setArg', data)
    }
}

const mutations = {
    setArg: async (state, data) => {
        state.arguments = data
    }
}

export default {
    state,
    getters,
    actions,
    mutations
}






