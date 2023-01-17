Vue.createApp({
  data() {
    return {
      userdata: {},
      ProductsList: [],
    };
  },
  methods: {
    ShowMyOrders() {
      document.querySelector("#OrdersRow").style.display = "flex";
      document.querySelector("#ProductsRow").style.display = "none";
    },
    ShowMyProducts() {
      document.querySelector("#OrdersRow").style.display = "none";
      document.querySelector("#ProductsRow").style.display = "flex";
    },
  },
  mounted() {
    axios.get("/api/login/getuserid").then((res) => {
      // 拿userdata
      axios.get("/api/userinfo/" + res.data).then((res) => {
        this.userdata = res.data;
      });
      // 拿ProductsList
      axios.get("/api/userinfo/ProductList/" + res.data).then((res) => {
        this.ProductsList = res.data;
      });
    });
  },
}).mount("#app");
