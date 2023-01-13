const app = {
  data() {
    return {
      prodlist: [],
    };
  },
  mounted() {
    axios.get("/api/UserProject/prodlist/2").then((res) => {
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
          alert("錯誤");
        }
      });
      this.prodlist = res.data;
    });
  },
};

Vue.createApp(app).mount("#app");
// 審核中
// 審核不通過;
// 募資中;
// 募資成功;
// 生產階段;
// 寄送產品;
// 募資失敗（未達標）
