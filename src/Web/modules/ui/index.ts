import { createResolver, defineNuxtModule } from '@nuxt/kit'

export interface ModuleOptions {
    license?: string
    routerOptions?: boolean
    content?: boolean
}

export default defineNuxtModule({
    setup(options, nuxt) {
        const resolver = createResolver(import.meta.url)
        if (options.routerOptions) {
            nuxt.hook('pages:routerOptions', ({ files }) => {
                files.push({
                    path: resolver.resolve('runtime/app/router.options.ts'),
                    optional: true
                })
            })
        }
    }
})
