<template>
  <div class="container">
    <div class="row">
      <div class="col-2">
        <img :src="state.user.picture" class="img-fluid" alt="">
      </div>
      <div class="col">
        <h1>{{ state.user.name }}</h1>
      </div>
    </div>
  </div>
</template>
<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
export default {
  name: 'ProfilePage',
  setup() {
    const state = reactive({
      user: computed(() => AppState.profile)
    })
    return {
      state
    }
  }
}
</script>
