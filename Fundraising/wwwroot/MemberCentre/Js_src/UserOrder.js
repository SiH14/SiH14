// // 假設已登入
// sessionStorage.setItem("userName", "123");

const app = {
  data() {
    return {
      cancelform: {
        orderId: "",
        RefundResult: "",
      },
      ordercard: [],
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
      this.cancelform.orderId = button.getAttribute("data-bs-whatever");
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
              swal("已送出取消申請!", "待人員確認後會進行退款", {
                button: "Click Me!",
              });
            });
        });
      });
    },
    ordersession(e) {
      sessionStorage.setItem("orderId", e);
    },
  },
};

Vue.createApp(app).mount("#app");
