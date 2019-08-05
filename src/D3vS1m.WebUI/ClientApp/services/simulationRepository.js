import Repository from './Repository'

const resource = "/simulation";
let axiosConfig = {
    "Content-Type": "application/json;charset=UTF-8",
    "Access-Control-Allow-Origin": "*",
    "Access-Control-Allow-Methods": "GET, POST, PATCH, PUT, DELETE, OPTIONS",
    "Access-Control-Allow-Headers": "Origin, Content-Type, X-Auth-Token"
}

const config = {
  headers: {
   "Content-Type": "application/json",
     "Access-Control-Allow-Origin": "*",
    "Access-Control-Allow-Methods": "GET, POST, PATCH, PUT, DELETE, OPTIONS",
    "Access-Control-Allow-Headers": "Origin, Content-Type, X-Auth-Token"
  },
  data: {},
};



//Repository.defaults.headers['Content-Type'] = "application/json;charset=UTF-8"
// 
export default {
    get() {
        return Repository.get(`${resource}`)
    },

    runSimulation(guid, data) {
        console.log(data)
            return Repository.post(`${resource}` + "/test", data) 
    }
}