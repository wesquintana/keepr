<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-dark text-light">
    <router-link class="navbar-brand" :to="{ name: 'home' }">Keepr</router-link>
    <button
      class="navbar-toggler"
      type="button"
      data-toggle="collapse"
      data-target="#navbarText"
      aria-controls="navbarText"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav mr-auto">
        <li
          class="nav-item"
          v-if="$auth.isAuthenticated"
          :class="{ active: $route.name == 'dashboard' }"
        >
          <router-link class="nav-link" :to="{ name: 'dashboard' }"
            >My-Dashboard</router-link
          >
        </li>
      </ul>
      <span class="navbar-text">
        <button
          class="btn btn-success"
          @click="login"
          v-if="!$auth.isAuthenticated"
        >
          Login
        </button>
        <button class="btn btn-danger" @click="logout" v-else>logout</button>
      </span>
    </div>
  </nav>
</template>

<script>
import axios from "axios";

export default {
  name: "Navbar",
  methods: {
    async login() {
      try {
        await this.$auth.loginWithPopup();
        await this.$auth.getUserData();
        this.$store.dispatch("setBearer", this.$auth.bearer);
      } catch (e) {
        console.error(e);
      }
    },
    async logout() {
      await this.$auth.logout();
      this.$store.dispatch("resetBearer");
      this.$router.push({ name: "home" });
    }
  }
};
</script>

<style></style>
