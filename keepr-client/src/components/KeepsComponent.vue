<template>
  <div>
    <div class="card" @click="modalOpen()">
      <img class="card-img-top round-pic" :src="keepProp.img" alt="Card image">
      <div class="card-img-overlay keep-img">
        <div class="row" v-if="vaultPage && state.user.id == creatorId">
          <div class="col">
            <i class="fas fa-ban fa-2x text-danger" @click="removeFromVault"></i>
          </div>
        </div>
        <div class="row text-white to-the-bottom mx-1">
          <div class="col-10">
            <h1 class="card-title">
              {{ keepProp.name }}
            </h1>
          </div>
        </div>
      </div>
    </div>
    <div class="modal fade"
         :id="'modal' + keepProp.id"
         tabindex="-1"
         role="dialog"
         aria-labelledby="myLargeModalLabel"
         aria-hidden="true"
    >
      <div class="modal-dialog modal-dialog-centered modal-xl">
        <div class="modal-content">
          <div class="row px-3">
            <div class="col-12 col-lg-6 py-3 round">
              <img :src="state.keep.img" class="img-fluid rounded" alt="">
            </div>
            <div class="col-12 col-lg-6 py-1">
              <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
              </button>
              <div class="row text-success">
                <div class="col text-center mt-4 ml-5">
                  <p><span><i class="fas fa-eye"></i> {{ state.keep.views }} &nbsp; &nbsp; &nbsp; <b>K</b> {{ state.keep.keeps }}</span> </p>
                </div>
              </div>
              <div class="row">
                <div class="col text-center">
                  <h1>{{ state.keep.name }}</h1>
                </div>
              </div>
              <div class="row mt-3">
                <div class="col text-center">
                  <p>{{ state.keep.description }}</p>
                </div>
              </div>
              <hr>
              <div class="to-the-bottom mb-3 mr-3 row d-flex">
                <div class="col text-left">
                  <div class="dropdown">
                    <button class="btn btn-secondary dropdown-toggle"
                            type="button"
                            id="dropdownMenuButton"
                            data-toggle="dropdown"
                            aria-haspopup="true"
                            aria-expanded="false"
                    >
                      Add to Vault
                    </button>
                    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                      <VaultDropdownComponent v-for="v in state.vaults" :key="v.id" :vault-prop="v" />
                    </div>
                  </div>
                </div>
                <div class="col-1 text-center" v-if="state.user.id == state.keep.creatorId">
                  <i class="fas fa-trash text-danger point" title="Delete this Keep" @click="deleteKeep"></i>
                </div>
                <div class="col text-right" v-if="state.keep.creator">
                  <router-link :to="{ name: 'Profile', params: {id: keepProp.creatorId} }" @click="closeModal()">
                    <img
                      :src="state.keep.creator.picture"
                      class="creator-pic"
                      alt=""
                    >
                    <p class="name-pic text-dark">
                      &nbsp; {{ state.keep.creator.name }}
                    </p>
                  </router-link>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import $ from 'jquery'
import { keepsService } from '../services/KeepsService'
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
import swal from 'sweetalert'
import { logger } from '../utils/Logger'
import { vaultKeepsService } from '../services/VaultKeepsService'
export default {
  name: 'KeepsComponent',
  props: {
    keepProp: { type: Object, required: true },
    vaultPage: { type: Boolean, required: false },
    vaultId: { type: Number, default: 0 },
    creatorId: { type: String, required: false, default: '' }
  },
  setup(props) {
    const state = reactive({
      keep: computed(() => AppState.activeKeep),
      user: computed(() => AppState.account),
      vaults: computed(() => AppState.myVaults)
    })
    return {
      state,
      modalOpen() {
        keepsService.getOne(props.keepProp.id)
        $('#modal' + props.keepProp.id).modal('show')
      },
      closeModal() {
        $('#modal' + props.keepProp.id).modal('hide')
      },
      deleteKeep() {
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
                keepsService.delete(state.keep.id)
                swal('This Keep has been Deleted', {
                  icon: 'success'
                })
                $('#modal' + props.keepProp.id).modal('hide')
              } catch (error) {
                logger.log(error)
              }
            }
          })
      },
      removeFromVault() {
        event.stopPropagation()
        try {
          vaultKeepsService.delete(props.keepProp.vaultKeepId, props.vaultId)
          swal('This Keep has been removed from this Vault', {
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
.card{
  cursor: pointer;
  border-radius: 25px;
}
.round-pic{
  border-radius: 25px;
}
.creator-pic{
  max-width: 2rem;
  border-radius: 5px;
}
.name-pic{
  display: inline;
}

.to-the-bottom{
position: absolute;
left: 0;
right: 0;
bottom: 0;
}
.point{
  cursor: pointer;
}
.keep-img{
background: linear-gradient(180deg, rgba(226,226,226,0.0035364487591911242) 56%, rgba(42,42,42,0.5497549361541492) 100%);
border-radius: 25px;
}

</style>
