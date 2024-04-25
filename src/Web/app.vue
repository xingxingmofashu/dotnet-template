<template>
  <div>
    <NuxtLoadingIndicator />
    <Header />
    <NuxtLayout>
      <NuxtPage />
    </NuxtLayout>
  </div>
</template>
<style id="nuxt-ui-colors">
:root {
  --color-primary-50: 239 253 245;
  --color-primary-100: 217 251 232;
  --color-primary-200: 179 245 209;
  --color-primary-300: 117 237 174;
  --color-primary-400: 0 220 130;
  --color-primary-500: 0 193 106;
  --color-primary-600: 0 161 85;
  --color-primary-700: 0 127 69;
  --color-primary-800: 1 101 56;
  --color-primary-900: 10 83 49;
  --color-primary-950: 5 46 22;
  --color-primary-DEFAULT: var(--color-primary-500);
  --color-gray-50: 248 250 252;
  --color-gray-100: 241 245 249;
  --color-gray-200: 226 232 240;
  --color-gray-300: 203 213 225;
  --color-gray-400: 148 163 184;
  --color-gray-500: 100 116 139;
  --color-gray-600: 71 85 105;
  --color-gray-700: 51 65 85;
  --color-gray-800: 30 41 59;
  --color-gray-900: 15 23 42;
  --color-gray-950: 2 6 23;
}

.dark {
  --color-primary-DEFAULT: var(--color-primary-400);
}
</style>
<style id="nuxt-ui-variables">
:root {
  --header-height: 4rem;
  --ui-background: 255 255 255;
  --ui-foreground: var(--color-gray-700);
}

.dark {
  --ui-background: var(--color-gray-900);
  --ui-foreground: var(--color-gray-200);
}
</style>