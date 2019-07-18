import ArgumentsRepository from './argumentsRepository'

const repositories = {
    arguments: ArgumentsRepository
};
export default {
    get: name => repositories[name]
};