import type { DashboardSidebarMenuLink } from "~/types";
import type { Group } from '#ui/types'

export const useUserStore = defineStore('user', () => {
    const links = reactive<DashboardSidebarMenuLink[]>([
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
            id: 'settings',
            label: 'Settings',
            to: '/settings',
            icon: 'i-heroicons-cog-8-tooth',
            children: [{
                label: 'General',
                to: '/settings',
                exact: true
            }],
            tooltip: {
                text: 'Settings',
                shortcuts: ['G', 'S']
            }
        }])

    const groups = reactive<Group[]>([{
        key: 'links',
        label: 'Go to',
        links: links.map(link => ({ ...link, shortcuts: link.tooltip?.shortcuts }))
    }])

    return {
        links,
        groups
    }
})