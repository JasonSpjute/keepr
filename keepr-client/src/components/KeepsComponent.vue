<template>
  <div>
    <div class="card" @click="modalOpen()">
      <img class="card-img-top round-pic" :src="keepProp.img" alt="Card image">
      <div class="card-img-overlay">
        <div class="row text-white toTheBottom">
          <div class="col-10">
            <h1 class="card-title">
              {{ keepProp.name }}
            </h1>
          </div>
          <div class="col-1 text-right mr-3">
            <i class="far fa-user fa-3x"></i>
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
              <div class="toTheBottom row d-flex">
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
                  <i class="fas fa-trash text-danger"></i>
                </div>
                <div class="col text-right" v-if="state.keep.creator">
                  <img :src="state.keep.creator.picture" class="creatorPic" alt="">
                  <p class="namePic">
                    &nbsp; {{ state.keep.creator.name }}
                  </p>
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
import { vaultsService } from '../services/VaultsService'
export default {
  name: 'KeepsComponent',
  props: {
    keepProp: { type: Object, required: true }
  },
  setup(props) {
    const state = reactive({
      keep: computed(() => AppState.activeKeep),
      user: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults)
    })
    return {
      state,
      modalOpen() {
        vaultsService.getByAccount()
        keepsService.getOne(props.keepProp.id)
        $('#modal' + props.keepProp.id).modal('show')
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
.creatorPic{
  max-width: 2rem;
  border-radius: 5px;
}
.namePic{
  display: inline;
}

.toTheBottom{
position: absolute;
left: 1rem;
right: 1rem;
bottom: 1rem;
}

</style>
