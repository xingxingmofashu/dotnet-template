<script setup lang="ts">
const links = [{
  id: 'home',
  label: 'Home',
  icon: 'i-heroicons-home',
  to: '/',
  tooltip: {
    text: 'Home',
    shortcuts: ['G', 'H']
  }
}, {
  id: 'users',
  label: 'Users',
  icon: 'i-heroicons-user-group',
  to: '/users',
  tooltip: {
    text: 'Users',
    shortcuts: ['G', 'U']
  }
}]

const groups = [{
  key: 'links',
  label: 'Go to',
  commands: links.map(link => ({ ...link, shortcuts: link.tooltip?.shortcuts }))
}]
</script>

<template>
  <UDashboardLayout>
    <UDashboardPanel :width="250" :resizable="{ min: 200, max: 300 }" collapsible>
      <UDashboardNavbar class="!border-transparent" :ui="{ left: 'flex-1' }">
        <template #left>
          <UButton color="gray" variant="ghost" :class="['bg-gray-50 dark:bg-gray-800']" class="w-full">
            <UAvatar src="https://avatars.githubusercontent.com/u/23360933?s=200&v=4" size="2xs" />
            <span class="truncate text-gray-900 dark:text-white font-semibold">Nuxt</span>
          </UButton>
        </template>
      </UDashboardNavbar>
      <UDashboardSidebar>
        <template #header>
          <UDashboardSearchButton />
        </template>

        <UDashboardSidebarLinks :links="links" />

        <UDivider />
        <div class="flex-1" />

      </UDashboardSidebar>
    </UDashboardPanel>
    <UDashboardPage>
      <UDashboardPanel grow>
        <UDashboardNavbar>
          <template #right>
            <Header />
          </template>
        </UDashboardNavbar>
        <UDashboardPanelContent>
          <slot />
        </UDashboardPanelContent>
      </UDashboardPanel>
    </UDashboardPage>
    <ClientOnly>
      <LazyUDashboardSearch :groups="groups" />
    </ClientOnly>
  </UDashboardLayout>
</template>
