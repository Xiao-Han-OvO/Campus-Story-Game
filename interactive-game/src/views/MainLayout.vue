<template>
  <div class="min-h-screen bg-gray-50 dark:bg-gray-900 text-gray-800 dark:text-gray-200 transition-colors duration-300">
    <NavBar />
    <div class="game-layout relative">
      <!-- 左侧任务栏 -->
      <div :class="['sidebar', isTaskbarCollapsed ? 'sidebar-collapsed' : '']">
        <TaskBar 
          @toggle-collapse="toggleTaskbar"
          @start-task="handleStartTask"
          @show-reward="showRewardModal"
        />
      </div>
      
      <!-- 任务栏展开按钮 -->
      <button 
        v-if="isTaskbarCollapsed"
        @click="toggleTaskbar"
        class="absolute left-0 top-1/2 transform -translate-y-1/2 bg-white dark:bg-gray-800 p-2 rounded-r-lg shadow-md z-10 hover:bg-gray-100 dark:hover:bg-gray-700 transition-colors"
      >
        <font-awesome-icon icon="chevron-right" class="w-5 h-5 text-gray-600 dark:text-gray-400" />
      </button>
      
      <!-- 中间信息栏 -->
      <InfoBar />
      
      <!-- 右侧状态栏 -->
      <div :class="['sidebar', isStatusbarCollapsed ? 'sidebar-collapsed' : '']">
        <StatusBar @toggle-collapse="toggleStatusbar" />
      </div>
      
      <!-- 状态栏展开按钮 -->
      <button 
        v-if="isStatusbarCollapsed"
        @click="toggleStatusbar"
        class="absolute right-0 top-1/2 transform -translate-y-1/2 bg-white dark:bg-gray-800 p-2 rounded-l-lg shadow-md z-10 hover:bg-gray-100 dark:hover:bg-gray-700 transition-colors"
      >
        <font-awesome-icon icon="chevron-left" class="w-5 h-5 text-gray-600 dark:text-gray-400" />
      </button>
      
      <!-- 奖励模态框 -->
      <div v-if="showModal" class="modal-overlay" @click="closeModal">
        <div class="fixed inset-0 flex items-center justify-center p-4">
          <div class="bg-white dark:bg-gray-800 rounded-xl shadow-2xl max-w-md w-full p-6" @click.stop>
            <div class="flex justify-between items-center mb-4">
              <h3 class="text-xl font-bold text-gray-800 dark:text-gray-200">任务奖励</h3>
              <button @click="closeModal" class="p-1 rounded-lg hover:bg-gray-100 dark:hover:bg-gray-700 transition-colors">
                <font-awesome-icon icon="times" class="w-6 h-6" />
              </button>
            </div>
            <div class="space-y-4">
              <div v-if="selectedTask" class="p-4 bg-gradient-to-r from-yellow-50 to-orange-50 dark:from-yellow-900/20 dark:to-orange-900/20 rounded-lg">
                <h4 class="font-bold text-lg text-gray-800 dark:text-gray-200 mb-2">{{ selectedTask.title }}</h4>
                <div class="flex items-center text-yellow-600 dark:text-yellow-400 mb-3">
                  <font-awesome-icon icon="star" class="w-5 h-5 mr-2" />
                  <span class="font-medium">奖励：</span>
                </div>
                <p class="text-gray-700 dark:text-gray-300">{{ selectedTask.reward }}</p>
              </div>
              <div class="text-sm text-gray-500 dark:text-gray-400">
                完成任务可以获得经验值和资源奖励，提升角色属性。
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, onUnmounted } from 'vue'
import NavBar from '@/components/layout/NavBar.vue'
import TaskBar from '@/components/layout/TaskBar.vue'
import InfoBar from '@/components/layout/InfoBar.vue'
import StatusBar from '@/components/layout/StatusBar.vue'

// 响应式状态
const isTaskbarCollapsed = ref(false)
const isStatusbarCollapsed = ref(false)
const showModal = ref(false)
const selectedTask = ref<any>(null)

// 切换函数
const toggleTaskbar = () => {
  isTaskbarCollapsed.value = !isTaskbarCollapsed.value
}

const toggleStatusbar = () => {
  isStatusbarCollapsed.value = !isStatusbarCollapsed.value
}

// 处理任务开始
const handleStartTask = (task: any) => {
  console.log('开始任务:', task)
  // 这里可以添加任务开始的逻辑
}

// 显示奖励模态框
const showRewardModal = (task: any) => {
  selectedTask.value = task
  showModal.value = true
}

const closeModal = () => {
  showModal.value = false
  selectedTask.value = null
}

// 响应式布局：根据窗口宽度自动收起侧边栏
const checkWindowSize = () => {
  const width = window.innerWidth
  if (width < 1024) { // lg 断点
    isTaskbarCollapsed.value = true
    isStatusbarCollapsed.value = true
  } else {
    isTaskbarCollapsed.value = false
    isStatusbarCollapsed.value = false
  }
}

onMounted(() => {
  checkWindowSize()
  window.addEventListener('resize', checkWindowSize)
})

onUnmounted(() => {
  window.removeEventListener('resize', checkWindowSize)
})
</script>

<style scoped>
.modal-overlay {
  @apply fixed inset-0 bg-black/50 backdrop-blur-sm z-50 flex items-center justify-center;
}
</style>
