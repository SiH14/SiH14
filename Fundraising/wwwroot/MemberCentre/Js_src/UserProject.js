const app = {
  data() {
    return {
      prodlist: [
        {
          productId: 20,
          coverphoto: "",
          productTitle:
            "香草城市 智能版 Herb City Connect｜專屬APP藍牙無線操作 時尚智能盆栽",
          startime: "2022-12-15",
          endtime: "2023-01-17",
          targetAmount: 888888,
          currentAmount: 2760,
          productStateId: 3,
        },
      ],
    };
  },
  mounted() {
    axios.get("/api/UserProject/prodlist/7").then((res) => {
      res.data.forEach((element) => {
        if (element.productStateId == 1) {
          element.productStateId = "審核中";
        } else if (element.productStateId == 2) {
          element.productStateId = "審核不通過";
        } else if (
          element.productStateId == 3 &&
          new Date(element.endtime) > new Date()
        ) {
          element.productStateId = "募資中";
        } else if (
          element.productStateId == 3 &&
          new Date(element.endtime) < new Date() &&
          element.currentAmount > element.targetAmount
        ) {
          element.productStateId = "募資成功";
        } else if (
          element.productStateId == 3 &&
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
