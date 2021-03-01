<template>
  <div class="container-fluid">
    <div class="row">
      <div class="card-columns mx-5">
        <KeepsComponent v-for="k in state.keeps" :key="k.id" :keep-prop="k" />
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
      keeps: computed(() => AppState.keeps)
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
.card-columns {
    column-count: 2;
    @media (min-width: 992px){
      column-count: 4;
    }
}
</style>
