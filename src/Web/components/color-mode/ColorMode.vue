<template>
    <UButton variant="ghost" @click="toggleMode"
        class="focus:outline-none focus-visible:outline-0 disabled:cursor-not-allowed disabled:opacity-75 flex-shrink-0 font-medium rounded-md text-sm gap-x-1.5 p-1.5 text-gray-700 dark:text-gray-200 hover:text-gray-900 dark:hover:text-white hover:bg-gray-50 dark:hover:bg-gray-800 focus-visible:ring-inset focus-visible:ring-2 focus-visible:ring-primary-500 dark:focus-visible:ring-primary-400 inline-flex items-center"
        square :aria-label="`Switch to ${colorMode} mode`">
        <UIcon v-if="colorMode.preference === 'dark'" name="i-heroicons-moon-20-solid" class="flex-shrink-0 h-5 w-5" />
        <UIcon v-else name="i-heroicons-sun-20-solid" class="flex-shrink-0 h-5 w-5" />
    </UButton>
</template>
<script setup lang="ts">
const colorMode = useColorMode()
function toggleMode() {
    colorMode.preference = colorMode.value === 'light' ? 'dark' : 'light'
}
</script>