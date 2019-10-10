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
    },
    UPDATE: (context, updatedArg)  => {
        console.log("cc")
        console.log(context.state.arguments)
        console.log(updatedArg)
        var updatedElementIndex = context.state.arguments.findIndex(x => x.guid == updatedArg[0].guid) 
        console.log(updatedElementIndex)
        var updatedData = context.state.arguments
        updatedData[updatedElementIndex] = updatedArg[0]
        context.commit('setArg', updatedData)
        
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






