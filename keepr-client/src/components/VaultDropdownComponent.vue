<template>
  <a class="dropdown-item" :id="vaultProp.id" @click="createVaultKeep">{{ vaultProp.name }}</a>
</template>
<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { vaultKeepsService } from '../services/VaultKeepsService'
import swal from 'sweetalert'
export default {
  name: 'VaultDropdownComponent',
  props: {
    vaultProp: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const state = reactive({
      keep: computed(() => AppState.activeKeep)
    })
    return {
      state,
      createVaultKeep() {
        const body = { vaultId: props.vaultProp.id, keepId: state.keep.id }
        try {
          vaultKeepsService.create(body)
          swal({
            title: 'Right On!',
            text: 'This keep has been added to your vault!',
            icon: 'success',
            button: 'Cool!'
          })
        } catch (error) {
          logger.log(error)
        }
      }
    }
  }
}
</script>
