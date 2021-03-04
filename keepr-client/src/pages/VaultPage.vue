<template>
  <div class="container-fluid">
    <div>
      <h1>Can you see me</h1>
    </div>
    <div class="row mx-5 mt-5">
      <div class="col">
        <h1 class="title-trash" v-if="state.vault">
          {{ state.vault.name }} &nbsp;
        </h1>
        <div class="title-trash" v-if="state.vault.creatorId == state.account.id">
          <i class="fas fa-trash text-danger fa-lg point" title="Delete Vault" @click="deleteVault"></i>
        </div>
      </div>
      <div class="col text-right" v-if="state.account.id == state.vault.creatorId">
        <div v-if="state.vault.isPrivate">
          <h4>This Vault is Private</h4>
          <button class="btn btn-primary" @click="makePublic">
            Make it Public
          </button>
        </div>
        <div v-if="!state.vault.isPrivate">
          <h4>This Vault is Public</h4>
          <button class="btn btn-primary" @click="makePrivate">
            Make it Private
          </button>
        </div>
      </div>
    </div>
    <div class="row ml-5">
      <div class="col">
        <h4>Keeps: {{ state.keeps.length }}</h4>
      </div>
    </div>
    <div class="row mt-5">
      <div class="card-columns mx-5">
        <KeepsComponent v-for="k in state.keeps"
                        :key="k.index"
                        :keep-prop="k"
                        :vault-page="state.vaultPage"
                        :vault-id="state.vault.id"
                        :creator-id="state.vault.creatorId"
        />
      </div>
    </div>
  </div>
</template>
<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { useRoute, useRouter } from 'vue-router'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import { logger } from '../utils/Logger'
import swal from 'sweetalert'

export default {
  name: 'VaultPage',
  setup() {
    const route = useRoute()
    const router = useRouter()
    const state = reactive({
      vault: computed(() => AppState.currentVault),
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps),
      user: computed(() => AppState.user),
      vaultPage: true
    })
    onMounted(() => {
      keepsService.clearKeeps()
      vaultsService.getOne(route.params.id)
      keepsService.getByVaultId(route.params.id)
    })
    return {
      state,
      deleteVault() {
        swal({
          title: 'Are you sure?',
          text: 'This is cannot be undone!',
          icon: 'warning',
          buttons: true,
          dangerMode: true
        })
          .then((willDelete) => {
            if (willDelete) {
              try {
                vaultsService.delete(route.params.id)
                swal('This Vault has been Deleted', {
                  icon: 'success'
                })
                router.push({ name: 'MyProfile', params: { id: state.account.id } })
              } catch (error) {
                logger.log(error)
              }
            }
          })
      },
      makePrivate() {
        const body = { isPrivate: true }
        try {
          vaultsService.edit(body, route.params.id)
          swal('This Vault is now Private', {
            icon: 'success'
          })
        } catch (error) {
          logger.log(error)
        }
      },
      makePublic() {
        const body = { isPrivate: false }
        try {
          vaultsService.edit(body, route.params.id)
          swal('This Vault is now Private', {
            icon: 'success'
          })
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
    @media (min-width: 992px){
      column-count: 4;
    }
}
.title-trash {
  display: inline;
}
.point {
  cursor: pointer;
}
</style>
