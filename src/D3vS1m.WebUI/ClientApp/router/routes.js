import HomePage from '../components/pages/home'
import ScenePage from '../components/pages/scene'

export const routes = [
  { name: 'home', path: '/', component: HomePage, display: 'Home', icon: 'home' },
  { name: 'scene', path: '/scene', component: ScenePage, display: '3D Scene', icon: 'home' },
]
