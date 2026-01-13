<template>
  <aside class="w-64 bg-white dark:bg-gray-800 shadow-lg flex flex-col h-full border-l border-gray-200 dark:border-gray-700">
    <!-- 状态栏标题和折叠按钮 -->
    <div class="p-4 border-b border-gray-200 dark:border-gray-700 flex items-center justify-between">
      <h2 class="text-lg font-semibold text-gray-800 dark:text-gray-200">状态面板</h2>
      <button @click="$emit('toggle-collapse')" class="lg:hidden p-1 rounded-md hover:bg-gray-100 dark:hover:bg-gray-700">
        <font-awesome-icon icon="chevron-right" class="w-5 h-5 text-gray-600 dark:text-gray-400" />
      </button>
    </div>
    
    <!-- 状态列表 -->
    <div class="flex-1 overflow-y-auto p-4 space-y-6">
      <!-- 基础状态 -->
      <div>
        <h3 class="font-medium text-gray-700 dark:text-gray-300 mb-3">基础状态</h3>
        <div class="space-y-4">
          <StatusItem 
            v-for="stat in basicStats" 
            :key="stat.id"
            :name="stat.name" 
            :value="stat.value" 
            :max="stat.max"
            :color="stat.color"
            :icon="stat.icon"
          />
        </div>
      </div>
      
      <!-- 资源状态 -->
      <div>
        <h3 class="font-medium text-gray-700 dark:text-gray-300 mb-3">资源</h3>
        <div class="space-y-4">
          <StatusItem 
            v-for="stat in resourceStats" 
            :key="stat.id"
            :name="stat.name" 
            :value="stat.value" 
            :max="stat.max"
            :color="stat.color"
            :icon="stat.icon"
            :show-number="true"
          />
        </div>
      </div>
      
      <!-- 属性状态 -->
      <div>
        <h3 class="font-medium text-gray-700 dark:text-gray-300 mb-3">属性</h3>
        <div class="grid grid-cols-2 gap-3">
          <div v-for="attr in attributes" :key="attr.id" class="text-center p-3 bg-gray-50 dark:bg-gray-900 rounded-lg hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors">
            <font-awesome-icon :icon="attr.icon" class="w-6 h-6 mb-2 text-blue-500 dark:text-blue-400" />
            <div class="text-sm text-gray-600 dark:text-gray-400 mb-1">{{ attr.name }}</div>
            <div class="font-bold text-gray-800 dark:text-gray-200">{{ attr.value }}</div>
          </div>
        </div>
      </div>
    </div>
  </aside>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import StatusItem from './StatusItem.vue'

defineEmits(['toggle-collapse'])

interface Status {
  id: number
  name: string
  value: number
  max: number
  color: string
  icon: string
}

interface Attribute {
  id: number
  name: string
  value: number
  icon: string
}

const basicStats = ref<Status[]>([
  { id: 1, name: '生命', value: 85, max: 100, color: 'bg-red-500', icon: 'heart' },
  { id: 2, name: '精力', value: 70, max: 100, color: 'bg-blue-500', icon: 'bolt' },
  { id: 3, name: '心态', value: 90, max: 100, color: 'bg-green-500', icon: 'smile' },
  { id: 4, name: '健康', value: 95, max: 100, color: 'bg-purple-500', icon: 'hospital' }
])

const resourceStats = ref<Status[]>([
  { id: 5, name: '金钱', value: 1500, max: 10000, color: 'bg-yellow-500', icon: 'coins' },
  { id: 6, name: '经验', value: 350, max: 1000, color: 'bg-indigo-500', icon: 'chart-line' },
  { id: 7, name: '声望', value: 30, max: 100, color: 'bg-pink-500', icon: 'medal' }
])

const attributes = ref<Attribute[]>([
  { id: 1, name: '智力', value: 75, icon: 'brain' },
  { id: 2, name: '体力', value: 65, icon: 'dumbbell' },
  { id: 3, name: '社交', value: 80, icon: 'users' },
  { id: 4, name: '效率', value: 70, icon: 'cog' },
  { id: 5, name: '创造力', value: 85, icon: 'palette' },
  { id: 6, name: '学习力', value: 90, icon: 'book' }
])
</script>
