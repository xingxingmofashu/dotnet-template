import type { User } from "~/types/user";

export const useUserStore = defineStore('user', () => {
    const isAuthenticated = ref(false);
    const user=ref<User>();

    return {
        isAuthenticated,
        user
    }
})

if (import.meta.hot) {
    import.meta.hot.accept(acceptHMRUpdate(useUserStore, import.meta.hot))
}