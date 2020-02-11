<template>
  <div class="home container-fluid">
    <div class="row">
      <div class="col-12 db-title">
        <h1 class="mt-3 mb-3">Welcome Home</h1>
      </div>
    </div>
    <div class="row">
      <div
        class="col-3 keep-cols"
        v-for="keep in keeps"
        :key="keep.id"
        :style="
          'height: 25vw; background-image: url(' +
            keep.img +
            '); background-size: cover;'
        "
      >
        <h1 class="keep-text">{{ keep.name }}</h1>
        <h3 class="keep-text">{{ keep.description }}</h3>
        <div class="d-flex keep-buttons-div">
          <router-link
            class="btn btn-info keep-buttons"
            :to="'/keeps/' + keep.id"
            tag="button"
            >View</router-link
          >
          <button
            class="btn btn-warning keep-buttons"
            type="button"
            id="dropdownMenuButton"
            data-toggle="dropdown"
            aria-haspopup="true"
            aria-expanded="false"
          >
            Keep
          </button>
          <div
            class="dropdown-menu border dropdown-menu-right"
            aria-labelledby="dropdownMenuButton"
          >
            <a
              v-for="vault in vaults"
              :key="vault.id"
              :to="{ name: 'profile', params: { id: vault._id } }"
              class="dropdown-item"
              @click="addKeepToVault(vault.id, keep.id)"
              >{{ vault.name }}</a
            >
          </div>
          <button
            class="btn btn-success keep-buttons"
            v-clipboard="() => location + '\#/keeps/' + keep.id"
            @click="share(keep.id)"
          >
            Share
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "home",
  mounted() {
    this.$store.dispatch("getResource", { name: "keeps" });
    this.$store.dispatch("getResource", { name: "vaults" });
  },
  computed: {
    user() {
      return this.$store.state.user;
    },
    keeps() {
      return this.$store.state.keeps;
    },
    vaults() {
      return this.$store.state.vaults;
    },
    location() {
      return location.host;
    }
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    },
    addKeepToVault(vaultId, keepId) {
      this.$store.dispatch("addKeepToVault", { vaultId, keepId });
    },
    share(id) {
      this.$store.dispatch("shareKeep", id);
    }
  }
};
</script>
<style>
.keep-text {
  -webkit-text-stroke: 0.05em black;
  font-family: "Palatino-Linotype";
  font-weight: bold;
  color: whitesmoke;
}
.keep-cols {
  padding: 0 0;
}
.keep-buttons-div {
  position: absolute;
  bottom: 0;
  width: -webkit-fill-available;
}
.keep-buttons {
  width: -webkit-fill-available;
  opacity: 0.7;
  border-radius: 0 0 0 0;
}
.keep-buttons:hover {
  opacity: 1;
}
.db-title {
  background-color: rgb(245, 245, 245);
  border: 1px solid rgb(225, 225, 225);
}
</style>
