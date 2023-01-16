const app = {
  data() {
    return {
      pjorderlist: [],
      orderdetail: {
        recipientName: "王思呈",
        recipientPhone: 123,
        recipientMail: "123",
        recipientAddress: "234324fx",
        note: "你好",
        planTitle: "",
        planPrice: "",
        addSponsorship: "",
      },
    };
  },
  mounted() {
    axios.get("/api/UserOrder/ProjectOrder/15").then((res) => {
      res.data.forEach((element) => {
        if (element.orderStateId == 1) {
          element.orderStateId = "待開始";
        } else if (element.orderStateId == 2) {
          element.orderStateId = "備貨中";
        } else if (element.orderStateId == 3) {
          element.orderStateId = "已寄送";
        } else if (element.orderStateId == 4 || element.orderStateId == 5) {
          element.orderStateId = "已取消";
        }
      });

      this.pjorderlist = res.data;
    });

    // 取消id事件監聽資料帶入
    this.$refs.box.addEventListener("show.bs.modal", (event) => {
      let button = event.relatedTarget;
      let thisorder = button.getAttribute("data-bs-whatever");
      axios
        .get(url, params)
        .then((res) => {
          console.log(res);
        })
        .catch((err) => {
          console.error(err);
        });
    });
  },
};

Vue.createApp(app).mount("#app");
