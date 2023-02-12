Vue.use(VueLoading);

const app = new Vue({
  el: "#app",
  data: {
    userinfo: {},
  },
  components: {
    Loading: VueLoading,
  },
  mounted() {
    let loader = this.$loading.show({
      loader: "dots",
    });

    axios.get("/api/login/getuserid").then((res) => {
      axios.get("/api/userinfo/setting/" + res.data).then((res) => {
        this.userinfo = res.data;
        setTimeout(() => loader.hide(), 400);
      });
    });
  },
  methods: {
    readURL(e) {
      if (e.target.files && e.target.files[0]) {
        var file = e.target.files[0];
        var reader = new FileReader();
        reader.onload = (e) => {
          document.querySelector("#output").src = e.target.result;
          this.userinfo.userPhoto = e.target.result;
        };
        reader.readAsDataURL(file);
      }
    },
    submitSetting() {
      axios
        .put("/api/userinfo/setting/" + this.userinfo.userId, this.userinfo)
        .then(() => {
          swal("儲存成功！", "", "success", { button: "確定" }).then(() => {
            history.go(0);
          });
        });
    },
  },
});
