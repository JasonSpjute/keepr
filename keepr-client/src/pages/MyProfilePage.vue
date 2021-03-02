<template>
  <div class="container-fluid">
    <div class="row mt-5">
      <div class="col-2 text-right">
        <img :src="state.profile.picture" class="img-fluid" alt="">
      </div>
      <div class="col">
        <h1>{{ state.profile.name }}</h1>
        <h3>Vaults: {{ state.vaults.length }}</h3>
        <h3>Keeps: {{ state.keeps.length }}</h3>
      </div>
    </div>
    <div class="row m-3">
      <div class="col">
        <h2>Vaults</h2>
      </div>
    </div>
    <div class="row mx-3">
      <div class="card-columns">
        <VaultComponent v-for="v in state.vaults" :key="v.id" :vault-prop="v" />
      </div>
    </div>
    <div class="row m-3">
      <div class="col">
        <h2>Keeps</h2>
      </div>
    </div>
    <div class="row">
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
import { profilesService } from '../services/ProfilesService'
import { keepsService } from '../services/KeepsService'
export default {
  name: 'MyProfilePage',
  setup() {
    const route = useRoute()
    const state = reactive({
      profile: computed(() => AppState.profile),
      vaults: computed(() => AppState.myVaults),
      keeps: computed(() => AppState.keeps)
    })
    onMounted(() => {
      profilesService.getProfile(route.params.id)
      keepsService.getByProfileId(route.params.id)
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
    @media (min-width: 786px){
    column-count: 4;
    }
    @media (min-width: 992px){
      column-count: 6;
    }
}
</style>
