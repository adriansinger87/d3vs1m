import Repository from './Repository'

const resource = "/simulation";

export default {
    get() {
        return Repository.get(`${resource}`)
    },

    runSimulation(guid) {
        return Repository.get(`${resource}` + "/run/" + guid)
    }
}