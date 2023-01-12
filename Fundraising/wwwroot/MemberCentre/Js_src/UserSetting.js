const app = {
  data() {
    return {
      userinfo: {},
    };
  },
  mounted() {
    axios.get("/api/userinfo/setting/1").then((res) => {
      this.userinfo = res.data;
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
      axios.put("/api/userinfo/setting/1", this.userinfo).then((res) => {
        alert("儲存成功!");
        history.go(0);
      });
    },
  },
};

Vue.createApp(app).mount("#app");
