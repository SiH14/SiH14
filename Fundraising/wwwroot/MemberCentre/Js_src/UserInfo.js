Vue.use(VueLoading);

const app = new Vue({
  el: "#app",
  data: {
    userdata: {},
    ProductsList: [],
    OrdersList: [],
  },
  components: {
    Loading: VueLoading,
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
    let loader = this.$loading.show({
      loader: "dots",
    });

    axios.get("/api/login/getuserid").then((res) => {
      // 拿userdata
      axios.get("/api/userinfo/" + res.data).then((res) => {
        this.userdata = res.data;
      });
      // 拿ProductsList
      axios.get("/api/userinfo/ProductList/" + res.data).then((res) => {
        this.ProductsList = res.data;
      });
      // 拿OrdersList
      axios.get("/api/userinfo/OrderList/" + res.data).then((res) => {
        this.OrdersList = res.data;
        setTimeout(() => loader.hide(), 600);
      });
    });
  },
});
