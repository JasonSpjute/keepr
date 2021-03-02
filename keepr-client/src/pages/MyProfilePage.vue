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
    <div class="row my-3 mx-5">
      <div class="col">
        <h2><i class="fa fa-plus text-success" @click="state.vaultForm = !state.vaultForm" aria-hidden="true"></i> &nbsp; Vaults:</h2>
      </div>
    </div>
    <div class="row" v-if="state.vaultForm">
      <div class="col-6 ml-5 mb-4 py-3 px-3 bg-success rounded">
        <h5>Create a Vault!</h5>
        <form>
          <div class="form-group">
            <label for="vaultName">Vault Name</label>
            <input type="string" class="form-control" id="vaultName" placeholder="Enter name" v-model="state.newVault.name">
          </div>
          <div class="form-group">
            <label for="vaultDescription">Vault Description</label>
            <input type="string" class="form-control" id="vaultDescription" placeholder="Enter Description">
          </div>
          <div class="form-group form-check">
            <input type="checkbox" class="form-check-input" id="privacyCheck">
            <label class="form-check-label" for="privacyCheck">Private</label>
            <small id="emailHelp" class="form-text text-muted">Check this box if you don't want anyone else to see your vault.</small>
          </div>
          <button type="submit" class="btn btn-primary">
            Submit
          </button>
        </form>
      </div>
    </div>
    <div class="row mx-5">
      <div class="card-columns">
        <VaultComponent v-for="v in state.vaults" :key="v.id" :vault-prop="v" />
      </div>
    </div>
    <div class="row my-3 mx-5">
      <div class="col">
        <h2><i class="fa fa-plus text-success" @click="state.keepForm = !state.keepForm" aria-hidden="true"></i> &nbsp; Keeps:</h2>
      </div>
    </div>
    <div class="row" v-if="state.keepForm">
      <div class="col-6 ml-5 mb-4 py-3 px-3 bg-success rounded">
        <h5>Create a Keep!</h5>
        <form>
          <div class="form-group">
            <label for="keepName">Keep Name</label>
            <input type="string" class="form-control" id="keepName" placeholder="Enter name" v-model="state.newKeep.name">
          </div>
          <div class="form-group">
            <label for="keepDescription">Keep Description</label>
            <input type="string" class="form-control" id="keepDescription" placeholder="Enter Description">
          </div>
          <div class="form-group">
            <label for="keepPicture">Keep Image</label>
            <input type="string" class="form-control" id="keepDescription" placeholder="Enter Image Url">
          </div>
          <button type="submit" class="btn btn-primary">
            Submit
          </button>
        </form>
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
      keeps: computed(() => AppState.keeps),
      vaultForm: false,
      newVault: {},
      keepForm: false,
      newKeep: {}
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
