<template>
  <div class="card m-3" @click="modalOpen()">
    <img class="card-img-top" :src="keepProp.img" alt="Card image">
    <div class="card-img-overlay">
      <h4 class="card-title">
        {{ keepProp.name }}
      </h4>
      <p class="card-text">
        {{ keepProp.description }}
      </p>
      <a href="#" class="btn btn-primary">See Profile</a>
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
          <div class="row">
            <div class="col-6 p-3">
              <img :src="keepProp.img" class="img-fluid" alt="">
            </div>
            <div class="col-6"></div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import $ from 'jquery'
export default {
  name: 'KeepsComponent',
  props: {
    keepProp: { type: Object, required: true }
  },
  setup(props) {
    return {
      modalOpen() {
        $('#modal' + props.keepProp.id).modal('toggle')
      }
    }
  }
}
</script>
<style lang="scss" scoped>
.card{
  cursor: pointer;
}
</style>
