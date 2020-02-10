<template>
  <div class="vault-details container-fluid">
    <div class="row">
      <div class="col">{{activeVault.name}}: {{activeVault.description}}</div>
    </div>
    <div class="row">
      <div
        class="col-3 keep-cols"
        v-for="keep in keeps"
        :key="keep.id"
        :style="'height: 25vw; background-image: url('+keep.img+'); background-size: cover;'"
      >
        <h1 class="keep-text">{{keep.name}}</h1>
        <h3 class="keep-text">{{keep.description}}</h3>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "VaultDetails",
  data() {
    return {};
  },
  mounted() {
    this.$store.dispatch("getVaultById", this.$route.params.id);
  },
  computed: {
    activeVault() {
      return this.$store.state.activeVault;
    },
    keeps() {
      return this.$store.state.keeps;
    }
  },
  methods: {}
};
</script>
<style>
</style>