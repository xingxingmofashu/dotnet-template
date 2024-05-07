<script setup lang="ts">
import type { DashboardSidebarMenuLink } from '~/types';
import type { Group } from '#ui/types'

const userStore = useUserStore()

const { links, groups } = storeToRefs(userStore)

/* const links = reactive<DashboardSidebarMenuLink[]>([
  {
    id: 'home',
    label: 'Home',
    icon: 'i-heroicons-home',
    to: '/',
    tooltip: {
      text: 'Home',
      shortcuts: ['G', 'H']
    }
  }, {
    id: 'PersonnelManagement',
    label: 'Personnel Management',
    to: '/PersonnelManagement',
    icon: 'i-heroicons-cog-8-tooth',
    children: [{
      label: 'Users',
      to: '/PersonnelManagement/users',
      exact: true
    }],
    tooltip: {
      text: 'Personnel Management',
      shortcuts: ['G', 'S']
    }
  }, {
    id: 'SystemSettings',
    label: 'System Settings',
    to: '/SystemSettings',
    icon: 'i-heroicons-cog-8-tooth',
    children: [{
      label: 'General',
      to: '/SystemSettings',
      exact: true
    }],
    tooltip: {
      text: 'System Settings',
      shortcuts: ['G', 'S']
    }
  }])

const groups = reactive<Group[]>([{
  key: 'links',
  label: 'Go to',
  links: links.map(link => ({ ...link, shortcuts: link.tooltip?.shortcuts }))
}]) */

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
