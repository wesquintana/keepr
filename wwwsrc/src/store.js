import Vue from "vue";
import Vuex from "vuex";
import Axios from "axios";
import router from "./router";

Vue.use(Vuex);

let baseUrl = "https://noterest.herokuapp.com/";
let api = Axios.create({
  baseURL: baseUrl + "api/",
  timeout: 3000,
  withCredentials: true
});

export default new Vuex.Store({
  state: {
    keeps: [],
    activeKeep: {},
    activeKeeps: [],
    myKeeps: [],
    vaults: [],
    activeVault: {}
  },
  mutations: {
    setResource(state, payload) {
      state[payload.name] = payload.data;
    },
    setViews(state, views) {
      state.activeKeep.views++;
    },
    addResourceToArray(state, payload) {
      state[payload.name].push(payload.data);
    },
    removeKeepFromVault(state, id) {
      let index = state.activeKeeps.findIndex(k => k.id == id);
      state.activeKeeps.splice(index, 1);
    },
    deleteKeep(state, id) {
      let index = state.myKeeps.findIndex(k => k.id == id);
      state.myKeeps.splice(index, 1);
    },
    deleteVault(state, id) {
      let index = state.vaults.findIndex(v => v.id == id);
      state.vaults.splice(index, 1);
    }
  },
  actions: {
    setBearer({}, bearer) {
      api.defaults.headers.Authorization = bearer;
    },
    // resetBearer() {
    //   api.defaults.headers.Authorization = "";
    // },
    async getResource({ commit, dispatch }, payload) {
      let data = await api.get(payload.name);
      payload.data = data.data;
      commit("setResource", payload);
    },
    async getKeepById({ commit, dispatch }, id) {
      let data = await api.get("keeps/" + id);
      commit("setResource", { name: "activeKeep", data: data.data });
      dispatch("viewKeep", id);
    },
    async getVaultById({ commit, dispatch }, id) {
      let data = await api.get("vaults/" + id);
      commit("setResource", { name: "activeVault", data: data.data });
      dispatch("getKeepsByVaultId", id);
    },
    async getKeepsByVaultId({ commit, dispatch }, id) {
      let data = await api.get("vaultkeeps/" + id + "/keeps");
      commit("setResource", { name: "activeKeeps", data: data.data });
    },
    async viewKeep({ commit, dispatch }, id) {
      let data = await api.put("keeps/" + id + "/view");
      commit("setViews", data.data);
    },
    async keepKeep({ commit, dispatch }, id) {
      let data = await api.put("keeps/" + id + "/keep");
    },
    async shareKeep({ commit, dispatch }, id) {
      let data = await api.put("keeps/" + id + "/share");
    },
    async createKeep({ commit, dispatch }, keep) {
      let data = await api.post("keeps", keep);
      commit("addResourceToArray", { name: "keeps", data: data.data });
    },
    async createVault({ commit, dispatch }, vault) {
      let data = await api.post("vaults", vault);
      commit("addResourceToArray", { name: "vaults", data: data.data });
    },
    async addKeepToVault({ commit, dispatch }, payload) {
      let data = await api.post("vaultkeeps", payload);
      dispatch("keepKeep", payload.keepId);
    },
    async removeKeepFromVault({ commit, dispatch }, payload) {
      let data = await api.delete(
        "vaultkeeps/" + payload.vaultId + "/keeps/" + payload.keepId
      );
      commit("removeKeepFromVault", payload.keepId);
    },
    async getMyKeeps({ commit, dispatch }) {
      let data = await api.get("/keeps/user");
      commit("setResource", { name: "myKeeps", data: data.data });
    },
    async deleteKeep({ commit, dispatch }, id) {
      await api.delete("/keeps/" + id);
      commit("deleteKeep", id);
    },
    async deleteVault({ commit, dispatch }, id) {
      await api.delete("/vaults/" + id);
      commit("deleteVault", id);
    }
  }
});
