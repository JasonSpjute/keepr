<template>
  <div class="div">
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
      <div class="row mt-5 mb-3 mx-5">
        <div class="col">
          <h2><i class="fa fa-plus text-success" @click="openVaultModal()" aria-hidden="true"></i> &nbsp; Vaults:</h2>
        </div>
      </div>
      <div class="row mx-5">
        <div class="card-columns">
          <VaultComponent v-for="v in state.vaults" :key="v.id" :vault-prop="v" />
        </div>
      </div>
      <div class="row my-3 mx-5">
        <div class="col">
          <h2><i class="fa fa-plus text-success" @click="openKeepModal" aria-hidden="true"></i> &nbsp; Keeps:</h2>
        </div>
      </div>
      <div class="row">
        <div class="card-columns mx-5">
          <KeepsComponent v-for="k in state.keeps" :key="k.id" :keep-prop="k" />
        </div>
      </div>
    </div>
    <div class="modal" tabindex="-1" role="dialog" id="vaultModal">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">
              Modal title
            </h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <form @submit.prevent="createVault">
            <div class="modal-body">
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
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-secondary" data-dismiss="modal">
                Close
              </button>
              <button type="submit" class="btn btn-primary">
                Save changes
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
    <div class="modal" tabindex="-1" role="dialog" id="keepModal">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">
              Modal title
            </h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <form @submit.prevent="createKeep">
            <div class="modal-body">
              <div class="form-group">
                <label for="keepName">Keep Name</label>
                <input type="string" class="form-control" id="keepName" placeholder="Enter name" v-model="state.newKeep.name">
              </div>
              <div class="form-group">
                <label for="keepDescription">Keep Description</label>
                <input type="string" class="form-control" id="keepDescription" placeholder="Enter Description" v-model="state.newKeep.description">
              </div>
              <div class="form-group">
                <label for="keepPicture">Keep Image</label>
                <input type="string" class="form-control" id="keepDescription" placeholder="Enter Image Url" v-model="state.newKeep.img">
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-secondary" data-dismiss="modal">
                Close
              </button>
              <button type="submit" class="btn btn-primary">
                Save changes
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { profilesService } from '../services/ProfilesService'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import { logger } from '../utils/Logger'
import { useRoute } from 'vue-router'
import $ from 'jquery'
export default {
  name: 'MyProfilePage',
  setup() {
    const route = useRoute()
    const state = reactive({
      profile: computed(() => AppState.profile),
      vaults: computed(() => AppState.myVaults),
      keeps: computed(() => AppState.keeps),
      newVault: {},
      newKeep: {}
    })
    onMounted(() => {
      profilesService.getProfile(route.params.id)
      keepsService.getByProfileId(route.params.id)
    })
    return {
      state,
      openVaultModal() {
        $('#vaultModal').modal('show')
      },
      openKeepModal() {
        $('#keepModal').modal('show')
      },
      createVault() {
        try {
          vaultsService.createVault(state.newVault)
          state.newVault = {}
          $('#vaultModal').modal('hide')
        } catch (error) {
          logger.log(error)
        }
      },
      createKeep() {
        try {
          keepsService.createKeep(state.newKeep)
          state.newKeep = {}
          $('#keepModal').modal('hide')
        } catch (error) {
          logger.log(error)
        }
      }
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
