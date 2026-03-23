import './assets/main.css'

import { createApp } from 'vue'
import VueHex from 'vuehex'
import 'vuehex/style.css'

import App from './App.vue'

const app = createApp(App);
app.use(VueHex);
app.mount('#app');
