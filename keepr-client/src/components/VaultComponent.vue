<template>
  <router-link :to="{ name: 'VaultPage', params: { id: vaultProp.id }}">
    <div
      class="card grey vault-card"
    >
      <div class="card-body text-white">
        <i class="fas fa-lock" v-if="vaultProp.isPrivate">&nbsp;private</i>
        <h1>
          {{ vaultProp.name }}
        </h1>
      </div>
    </div>
  </router-link>
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
