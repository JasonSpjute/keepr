<template>
  <div class="card">
    <div class="card-body">
      <div class="card-title">
        {{ vaultProp.name }}
      </div>
      <div class="card-text">
        {{ vaultProp.description }}
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: 'VaultComponent',
  props: {
    vaultProp: {
      type: Object,
      required: true
    }
  }
}
</script>
