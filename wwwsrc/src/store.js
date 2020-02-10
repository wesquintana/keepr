import Vue from "vue";
import Vuex from "vuex";
import Axios from "axios";
import router from "./router";

Vue.use(Vuex);

let baseUrl = location.host.includes("localhost")
  ? "https://localhost:5001/"
  : "/";

let api = Axios.create({
  baseURL: baseUrl + "api/",
  timeout: 3000,
  withCredentials: true
});

export default new Vuex.Store({
  state: {
    keeps: [],
    activeKeep: {},
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
    }
  },
  actions: {
    setBearer({}, bearer) {
      api.defaults.headers.authorization = bearer;
    },
    resetBearer() {
      api.defaults.headers.authorization = "";
    },
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
      commit("setResource", { name: "vaults", data: data.data });
    },
    async viewKeep({ commit, dispatch }, id) {
      let data = await api.put("keeps/" + id + "/view");
      commit("setViews", data.data);
    },
    async createKeep({ commit, dispatch }, keep) {
      let data = await api.post("keeps", keep);
      commit("addResourceToArray", { name: "keeps", data: data.data });
    },
    async createVault({ commit, dispatch }, vault) {
      let data = await api.post("vaults", vault);
      commit("addResourceToArray", { name: "vaults", data: data.data });
    }
  }
});
