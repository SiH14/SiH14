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
    axios.get("/api/userinfo/1").then((res) => {
      this.userdata = res.data;
    });
    axios.get("/api/userinfo/ProductList/1").then((res) => {
      this.ProductsList = res.data;
    });
  },
}).mount("#app");
