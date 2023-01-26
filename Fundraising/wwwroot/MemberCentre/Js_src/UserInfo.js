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
      document.querySelector(".selectedbtn").classList.remove("selectedbtn");
      document.querySelector("#orderbtn").classList.add("selectedbtn");
    },
    ShowMyProducts() {
      document.querySelector("#OrdersRow").style.display = "none";
      document.querySelector("#ProductsRow").style.display = "flex";
      document.querySelector(".selectedbtn").classList.remove("selectedbtn");
      document.querySelector("#productbtn").classList.add("selectedbtn");
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
