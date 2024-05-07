// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  devtools: { enabled: true },
  components: {
    dirs: [{
      path: '~/components/ui',
      prefix: 'U',
      pathPrefix: false
    }, '~/components']
  },
  imports: {
    dirs: ['composables', 'composables/ui/**'],
  },
  plugins: [
    '~/plugins/ui/presets.ts',
    '~/plugins/ui/variables.ts',
    {
      mode: 'client',
      src: '~/plugins/ui/scrollbars.client.ts',
    }
  ],
  modules: [
    '@pinia/nuxt',
    '@vueuse/nuxt',
    '@nuxt/ui',
    '@nuxtjs/tailwindcss',
    '@nuxt/eslint'
  ],
  tailwindcss: {
    configPath: './tailwind.config.ts',
  },
  css: ['./assets/css/main.css', './assets/css/scrollbars.css'],
  pinia: {
    storesDirs: ['./stores/**']
  },
  ui: {
    global: true,
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