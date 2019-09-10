import ArgumentsRepository from './argumentsRepository'
import SimulationRepository from "./simulationRepository";
import objFilesRepository from './objFilesRepository';

const repositories = {
    arguments: ArgumentsRepository,
    simulation: SimulationRepository,
    objFiles: objFilesRepository
};
export default {
    get: name => repositories[name]
};