<template>
  <nav class="h-16 bg-white dark:bg-gray-800 border-b border-gray-200 dark:border-gray-700 flex items-center px-4 shadow-sm">
    <!-- 左侧：Logo和图标 -->
    <div class="flex items-center space-x-4 flex-1">
      <!-- Logo (16:9比例) -->
      <div class="w-32 h-9 bg-gradient-to-r from-blue-500 to-purple-600 rounded flex items-center justify-center">
        <span class="text-white font-bold text-lg">校园物语</span>
      </div>
      
      <!-- 导航图标 -->
      <button class="p-2 rounded-lg hover:bg-gray-100 dark:hover:bg-gray-700 transition-colors" title="成就">
        <font-awesome-icon icon="trophy" class="w-5 h-5 text-yellow-500" />
      </button>
      
      <button class="p-2 rounded-lg hover:bg-gray-100 dark:hover:bg-gray-700 transition-colors" title="帮助">
        <font-awesome-icon icon="question-circle" class="w-5 h-5 text-blue-500" />
      </button>
      
      <button class="p-2 rounded-lg hover:bg-gray-100 dark:hover:bg-gray-700 transition-colors" title="商店">
        <font-awesome-icon icon="store" class="w-5 h-5 text-green-500" />
      </button>
    </div>
    
    <!-- 中间：当前任务状态 -->
    <div class="flex-1 text-center">
      <div class="inline-flex items-center bg-blue-50 dark:bg-blue-900/30 px-4 py-2 rounded-full">
        <span class="text-sm font-medium text-blue-600 dark:text-blue-400">
          当前任务：新生报到注册
        </span>
      </div>
    </div>
    
    <!-- 右侧：用户头像 -->
    <div class="flex items-center space-x-4 flex-1 justify-end">
      <button class="relative" title="用户信息">
        <div class="w-10 h-10 rounded-full bg-gradient-to-r from-purple-500 to-pink-500 flex items-center justify-center">
          <font-awesome-icon icon="user" class="w-5 h-5 text-white" />
        </div>
        <!-- 在线状态指示器 -->
        <div class="absolute bottom-0 right-0 w-3 h-3 bg-green-500 rounded-full border-2 border-white dark:border-gray-800"></div>
      </button>
    </div>
  </nav>
</template>

<script setup lang="ts">
</script>
