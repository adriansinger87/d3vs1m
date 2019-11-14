import Repository from './Repository'

const resource = "/simulation";

export default {
    get() {
        return Repository.get(`${resource}`)
    },

    runSimulation(data) {
      return Repository.post(`${resource}` + "/run/" + data) 
    }
}