<template>
  <UHeader :links="links">
    <template #right>
      <ColorPicker />
      <UColorModeButton />
      <UButton to="https://github.com/xingxingmofashu/XBoot" target="_blank" icon="i-simple-icons-github"
        aria-label="GitHub" v-bind="($ui.button.secondary as any)" />
    </template>
  </UHeader>
</template>
<script setup lang="ts">
import type { HeaderLink } from '~/types/ui';

defineProps<{
  links?: HeaderLink[]
}>();
</script>