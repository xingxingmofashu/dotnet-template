// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  devtools: { enabled: false },
  components: {
    dirs: [{
      path: '~/components/ui',
      prefix: 'U',
      pathPrefix: false
    }, "~/components"]
  },
  modules: ["@pinia/nuxt", "@vueuse/nuxt", "@nuxt/ui", "@nuxtjs/tailwindcss", '@nuxt/eslint'],
  tailwindcss: {
    configPath: './tailwind.config.ts',
  },
  css: ['./assets/css/main.css', './assets/css/scrollbars.css'],
  pinia: {
    storesDirs: ['./store/**']
  },
  ui: {
    global: true,
    icons: ['heroicons', 'simple-icons'],
  },
  colorMode: {
    preference: 'light',
  },
  vite: {
    build: {
      minify: 'esbuild',
      cssMinify: 'esbuild'
    }
  },
  eslint: {
    config: {
      stylistic: {
        commaDangle: 'never',
        braceStyle: '1tbs'
      }
    }
  }
})