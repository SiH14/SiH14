const orderdetail = {
  data() {
    return {
      myorder: {},
    };
  },
  mounted() {
    axios
      .get("/api/UserOrder/myorder/" + sessionStorage.getItem("orderdetailId"))
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
        axios.put("/api/UserOrder/" + this.myorder.orderId, order).then(() => {
          window.location.replace("./userorder.html");
        });
      });
    },
  },
};

Vue.createApp(orderdetail).mount("#app");
