<template>
  <div class="vault-details container-fluid">
    <div class="row">
      <div class="col">
        {{ activeVault.name }}: {{ activeVault.description }}
      </div>
    </div>
    <div class="row" v-if="activeKeeps">
      <div
        class="col-3 keep-cols"
        v-for="keep in activeKeeps"
        :key="keep.id"
        :style="
          'height: 25vw; background-image: url(' +
            keep.img +
            '); background-size: cover;'
        "
      >
        <h1 class="keep-text">{{ keep.name }}</h1>
        <h3 class="keep-text">{{ keep.description }}</h3>

        <i
          class="fas fa-times delete-icon"
          style="position: absolute; bottom: 0; right: 0;"
          @click="removeKeepFromVault(activeVault.id, keep.id)"
        ></i
        ><router-link
          class="btn btn-info keep-buttons"
          style="position: absolute; bottom: 3vh; right: 0;"
          :to="'/keeps/' + keep.id"
          tag="button"
          >View</router-link
        >
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
    activeKeeps() {
      return this.$store.state.activeKeeps;
    }
  },
  methods: {
    removeKeepFromVault(vaultId, keepId) {
      this.$store.dispatch("removeKeepFromVault", { vaultId, keepId });
    }
  }
};
</script>
<style>
.delete-icon {
  position: absolute;
  right: 0;
  color: red;
  bottom: 0;
  font-size: 2em;
}
.delete-icon:hover {
  cursor: pointer;
}
</style>
