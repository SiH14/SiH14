Vue.use(VueLoading);

const app = new Vue({
  el: "#app",
  data: {
    prodlist: [],
  },
  components: {
    Loading: VueLoading,
  },
  mounted() {
    let loader = this.$loading.show({
      loader: "dots",
    });
    axios.get("/api/login/getuserid").then((res) => {
      axios.get("/api/UserProject/prodlist/" + res.data).then((res) => {
        res.data.forEach((element) => {
          if (element.productStateId == 1) {
            element.productStateId = "審核中";
          } else if (element.productStateId == 2) {
            element.productStateId = "審核不通過";
          } else if (new Date(element.endtime) > new Date()) {
            element.productStateId = "募資中";
          } else if (
            new Date(element.endtime) < new Date() &&
            element.currentAmount > element.targetAmount
          ) {
            element.productStateId = "募資成功";
          } else if (
            new Date(element.endtime) < new Date() &&
            element.currentAmount < element.targetAmount
          ) {
            element.productStateId = "募資失敗";
          } else {
            alert("錯誤，請聯絡客服人員");
          }
        });
        this.prodlist = res.data;
        setTimeout(() => loader.hide(), 400);
      });
    });
  },
  methods: {
    manage(e) {
      sessionStorage.setItem("pmId", e.productId);
    },
  },
});
