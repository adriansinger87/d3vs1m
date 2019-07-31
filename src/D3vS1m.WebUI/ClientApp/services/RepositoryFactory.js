import ArgumentsRepository from './argumentsRepository'
import SimulationRepository from "./simulationRepository";

const repositories = {
    arguments: ArgumentsRepository,
    simulation: SimulationRepository
};
export default {
    get: name => repositories[name]
};