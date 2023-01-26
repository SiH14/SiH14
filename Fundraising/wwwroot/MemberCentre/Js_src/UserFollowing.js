const app = {
  data() {
    return {
      myfollow: [],
    };
  },
  mounted() {
    axios.get("/api/login/getuserid").then((res) => {
      // 拿取追蹤中的專案
      axios.get("/api/UserFollowing/my/" + res.data).then((res) => {
        this.myfollow = res.data;
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
            });
          });
        });
    },
  },
};
Vue.createApp(app).mount("#app");
