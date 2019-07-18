import Repository from './Repository'

const resource = "/arguments";
export default {
    get() {
        return Repository.get(`${resource}`)
    }
}
