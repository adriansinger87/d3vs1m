import Repository from './Repository'

const resource = "/objfilemanager/";

import EventBus from './EventBus/EventBus'

const config = {
    onUploadProgress: function(progressEvent) {
        var percentCompleted = parseInt( Math.round( ( progressEvent.loaded * 100 ) / progressEvent.total ) ) 
        EventBus.$emit('onProLoadPercentageChange', percentCompleted) 
        console.log(percentCompleted)
    },
    headers: { 'Content-Type': 'multipart/form-data' }
}



export default {
    get() {
        return Repository.get(`${resource}`)
    },
    uploadFile(data) {
        return Repository.post(`${resource}` + "/uploadfile", data, config)
    }

}
