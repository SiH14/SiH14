// 假設已登入
sessionStorage.setItem("userName", "123");

const order = {
  data() {
    return {
      orderlist: [
        {
          orderId: 9,
          orderDateId: "20230103001 ",
          userId: 4,
          recipientName: "陳媽媽",
          recipientPhone: "0932143233",
          recipientAddress: "台中市南屯區南京路11號",
          note: "",
          masterCardId: "1234567812345678",
          orderStateId: 1,
          purchaseTime: "2022-12-23T00:00:00",
          orderState: null,
          user: null,
          orderDetails: [],
          refunds: [],
        },
        {
          orderId: 9,
          orderDateId: "20230103002 ",
          userId: 4,
          recipientName: "王媽媽",
          recipientPhone: "0932143233",
          recipientAddress: "台中市南屯區南京路11號",
          note: "",
          masterCardId: "1234567812345678",
          orderStateId: 1,
          purchaseTime: "2022-12-23T00:00:00",
          orderState: null,
          user: null,
          orderDetails: [],
          refunds: [],
        },
      ],
    };
  },
  mounted() {
    // axios.get("/api/orders").then((res) => {
    //   this.orderlist = res.data;
    //   console.log(this.orderlist);
    // });
  },
};

Vue.createApp(order).mount("#OrderCard");
