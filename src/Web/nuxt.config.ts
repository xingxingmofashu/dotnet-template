// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  devtools: { enabled: false },
  modules: [
    "@pinia/nuxt",
    "@vueuse/nuxt",
    "@nuxt/ui"
  ],
  pinia: {
    storesDirs: ['./store/**']
  },
  vite: {
    build: {
      minify: 'esbuild',
      cssMinify: 'esbuild'
    }
  }
})