// import repository 
import RepositoryFactory from "../../services/RepositoryFactory";
const argumentsRepository = RepositoryFactory.get("arguments");

const state = {
    codedata: ""
}


const getters = {
    codeData: state => state.codedata
}

const actions = {
    GET_CODEDATA: async(context,payload) => {
        const {data} = await argumentsRepository.getID(payload)
        context.commit('setCode', data)
    }
}

const mutations = {
    setCode: async (state, data) => {
        state.codedata = data
    }
}

export default {
    state,
    getters,
    actions,
    mutations
}
