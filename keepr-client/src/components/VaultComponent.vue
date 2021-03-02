<template>
  <div class="card grey vault-card">
    <div class="card-body text-white">
      <h1>
        {{ vaultProp.name }}
      </h1>
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
<style lang="scss" scoped>
.grey{
  background-color: grey
}
.vault-card{
  // height: 17rem;
  border-radius: 25px;
  padding-top: 40%;
  width: 100%
}
.to-the-bottom{
position: absolute;
bottom: 0;
}
</style>
