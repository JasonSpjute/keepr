<template>
  <div class="container-fluid">
    <div class="row">
      <div class="card-columns">
        <KeepsComponent />
      </div>
    </div>
  </div>
</template>

<script>
import { AppState } from '../AppState'
import { computed, reactive, onMounted } from 'vue'
import { keepsService } from '../services/KeepsService'
export default {
  name: 'Home',
  setup() {
    const state = reactive({
      restaurants: computed(() => AppState.keeps)
    })
    onMounted(() => {
      keepsService.getKeeps()
    })
    return {
      state
    }
  }
}
</script>

<style scoped lang="scss">
.home{
  text-align: center;
  user-select: none;
  > img{
    height: 200px;
    width: 200px;
  }
}
</style>
