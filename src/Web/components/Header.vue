<template>
    <div :class="['backdrop-blur', 'border-b', '-mb-px', 'sticky', 'top-0', 'z-50',
        $route.path === '/' ? 'border-primary-200/75 dark:border-primary-900/50' : 'border-gray-200 dark:border-gray-800']
        ">
        <div class="mx-auto px-4 sm:px-6 lg:px-8 max-w-7xl flex items-center justify-between gap-3 h-[--header-height]">
            <div class="lg:flex-1 flex items-center gap-1.5 ">
                XBoot
            </div>
            <ul class="items-center gap-x-8 hidden lg:flex">
                <li class="relative">1</li>
                <li class="relative">2</li>
            </ul>
            <div class="flex items-center justify-end lg:flex-1 gap-1.5">
                <ColorPicker />

                <ColorMode />
            </div>
            
        </div>
    </div>
</template>