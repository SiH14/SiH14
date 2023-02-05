Vue.use(VueLoading);

const app = new Vue({
  el: "#app",
  data: {
    myfollow: [],
  },
  components: {
    Loading: VueLoading,
  },
  mounted() {
    let loader = this.$loading.show({
      loader: "dots",
    });

    axios.get("/api/login/getuserid").then((res) => {
      // 拿取追蹤中的專案
      axios.get("/api/UserFollowing/my/" + res.data).then((res) => {
        this.myfollow = res.data;
        setTimeout(() => loader.hide(), 600);
      });
    });
  },
  methods: {
    cancel(e) {
      axios
        .delete(`/api/UserFollowing/${e.userId}/${e.productId}`)
        .then((res) => {
          axios.get("/api/login/getuserid").then((res) => {
            // 拿取追蹤中的專案
            axios.get("/api/UserFollowing/my/" + res.data).then((res) => {
              this.myfollow = res.data;
              swal("已取消追蹤", e.ptitle, "success", { button: "確定" });
            });
          });
        });
    },
    getproductID(e) {
      sessionStorage.setItem("productId", e.productId);
    },
  },
});
