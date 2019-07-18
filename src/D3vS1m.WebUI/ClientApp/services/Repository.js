import axios from "axios"

const baseDomain = "http://localhost:51410"
const baseURL = `${baseDomain}/api`

export default axios.create({
    baseURL
})
