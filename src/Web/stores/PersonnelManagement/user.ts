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
        },{
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
    }])

    return {
        links,
        groups
    }
})