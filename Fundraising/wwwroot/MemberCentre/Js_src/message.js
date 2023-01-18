const app = {
  data() {
    return {
      msg: { fromUserId: "", toUserId: 2, contact: "" },
      messages: [],
      connection: null,
    };
  },
  mounted() {
    // 初始化
    axios.get("/api/login/getuserid").then((res) => {
      this.msg.fromUserId = res.data;
      // 連線
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl("/chatHub")
        .build();
      this.connection
        .start()
        .then(() => {
          this.connection.invoke("Connect", res.data);
        })
        .catch((err) => console.error(err.toString()));
      //拿取對話紀錄
      axios.get("/api/Messages").then((res) => {
        this.messages = res.data;
      });
    });
    //接收訊息
    this.connection.on("ReceiveMessage", (fromuser, touser, message) => {
      this.messages.push({
        fromUserId: fromuser,
        toUserId: touser,
        contact: message,
      });
    });
  },
  methods: {
    sendToUser() {
      this.connection
        .invoke(
          "SendMessageToUser",
          this.msg.fromUserId,
          this.msg.toUserId,
          this.msg.contact
        )
        .then(() => {
          this.messages.push({
            fromUserId: this.msg.fromUserId,
            toUserId: this.msg.toUserId,
            contact: this.msg.contact,
          });
          axios
            .post("/api/Messages", this.msg)
            .then((res) => {
              console.log(res.data);
            })
            .catch(() => {
              this.messages.pop();
            });
          this.msg.contact = "";
        })
        .catch((err) => console.error(err.toString()));
    },
  },
};

Vue.createApp(app).mount("#app");
