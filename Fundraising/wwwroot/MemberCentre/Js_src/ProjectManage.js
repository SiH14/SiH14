Vue.use(VueLoading);

const app = new Vue({
  el: "#app",
  data: {
    pjorderlist: [],
    orderdetail: {},
    showed: "全部贊助",
    changed: [],
    changedData: [],
    messageContent: "",
    chatting: "",
    thisuser: "",
    connection: null,
  },
  components: {
    Loading: VueLoading,
  },
  mounted() {
    let loader = this.$loading.show({
      loader: "dots",
    });
    // 初始載入資料
    axios
      .get("/api/UserOrder/ProjectOrder/" + sessionStorage.getItem("pmId"))
      .then((res) => {
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
        setTimeout(() => loader.hide(), 600);
      });

    // modal觸發事件
    // 傳送訊息
    this.$refs.chat.addEventListener("show.bs.modal", (event) => {
      let button = event.relatedTarget;
      this.thisuser = parseInt(button.getAttribute("data-bs-whatever"));
      axios.get("/api/UserInfo/name/" + this.thisuser).then((res) => {
        this.chatting = res.data;
      });
    });
    // 查看訂單詳細
    this.$refs.box.addEventListener("show.bs.modal", (event) => {
      let button = event.relatedTarget;
      let thisorder = button.getAttribute("data-bs-whatever");
      axios.get("/api/UserOrder/myorder/" + thisorder).then((res) => {
        this.orderdetail = res.data;
      });
    });

    axios.get("/api/login/getuserid").then((res) => {
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
                swal("儲存成功！", "", "success", { button: "確定" }).then(() => {
                    history.go(0);
                });
            });
        });
      }
    },
    send() {
      if (this.messageContent != "") {
        axios.get("/api/login/getuserid").then((res) => {
          let userId = res.data;
          axios
            .get(`/api/chatrooms/chat/${userId}/${this.thisuser}`)
            .then((res) => {
              let now = new Date();
              let year = now.getFullYear();
              let month = String(now.getMonth() + 1).padStart(2, "0");
              let day = String(now.getDate()).padStart(2, "0");
              let hour = now.getHours();
              let min = now.getMinutes();
              let currentime =
                year + "-" + month + "-" + day + " " + hour + ":" + min;
              let sender;
              let receiver;
              let chatroomId;
              // 假如已有聊天室
              if (res.data) {
                sender = userId;
                receiver = this.thisuser;
                chatroomId = res.data.chatroomId;

                // 寫進資料庫
                axios
                  .post("/api/Messages", {
                    SenderID: sender,
                    ReceiverID: receiver,
                    ChatroomID: chatroomId,
                    MessageContent: this.messageContent,
                  })
                  .then((res) => {
                    this.connection
                      .invoke(
                        "SendMessageToUser",
                        sender,
                        receiver,
                        chatroomId,
                        this.messageContent,
                        currentime
                      )
                      .then(() => {
                        // 清空input
                        this.messageContent = "";
                      });
                  });
              }
              // 無聊天室，需新增聊天室後送出
              else {
                axios
                  .post("/api/chatrooms", {
                    userId1: userId,
                    userId2: this.thisuser,
                  })
                  .then((res) => {
                    sender = userId;
                    receiver = this.thisuser;
                    chatroomId = res.data.chatroomId;

                    // 寫進資料庫
                    axios
                      .post("/api/Messages", {
                        SenderID: sender,
                        ReceiverID: receiver,
                        ChatroomID: chatroomId,
                        MessageContent: this.messageContent,
                      })
                      .then((res) => {
                        this.connection
                          .invoke(
                            "SendMessageToUser",
                            sender,
                            receiver,
                            chatroomId,
                            this.messageContent,
                            currentime
                          )
                          .then(() => {
                            // 清空input
                            this.messageContent = "";
                          });
                      });
                  });
              }
            });
        });
      }
    },
  },
});
