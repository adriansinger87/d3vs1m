import Repository from './Repository'

const resource = "/objfilemanager/";

export default {
    get() {
        return Repository.get(`${resource}`)
    },
}
