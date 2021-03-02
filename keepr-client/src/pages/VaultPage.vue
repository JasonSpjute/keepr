<template>
  <div class="containter">
    <div class="row ml-5 mt-5">
      <div class="col">
        <h1>{{ state.vault.name }}</h1>
      </div>
    </div>
    <div class="row ml-5">
      <div class="col">
        <h4>Keeps: {{ state.keeps.length }}</h4>
      </div>
    </div>
    <div class="row mt-5">
      <div class="card-columns mx-5">
        <KeepsComponent v-for="k in state.keeps" :key="k.id" :keep-prop="k" />
      </div>
    </div>
  </div>
</template>
<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
export default {
  name: 'VaultPage',
  setup() {
    const route = useRoute()
    const state = reactive({
      vault: computed(() => AppState.currentVault),
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps)
    })
    onMounted(() => {
      vaultsService.getOne(route.params.id)
      keepsService.getByVaultId(route.params.id)
    })
    return {
      state
    }
  }
}
</script>
<style lang="scss" scoped>
.card-columns {
    column-count: 2;
    @media (min-width: 992px){
      column-count: 4;
    }
}
</style>
