<template>
  <aside class="w-64 bg-white dark:bg-gray-800 shadow-lg flex flex-col h-full border-r border-gray-200 dark:border-gray-700">
    <!-- 任务栏标题和折叠按钮 -->
    <div class="p-4 border-b border-gray-200 dark:border-gray-700 flex items-center justify-between">
      <h2 class="text-lg font-semibold text-gray-800 dark:text-gray-200">任务列表</h2>
      <button @click="$emit('toggle-collapse')" class="lg:hidden p-1 rounded-md hover:bg-gray-100 dark:hover:bg-gray-700">
        <font-awesome-icon icon="chevron-left" class="w-5 h-5 text-gray-600 dark:text-gray-400" />
      </button>
    </div>
    
    <!-- 任务列表 -->
    <div class="flex-1 overflow-y-auto p-4 space-y-4">
      <!-- 主线任务 -->
      <div>
        <h3 class="font-medium text-gray-700 dark:text-gray-300 mb-2">主线任务</h3>
        <div class="space-y-3">
          <div v-for="task in mainTasks" :key="task.id" class="task-item">
            <div class="flex justify-between items-start mb-2">
              <h4 class="font-medium text-gray-800 dark:text-gray-200">{{ task.title }}</h4>
              <button @click="showReward(task)" class="text-gray-400 hover:text-yellow-500">
                <font-awesome-icon icon="star" class="w-4 h-4" />
              </button>
            </div>
            <div class="flex justify-between items-center">
              <span class="text-sm text-gray-500 dark:text-gray-400">{{ task.status }}</span>
              <button 
                @click="startTask(task)"
                :disabled="currentTask || task.status !== '可开始'"
                class="w-8 h-8 rounded-full bg-blue-500 text-white flex items-center justify-center hover:bg-blue-600 disabled:opacity-50 disabled:cursor-not-allowed"
              >
                <font-awesome-icon icon="plus" class="w-4 h-4" />
              </button>
            </div>
          </div>
        </div>
      </div>
      
      <!-- 支线任务 -->
      <div>
        <h3 class="font-medium text-gray-700 dark:text-gray-300 mb-2">支线任务</h3>
        <div class="space-y-3">
          <div v-for="task in sideTasks" :key="task.id" class="task-item">
            <div class="flex justify-between items-start mb-2">
              <h4 class="font-medium text-gray-800 dark:text-gray-200">{{ task.title }}</h4>
              <button @click="showReward(task)" class="text-gray-400 hover:text-yellow-500">
                <font-awesome-icon icon="star" class="w-4 h-4" />
              </button>
            </div>
            <div class="flex justify-between items-center">
              <span class="text-sm text-gray-500 dark:text-gray-400">{{ task.status }}</span>
              <button 
                @click="startTask(task)"
                :disabled="currentTask || task.status !== '可开始'"
                class="w-8 h-8 rounded-full bg-green-500 text-white flex items-center justify-center hover:bg-green-600 disabled:opacity-50 disabled:cursor-not-allowed"
              >
                <font-awesome-icon icon="plus" class="w-4 h-4" />
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </aside>
</template>

<script setup lang="ts">
import { ref, defineEmits } from 'vue'

const emit = defineEmits(['toggle-collapse', 'start-task', 'show-reward'])

interface Task {
  id: number
  title: string
  status: string
  reward?: string
}

const currentTask = ref<Task | null>(null)

// 更新为高中背景的任务
const mainTasks = ref<Task[]>([
  { id: 1, title: '高一新生报到', status: '进行中', reward: '经验值+100，适应力+10' },
  { id: 2, title: '熟悉校园环境', status: '可开始', reward: '经验值+50，精力+10' },
  { id: 3, title: '参加开学典礼', status: '未解锁', reward: '经验值+200，声望+20' },
  { id: 4, title: '期中考试复习', status: '未解锁', reward: '智力+15，学习力+20' }
])

// 更新为高中背景的支线任务（包括竞赛）
const sideTasks = ref<Task[]>([
  { id: 5, title: '数学竞赛报名', status: '可开始', reward: '智力+20，竞赛经验+30' },
  { id: 6, title: '参加篮球社团', status: '可开始', reward: '体力+15，社交+10' },
  { id: 7, title: '科学实验项目', status: '可开始', reward: '创造力+25，经验值+100' },
  { id: 8, title: '志愿者活动', status: '可开始', reward: '声望+15，社交+10' }
])

const startTask = (task: Task) => {
  if (task.status === '可开始') {
    currentTask.value = task
    emit('start-task', task)
  }
}

const showReward = (task: Task) => {
  emit('show-reward', task)
}
</script>

<style scoped>
.task-item {
  @apply bg-white dark:bg-gray-800 border border-gray-200 dark:border-gray-700 rounded-lg p-3 shadow-sm hover:shadow-md transition-all duration-200;
}
</style>
