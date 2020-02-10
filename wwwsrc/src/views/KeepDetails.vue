<template>
  <div class="keep-details container-fluid">
    <div class="row">
      <div class="col-9">
        <div class="row">
          <div class="col-12">
            <img :src="activeKeep.img" alt />
            <h1>{{activeKeep.name}}</h1>
          </div>
          <div class="col-12">
            <p>{{activeKeep.description}}</p>
          </div>
        </div>
      </div>
      <div class="col-3">
        <div class="row">
          <div class="col-4">Views: {{activeKeep.views}}</div>
          <div class="col-4">Shares: {{activeKeep.shares}}</div>
          <div class="col-4">Keeps: {{activeKeep.keeps}}</div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "KeepDetails",
  data() {
    return {};
  },
  mounted() {
    this.$store.dispatch("getKeepById", this.$route.params.id);
  },
  computed: {
    activeKeep() {
      return this.$store.state.activeKeep;
    }
  },
  methods: {}
};
</script>
<style>
</style>