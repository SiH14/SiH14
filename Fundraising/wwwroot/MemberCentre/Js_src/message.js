const app = {
  data() {
    return {
      userid: "",
      chatroom: [],
      chatting: "",
      chattingname: "",
      msg: { chatroom: "", content: "" },
      messages: [],
      connection: null,
    };
  },
  mounted() {
    // 初始化
    axios.get("/api/login/getuserid").then((res) => {
      this.userid = res.data;
      // 連線
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl("/chatHub")
        .build();
      this.connection
        .start()
        .then(() => {
          this.connection.invoke("Connect", res.data);
          //接收訊息
          this.connection.on(
            "ReceiveMessage",
            (from, to, chatroom, content, time) => {
              if (this.chatting == from) {
                this.messages.push({
                  chatroomId: chatroom,
                  sender: from,
                  receiver: to,
                  content: content,
                  sentTime: time,
                });
              }
            }
          );
        })
        .catch((err) => console.error(err.toString()));
      // 拿取對話清單
      axios.get("/api/chatrooms/chats/" + res.data).then((res) => {
        this.chatroom = res.data;
      });
    });
  },
  methods: {
    // 發送訊息
    sendToUser() {
      //時間格式化
      let now = new Date();
      let year = now.getFullYear();
      let month = String(now.getMonth() + 1).padStart(2, "0");
      let day = now.getDate();
      let hour = now.getHours();
      let min = now.getMinutes();
      let currentime = year + "-" + month + "-" + day + " " + hour + ":" + min;

      if (this.msg.content != "") {
        this.connection
          .invoke(
            "SendMessageToUser",
            this.userid,
            this.chatting,
            this.msg.chatroom,
            this.msg.content,
            currentime
          )
          .then(() => {
            // 寫進資料庫
            axios
              .post("/api/Messages", {
                SenderID: this.userid,
                ReceiverID: this.chatting,
                ChatroomID: this.msg.chatroom,
                MessageContent: this.msg.content,
              })
              .then((res) => {
                console.log(res.data);
              });
            // 對話新增(畫面)
            this.messages.push({
              chatroomId: this.msg.chatroom,
              sender: this.userid,
              receiver: this.chatting,
              content: this.msg.content,
              sentTime: currentime,
            });
            // 清空input
            this.msg.content = "";
          });
      }
    },
    // tab class切換
    nowchatting(e) {
      if (document.querySelector(".chatting") != null) {
        document.querySelector(".chatting").classList.remove("chatting");
      }
      e.currentTarget.classList.add("chatting");
    },
    // 目前對話對象
    getMsg(e) {
      this.messages = [];
      this.chatting = e.userId;
      this.chattingname = e.userName;
      this.msg.chatroom = e.chatroomId;
      axios.get("/api/messages/detail/" + e.chatroomId).then((res) => {
        this.messages = res.data;
      });
    },
  },
};

Vue.createApp(app).mount("#app");
