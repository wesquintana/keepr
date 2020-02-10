<template>
  <div class="dashboard container-fluid bg-light">
    <div class="row">
      <div class="col-12 d-flex justify-content-between db-title pt-3 pb-3">
        <h1>WELCOME TO THE DASHBOARD, {{this.$auth.user.name}}</h1>
        <div class="align-self-center">
          <button class="btn btn-keep" @click="displayingKeeps=true">K</button>
          <button class="ml-2 btn btn-vault" @click="displayingKeeps=false">V</button>
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col-8 offset-md-2">
        <form class="mt-5" @submit.prevent="createKeep" v-if="isKeeps">
          <div class="form-group row">
            <div class="col-sm-10">
              <input
                type="text"
                class="form-control"
                placeholder="Name"
                v-model="newKeep.name"
                required
              />
            </div>
          </div>
          <div class="form-group row">
            <div class="col-sm-10">
              <input
                type="text"
                class="form-control"
                placeholder="Description"
                v-model="newKeep.description"
                required
              />
            </div>
          </div>
          <div class="form-group row">
            <div class="col-sm-10">
              <input type="text" class="form-control" v-model="newKeep.img" placeholder="Image Url" />
            </div>
          </div>
          <div class="form-group row">
            <div class="col-sm-2">
              <b>Mark as Public</b>
            </div>
            <div class="col-sm-10">
              <div class="form-check">
                <input
                  class="form-check-input private-checkbox"
                  type="checkbox"
                  v-model="newKeep.isPrivate"
                  :true-value="false"
                  :false-value="true"
                />
              </div>
            </div>
          </div>
          <div class="form-group row">
            <div class="col-sm-10">
              <button type="submit" class="btn btn-keep form-button">Keep</button>
              <button type="button" class="btn btn-vault form-button">Reset</button>
            </div>
          </div>
        </form>
        <form class="mt-5" @submit.prevent="createVault" v-else>
          <div class="form-group row">
            <div class="col-sm-10">
              <input
                type="text"
                class="form-control"
                placeholder="thing"
                v-model="newVault.name"
                required
              />
            </div>
          </div>
          <div class="form-group row">
            <div class="col-sm-10">
              <input
                type="text"
                class="form-control"
                placeholder="Description"
                v-model="newVault.description"
                required
              />
            </div>
          </div>
          <div class="form-group row">
            <div class="col-sm-10">
              <button type="submit" class="btn btn-keep form-button">Create</button>
              <button type="button" class="btn btn-vault form-button">Reset</button>
            </div>
          </div>
        </form>
      </div>
    </div>
    <div class="row scroll-x">
      <div class="col-3" v-for="vault in vaults" :key="vault.id">
        <router-link :to="'/vaults/'+vault.id">
          <h5>{{vault.name}}</h5>
          <p>{{vault.description}}</p>
        </router-link>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Dashboard",
  data() {
    return {
      newKeep: {
        name: "",
        description: "",
        img: "",
        isPrivate: false
      },
      displayingKeeps: true,
      newVault: {
        name: "",
        description: ""
      }
    };
  },
  mounted() {
    this.$store.dispatch("getResource", { name: "vaults" });
  },
  methods: {
    createKeep() {
      let keep = { ...this.newKeep };
      this.$store.dispatch("createKeep", keep);
      this.newKeep = {
        name: "",
        description: "",
        img: "",
        isPrivate: false
      };
    },
    createVault() {
      let vault = { ...this.newVault };
      this.$store.dispatch("createVault", vault);
      this.newVault = {
        name: "",
        description: ""
      };
    }
  },
  computed: {
    isKeeps() {
      return this.displayingKeeps;
    },
    vaults() {
      return this.$store.state.vaults;
    }
  }
};
</script>

<style>
.db-title {
  background-color: rgb(245, 245, 245);
  border: 1px solid rgb(225, 225, 225);
}
.btn-keep {
  background-color: rgb(189, 189, 195);
  color: white;
  font-weight: bold;
  border-color: rgb(169, 169, 175);
}
.btn-keep:hover {
  background-color: rgb(169, 169, 175);
  color: white;
}
.btn-vault {
  background-color: rgb(40, 40, 45);
  color: white;
  font-weight: bold;
  border-color: rgb(20, 20, 25);
}
.btn-vault:hover {
  background-color: rgb(20, 20, 25);
  color: white;
}
.private-checkbox {
  height: 30px;
  width: 30px;
}
.form-button {
  width: 50%;
  border-radius: 0 0 0 0;
}
.scroll-x {
  overflow-x: auto;
  white-space: nowrap;
  flex-wrap: nowrap;
}
.scroll-x::-webkit-scrollbar {
  height: 8px;
}
.scroll-x::-webkit-scrollbar-thumb {
  background-color: rgba(100, 100, 100, 0.7);
  border-radius: 4px;
}
.scroll-x::-webkit-scrollbar-thumb:hover {
  background-color: rgba(100, 100, 100, 0.9);
}
</style>
