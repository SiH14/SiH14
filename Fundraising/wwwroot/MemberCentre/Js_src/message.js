const app = {
  data() {
    return {
      userid: 1,
      touser: "",
      message: "",
      messages: [],
      connection: null,
    };
  },
  mounted() {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl("/chatHub")
      .build();
    this.connection
      .start()
      .then(() => {
        this.connection.invoke("Connect", parseInt(this.userid));
      })
      .catch((err) => console.error(err.toString()));

    this.connection.on("ReceiveMessage", (message, userid) => {
      this.messages.push(`來自${userid}:` + message);
    });
  },
  methods: {
    connect() {
      if (this.connection) {
        this.connection.stop();
      }
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl("/chatHub")
        .build();

      this.connection
        .start()
        .then(() => {
          this.connection.invoke("Connect", parseInt(this.userid));
        })
        .catch((err) => console.error(err.toString()));

      this.connection.on("ReceiveMessage", (message, userid) => {
        this.messages.push(message + "來自" + userid);
      });

      alert("連接成功");
    },
    sendToUser() {
      this.connection
        .invoke(
          "SendMessageToUser",
          this.touser,
          this.message,
          parseInt(this.userid)
        )
        .catch((err) => console.error(err.toString()));
      this.messages.push("我說:" + this.message);
      this.message = "";
    },
  },
};

Vue.createApp(app).mount("#app");
