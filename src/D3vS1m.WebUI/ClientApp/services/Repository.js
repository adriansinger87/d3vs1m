import axios from "axios"
//TODO: add to GLOBAL VARIBLE deploy dymatically
const baseDomain = "http://localhost:62363"
const baseURL = `${baseDomain}/api`

export default axios.create({
    baseURL
})
