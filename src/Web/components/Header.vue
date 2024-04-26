<template>
  <UHeader :links="links" :class="{
    'border-primary-200/75 dark:border-primary-900/50': $route.path === '/',
    'border-gray-200 dark:border-gray-800': $route.path !== '/'
  }">
    <template #left>
      <NuxtLink to="/" class="flex items-end gap-2 font-bold text-xl text-gray-900 dark:text-white" aria-label="XBoot">
        XBoot
      </NuxtLink>
    </template>
    <template #right>
      <ColorPicker />
      <UColorModeButton />
      <UButton to="https://github.com/xingxingmofashu/XBoot" target="_blank" icon="i-simple-icons-github" aria-label="GitHub"
        v-bind="($ui.button.secondary as any)" />
    </template>
  </UHeader>
</template>
<script setup lang="ts">
import type { HeaderLink } from '~/types/ui';

defineProps<{
  links?: HeaderLink[]
}>();
</script>