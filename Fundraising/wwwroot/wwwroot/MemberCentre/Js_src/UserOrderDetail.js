const orderdetail = {
  data() {
    return {
      myorder: {
        orderId: 999,
        orderStateId: 1,
        purchaseTime: "2023-01-01",
        productId: 0,
        productTitle: "測試專案標題",
        productPhoto: "",
        startTime: "2022-01-01",
        endTime: "2023-12-31",
        currentAmount: 0,
        targetAmount: 0,
        planId: 0,
        planTitle: "",
        planContent: "",
        planPrice: 0,
        addSponsorship: 0,
        recipientName: "",
        recipientPhone: "",
        recipientMail: " ",
        recipientAddress: "",
        note: "",
      },
    };
  },
  mounted() {
    axios
      .get("/api/UserOrder/myorder/" + sessionStorage.getItem("orderId"))
      .then((res) => {
        this.myorder = res.data;
      });
  },
  methods: {
    reciSubmit() {
      axios.get("/api/userorder/" + this.myorder.orderId).then((res) => {
        let order = res.data;
        order.recipientName = this.myorder.recipientName;
        order.recipientPhone = this.myorder.recipientPhone;
        order.recipientMail = this.myorder.recipientMail;
        order.recipientAddress = this.myorder.recipientAddress;
        order.note = this.myorder.note;
        axios
          .put("/api/UserOrder/" + this.myorder.orderId, order)
          .then((res) => {
            window.location.replace("./userorder.html");
          });
      });
    },
  },
};

Vue.createApp(orderdetail).mount("#app");
