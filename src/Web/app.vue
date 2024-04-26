<template>
  <div>
    <NuxtLoadingIndicator />
    <NuxtLayout>
      <Header />
      <div class="grid grid-cols-6">
        <div class="col-span-1">
          aside
        </div>
        <UMain class="col-span-5">
          <NuxtPage />
        </UMain>
      </div>
    </NuxtLayout>
  </div>
</template>