import axios from "axios"
//TODO: add to GLOBAL VARIBLE deploy dymatically
const baseDomain = "http://localhost:51410"
const baseURL = `${baseDomain}/api`

export default axios.create({
    baseURL
})
