<script setup lang="ts">
const userStore = useUserStore()

const { links, groups } = storeToRefs(userStore)
</script>

<template>
  <UDashboardLayout>
    <UDashboardPanel :width="250" :resizable="{ min: 200, max: 300 }" collapsible>
      <UDashboardNavbar class="!border-transparent" :ui="{ left: 'flex-1' }">
        <template #left>
          <div class="w-full">
            <UButton color="gray" to="/" variant="ghost" class="w-full">
              <UAvatar src="https://avatars.githubusercontent.com/u/23360933?s=200&v=4" size="2xs" />
              <span class="truncate text-gray-900 dark:text-white font-semibold">Nuxt</span>
            </UButton>
          </div>
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
