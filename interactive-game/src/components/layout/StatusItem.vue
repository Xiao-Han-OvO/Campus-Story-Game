<template>
  <div class="space-y-1">
    <div class="flex justify-between text-sm">
      <span class="text-gray-600 dark:text-gray-400 flex items-center">
        <font-awesome-icon :icon="icon" class="w-4 h-4 mr-2" />
        {{ name }}
      </span>
      <span v-if="showNumber" class="text-gray-800 dark:text-gray-200 font-medium">
        {{ value }}/{{ max }}
      </span>
    </div>
    <div class="h-2 bg-gray-200 dark:bg-gray-700 rounded-full overflow-hidden">
      <div 
        :class="['h-full rounded-full transition-all duration-300', colorClass]" 
        :style="{ width: `${(value / max) * 100}%` }"
      ></div>
    </div>
    <div v-if="!showNumber" class="text-right text-xs text-gray-500 dark:text-gray-400">
      {{ Math.round((value / max) * 100) }}%
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed } from 'vue'

const props = defineProps<{
  name: string
  value: number
  max: number
  color: string
  icon: string
  showNumber?: boolean
}>()

const colorClass = computed(() => {
  // 将颜色字符串转换为Tailwind类
  switch (props.color) {
    case 'bg-red-500': return 'bg-red-500'
    case 'bg-blue-500': return 'bg-blue-500'
    case 'bg-green-500': return 'bg-green-500'
    case 'bg-purple-500': return 'bg-purple-500'
    case 'bg-yellow-500': return 'bg-yellow-500'
    case 'bg-indigo-500': return 'bg-indigo-500'
    case 'bg-pink-500': return 'bg-pink-500'
    default: return 'bg-gray-500'
  }
})
</script>
