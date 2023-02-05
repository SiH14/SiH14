Vue.use(VueLoading);

Vue.filter('money', function (num) {
    const parts = num.toString().split('.');
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    return parts.join('.');
});

const app = new Vue({
  el: "#app",
  data: {
    cancelform: {
      orderId: "",
      RefundResult: "",
    },
    ordercard: [],
    cancelput: {},
    chatting: "",
    messageContent: "",
    thisuser: "",
  },
  components: {
    Loading: VueLoading,
  },
  mounted() {
    // loading overlay
    let loader = this.$loading.show({
      loader: "dots",
    });
    axios.get("/api/login/getuserid").then((res) => {
      // ordercard資料帶入
      axios.get("/api/userorder/list/" + res.data).then((res) => {
        this.ordercard = res.data;
        setTimeout(() => loader.hide(), 400);
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
    });

    // modal觸發事件
    // 傳送訊息
    this.$refs.chat.addEventListener("show.bs.modal", (event) => {
      let button = event.relatedTarget;
      this.thisuser = parseInt(button.getAttribute("data-bs-whatever"));
      console.log(button.getAttribute("data-bs-whatever"));
      axios.get("/api/UserInfo/name/" + this.thisuser).then((res) => {
        this.chatting = res.data;
      });
    });
    // 取消id事件監聽資料帶入
    this.$refs.box.addEventListener("show.bs.modal", (event) => {
      let button = event.relatedTarget;
      this.cancelform.orderId = button.getAttribute("data-bs-whatever");
    });
  },
  methods: {
    // post取消表單
    confirmCancel() {
      axios.post("/api/Refunds", this.cancelform).then((res) => {
        // 然後更改order狀態
        axios.get("/api/userorder/" + res.data.orderId).then((res) => {
          let cancelput = res.data;
          cancelput.orderStateId = 4;
          axios
            .put("/api/userorder/" + cancelput.orderId, cancelput)
            .then(() => {
              swal("已送出取消申請", "待人員確認後會進行退款", "success", {
                button: "確定",
              }).then((x) => {
                history.go(0);
              });
            });
        });
      });
    },
    ordersession(e) {
      sessionStorage.setItem("orderdetailId", e);
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
