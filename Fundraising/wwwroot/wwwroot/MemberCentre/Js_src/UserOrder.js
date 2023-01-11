// // 假設已登入
// sessionStorage.setItem("userName", "123");

const order = {
  data() {
    return {
      cancelform: {
        orderId: "",
        RefundResult: "",
      },
      ordercard: [
        {
          userId: 999,
          orderId: 999,
          orderStateId: 1,
          purchaseTime: "2023-01-05",
          productId: 999,
          productTitle: "測試專案標題",
          productPhoto: "../img/product.png",
          startTime: "2022-11-25",
          endTime: "2023-03-03",
          currentAmount: 20000,
          targetAmount: 100000,
          planId: 7,
          planTitle: "測試方案標題",
          planContent: "測試方案內容",
          planPrice: 9999,
          addSponsorship: 1,
        },
      ],
      cancelput: {},
    };
  },
  mounted() {
    // ordercard資料帶入
    axios.get("/api/userorder/list/1").then((res) => {
      this.ordercard = res.data;
    });

    // 取消id事件監聽資料帶入
    this.$refs.box.addEventListener("show.bs.modal", (event) => {
      let button = event.relatedTarget;
      let oidforcancel = button.getAttribute("data-bs-whatever");
      this.cancelform.orderId = oidforcancel;
    });
  },
  methods: {
    confirmCancel() {
      axios.post("/api/Refunds", this.cancelform).then((res) => {
        axios.get("/api/userorder/" + res.data.orderId).then((res) => {
          let cancelput = res.data;
          cancelput.orderStateId = 4;
          axios
            .put("/api/userorder/" + cancelput.orderId, cancelput)
            .then(() => {
              window.location.reload();
            });
        });
      });
    },
    ordersession(e) {
      sessionStorage.setItem("orderId", e);
    },
  },
};

Vue.createApp(order).mount("#OrderCard");
