import { createApp } from 'vue'
import { createPinia } from 'pinia'
import { library } from '@fortawesome/fontawesome-svg-core'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { 
  faUser, faTrophy, faQuestionCircle, faStore, 
  faStar, faPlus, faBars, faTimes,
  faCircle, faCheckCircle, faChevronRight, faChevronLeft,
  faCircleDot, faSquare, faPlay, faPause, faArrowRight,
  faHeart, faBolt, faSmile, faHospital, faCoins,
  faChartLine, faMedal, faBrain, faDumbbell, faUsers,
  faCog, faBookOpen, faPalette, faBook
} from '@fortawesome/free-solid-svg-icons'

import App from './App.vue'
import router from './router'
import './assets/css/main.css'

// 添加所有图标到库（移除了moon和sun）
library.add(
  faUser, faTrophy, faQuestionCircle, faStore, 
  faStar, faPlus, faBars, faTimes,
  faCircle, faCheckCircle, faChevronRight, faChevronLeft,
  faCircleDot, faSquare, faPlay, faPause, faArrowRight,
  faHeart, faBolt, faSmile, faHospital, faCoins,
  faChartLine, faMedal, faBrain, faDumbbell, faUsers,
  faCog, faBookOpen, faPalette, faBook
)

const app = createApp(App)
const pinia = createPinia()

// 全局注册FontAwesome图标组件
app.component('font-awesome-icon', FontAwesomeIcon)

app.use(pinia)
app.use(router)
app.mount('#app')