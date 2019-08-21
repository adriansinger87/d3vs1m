import Repository from './Repository'

const resource = "/simulation";

export default {
    get() {
        return Repository.get(`${resource}`)
    },

    runSimulation(guid,data) {
      return Repository.post(`${resource}` + "/run/" + guid, data) 
    }
}