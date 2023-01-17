const app = {
  data() {
    return {
      pjorderlist: [],
      orderdetail: {},
      showed: "全部贊助",
      changed: [],
      changedData: [],
    };
  },
  mounted() {
    // 初始載入資料
    axios.get("/api/UserOrder/ProjectOrder/15").then((res) => {
      res.data.forEach((element) => {
        if (element.orderStateId == 1) {
          element.orderStateId = "待開始";
        } else if (element.orderStateId == 2) {
          element.orderStateId = "備貨中";
        } else if (element.orderStateId == 3) {
          element.orderStateId = "已寄送";
        } else if (element.orderStateId == 4 || element.orderStateId == 5) {
          element.orderStateId = "已取消";
        }
      });
      this.pjorderlist = res.data;
    });

    // 取消id事件監聽資料帶入
    this.$refs.box.addEventListener("show.bs.modal", (event) => {
      let button = event.relatedTarget;
      let thisorder = button.getAttribute("data-bs-whatever");
      axios.get("/api/UserOrder/myorder/" + thisorder).then((res) => {
        this.orderdetail = res.data;
      });
    });
  },
  methods: {
    filter(e) {
      document
        .querySelector(".btn-secondary")
        .classList.remove("btn-secondary");
      e.target.classList.add("btn-secondary");
      this.showed = e.target.value;
    },
    statechange(e) {
      let oid = e.target.value.split(",", 2).map((x) => parseInt(x))[0];
      let sid = e.target.value.split(",", 2).map((x) => parseInt(x))[1];
      let muti = 0;
      this.changed.forEach((element) => {
        if (element == oid) {
          muti = 1;
        }
      });

      if (muti == 0) {
        this.changed.push(oid);
        axios.get("/api/UserOrder/" + oid).then((res) => {
          res.data.orderStateId = sid;
          this.changedData.push(res.data);
        });
      }
    },
    submitstate() {
      if (this.changed.length > 0) {
        this.changedData.forEach((element) => {
          axios
            .put("/api/UserOrder/" + element.orderId, element)
            .then((res) => {
              alert("儲存成功");
              window.location.reload();
            });
        });
      }
    },
  },
};

Vue.createApp(app).mount("#app");
