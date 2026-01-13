import { ref, onMounted, onUnmounted } from 'vue'
import { defineStore } from 'pinia'

export const useThemeStore = defineStore('theme', () => {
  const isDark = ref(false)
  
  // 检查系统主题
  const checkSystemTheme = () => {
    isDark.value = window.matchMedia('(prefers-color-scheme: dark)').matches
    updateDocumentClass()
  }
  
  // 更新文档类
  const updateDocumentClass = () => {
    if (isDark.value) {
      document.documentElement.classList.add('dark')
    } else {
      document.documentElement.classList.remove('dark')
    }
  }
  
  // 监听系统主题变化
  const handleSystemThemeChange = (e: MediaQueryListEvent) => {
    isDark.value = e.matches
    updateDocumentClass()
  }
  
  onMounted(() => {
    // 初始检查
    checkSystemTheme()
    
    // 监听系统主题变化
    const mediaQuery = window.matchMedia('(prefers-color-scheme: dark)')
    mediaQuery.addEventListener('change', handleSystemThemeChange)
    
    // 清理函数
    return () => {
      mediaQuery.removeEventListener('change', handleSystemThemeChange)
    }
  })
  
  return {
    isDark
  }
})
