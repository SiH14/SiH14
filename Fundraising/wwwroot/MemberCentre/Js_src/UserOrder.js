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
    axios.get("/api/login/getuserid").then((res) => {
      // ordercard資料帶入
      axios.get("/api/userorder/list/" + res.data).then((res) => {
        this.ordercard = res.data;
      });
    });

    // 取消id事件監聽資料帶入
    this.$refs.box.addEventListener("show.bs.modal", (event) => {
      let button = event.relatedTarget;
      this.cancelform.orderId = button.getAttribute("data-bs-whatever");
    });
  },
  methods: {
    // post取消表單
    confirmCancel() {
      axios.post("/api/Refunds", this.cancelform).then((res) => {
        // 然後更改order狀態
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
      sessionStorage.setItem("orderdetailId", e);
    },
    chat(e) {
      axios.get(`/api/chatrooms/chat/${e.userId}/${e.puserId}`).then((res) => {
        if (res.data) {
          sessionStorage.setItem("chatuserId", e.puserId);
          location.href = "./UserMessage.html";
        } else {
          axios
            .post("/api/chatrooms", {
              userId1: e.userId,
              userId2: e.puserId,
            })
            .then((res) => {
              sessionStorage.setItem("chatuserId", e.puserId);
              location.href = "./UserMessage.html";
            });
        }
      });
    },
  },
};

Vue.createApp(app).mount("#app");
