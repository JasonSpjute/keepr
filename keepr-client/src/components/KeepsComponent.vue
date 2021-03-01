<template>
  <div>
    <div class="card" @click="modalOpen()">
      <img class="card-img-top round-pic" :src="keepProp.img" alt="Card image">
      <div class="card-img-overlay">
        <h4 class="card-title">
          {{ keepProp.name }}
        </h4>
        <p class="card-text">
          {{ keepProp.description }}
        </p>
        <a href="#" class="btn btn-primary">See Profile</a>
      </div>
    </div>
    <div class="modal-fixed-footer modal fade"
         :id="'modal' + keepProp.id"
         tabindex="-1"
         role="dialog"
         aria-labelledby="myLargeModalLabel"
         aria-hidden="true"
    >
      <div class="modal-dialog modal-dialog-centered modal-xl">
        <div class="modal-content">
          <div class="row px-3">
            <div class="col-6 py-3 round">
              <img :src="state.keep.img" class="img-fluid rounded" alt="">
            </div>
            <div class="col-6 py-1">
              <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
              </button>
              <div class="row text-success">
                <div class="col text-center mt-4">
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
              <div class="modal-footer">
                <div class="col text-left">
                  <div class="dropdown">
                    <button class="btn btn-secondary dropdown-toggle"
                            type="button"
                            id="dropdownMenuButton"
                            data-toggle="dropdown"
                            aria-haspopup="true"
                            aria-expanded="false"
                    >
                      Dropdown button
                    </button>
                    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                      <a class="dropdown-item" href="#">Action</a>
                      <a class="dropdown-item" href="#">Another action</a>
                      <a class="dropdown-item" href="#">Something else here</a>
                    </div>
                  </div>
                </div>
                <div class="col text-right" v-if="state.keep.creator">
                  <h6>{{ state.keep.creator.name }}</h6>
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
export default {
  name: 'KeepsComponent',
  props: {
    keepProp: { type: Object, required: true }
  },
  setup(props) {
    const state = reactive({
      keep: computed(() => AppState.activeKeep)
    })
    return {
      state,
      modalOpen() {
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
</style>
