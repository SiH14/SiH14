const app = {
  data() {
    return {
      msg: { fromUserID: "", toUserID: "", contact: "" },
      messages: [],
      connection: null,
    };
  },
  mounted() {
    axios.get("/api/login/getuserid").then((res) => {
      this.msg.fromUserID = res.data;

      axios.get("/api/Messages").then((res) => {
        this.messages = res.data;
      });
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
      //接收訊息
      this.connection.on("ReceiveMessage", (fromuser, touser, message) => {
        this.messages.push({
          fromUserId: fromuser,
          toUserId: touser,
          contact: message,
        });
      });
    });
  },
  methods: {
    sendToUser() {
      this.connection
        .invoke(
          "SendMessageToUser",
          this.msg.fromUserID,
          this.msg.toUserID,
          this.msg.contact
        )
        .then(() =>
          axios.post("/api/Messages", this.msg).then((res) => {
            axios.get("/api/Messages").then((res) => {
              this.messages = res.data;
            });
          })
        )
        .catch((err) => console.error(err.toString()));
    },
  },
};

Vue.createApp(app).mount("#app");
