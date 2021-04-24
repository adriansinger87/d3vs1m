# API


|  Method | Route                   | Description |
| ------- | ----------------------- | ------------|
| `GET`   | /arguments              | gets all existing arguments that will instanciate simulation models
| `GET`   | /arguments/:key         | gets a single arguments object based on the key identifier 
| `PUT`   | /arguments/:key         | updates an existing arguments object
| `PUT`   | /arguments/:key/upload  | file upload for a specific arguments object
| `GET`   | /runtime                | gets the status of the simulation runtime
| `POST`  | /runtime/start          | starts the simulation
| `POST`  | /runtime/stop           | stops the simulation
