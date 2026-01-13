<template>
  <main class="flex-1 bg-gray-50 dark:bg-gray-900 overflow-y-auto relative">
    <!-- 游戏信息显示区域 -->
    <div class="p-6 max-w-3xl mx-auto">
      <!-- 当前任务信息 -->
      <div class="mb-8">
        <div class="flex items-center mb-4">
          <font-awesome-icon icon="circle" class="w-3 h-3 text-blue-500 mr-2" />
          <h2 class="text-xl font-bold text-gray-800 dark:text-gray-200">当前进行中：高一新生报到</h2>
        </div>
        
        <!-- 游戏文本内容 -->
        <div class="space-y-4">
          <div v-for="(message, index) in messages" :key="index" 
               class="bg-white dark:bg-gray-800 rounded-lg p-4 shadow-sm hover:shadow-md transition-shadow">
            <p class="text-gray-700 dark:text-gray-300 leading-relaxed">{{ message }}</p>
          </div>
        </div>
      </div>
      
      <!-- 选项区域 -->
      <div v-if="showChoices" class="fixed bottom-0 left-0 right-0 bg-white dark:bg-gray-800 border-t border-gray-200 dark:border-gray-700 p-4 shadow-lg">
        <div class="max-w-3xl mx-auto">
          <h3 class="text-lg font-semibold mb-3 text-gray-800 dark:text-gray-200">请选择：</h3>
          <div class="space-y-2">
            <button 
              v-for="(choice, index) in choices" 
              :key="index"
              @click="selectChoice(choice)"
              class="w-full text-left p-3 rounded-lg hover:bg-gray-100 dark:hover:bg-gray-700 flex items-center transition-colors"
            >
              <font-awesome-icon icon="circle" class="w-5 h-5 text-gray-400 dark:text-gray-500 mr-3 flex-shrink-0" />
              <span class="text-gray-700 dark:text-gray-300">{{ choice.text }}</span>
            </button>
          </div>
        </div>
      </div>
      
      <!-- 新消息输入（开发用） -->
      <div class="mt-8 p-4 bg-white dark:bg-gray-800 rounded-lg shadow">
        <h3 class="font-medium text-gray-800 dark:text-gray-200 mb-2">添加游戏消息（开发测试）</h3>
        <div class="flex space-x-2">
          <input 
            v-model="newMessage" 
            @keyup.enter="addMessage"
            placeholder="输入游戏消息..."
            class="flex-1 px-3 py-2 border border-gray-300 dark:border-gray-600 rounded-lg bg-transparent focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
          <button @click="addMessage" class="px-4 py-2 bg-blue-500 text-white rounded-lg hover:bg-blue-600 transition-colors flex items-center">
            <font-awesome-icon icon="plus" class="w-4 h-4" />
            <span class="ml-2">添加</span>
          </button>
        </div>
      </div>
    </div>
  </main>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'

interface Choice {
  id: number
  text: string
  consequence?: string
}

// 更新为高中背景的消息
const messages = ref<string[]>([
  '欢迎来到高中校园！你是一名刚刚入学的高一新生，站在校门口，看着熟悉又陌生的环境。',
  '校园广播正在播放欢迎新生的通知，你看到前方有一个指示牌指向"新生报到处"。',
  '你需要完成新生报到流程，领取学生证和班级分配通知。',
  '班主任提醒：高中三年是关键时期，要合理规划学习和生活。'
])

// 更新为高中背景的选择
const choices = ref<Choice[]>([
  { id: 1, text: '直接前往新生报到处', consequence: '快速完成注册，获得经验值' },
  { id: 2, text: '先和同学交流认识', consequence: '提升社交能力，可能获得新朋友' },
  { id: 3, text: '查看校园地图', consequence: '熟悉环境，后续行动更高效' }
])

const showChoices = ref(true)
const newMessage = ref('')

const selectChoice = (choice: Choice) => {
  messages.value.push(`你选择了：${choice.text}`)
  if (choice.consequence) {
    messages.value.push(`结果：${choice.consequence}`)
  }
  showChoices.value = false
  // 模拟服务器响应
  setTimeout(() => {
    messages.value.push('系统：任务进度已更新。')
    showChoices.value = true
  }, 1000)
}

const addMessage = () => {
  if (newMessage.value.trim()) {
    messages.value.push(newMessage.value)
    newMessage.value = ''
  }
}

onMounted(() => {
  // 模拟游戏初始化
  setTimeout(() => {
    messages.value.push('系统提示：高中生活充满挑战和机遇，合理分配时间很重要。')
  }, 2000)
})
</script>
