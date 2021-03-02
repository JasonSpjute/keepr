<template>
  <a class="dropdown-item" :id="vaultProp.id">{{ vaultProp.name }}</a>
</template>
<script>
export default {
  name: 'VaultDropdownComponent',
  props: {
    vaultProp: {
      type: Object,
      required: true
    }
  }
}
</script>
