let header =
    `<nav class="headernav navbar navbar-expand-lg navbar-light bg-white border-bottom" id="header">

<div class="container-fluid headernav" id="header-container">
    <!--LOGO -->
    <a class="navbar-brand fs-2 headernav" id="header-brand" href="../ProductPage/mymainpage.html">咱的募資平台</a>

    <!-- Bar -->

    <div class="collapse navbar-collapse ms-5 headernav" id="headerlinkbar">
        <!-- LEFT -->
        <ul class="navbar-nav me-auto fs-5 headernav">
            <li class="nav-item headernav">
                <a class="nav-link headernav" href="../ProductPage/mymainpage.html" id="header-home">首頁</a>
            </li>
            <li class="nav-item headernav" id="header-items">
                <a class="nav-link headernav" href="../Proposal/myproposal.html">提案</a>
            </li>
            <li class="nav-item headernav" id="header-items">
                <a class="nav-link headernav" href="#">探索</a>
            </li>

        </ul>

    </div>

    <div class="headernav">
        <ul id="headicon" class="navbar-nav me-5 headernav">
            <li class="headernav" style="padding-right: 10px;">
                <div class="search headernav">                
                    <a class="icon headernav" href="#" role="button"></a>
                    <div class="input headernav">
                        <input class="headernav" type="text" placeholder="搜尋專案" id="mysearch">
                    </div>
                    <span class="clear headernav"></span>
                </div>
            </li>
            <div class="dropdown">
        <a class="btn dropdown-toggle" href="#" role="button" id="dropdownMenuLink"
            data-bs-toggle="dropdown" aria-expanded="false">
            <img id="iconimg" class="headernav loginicon" src="../img/loginicon.png" alt="">
        </a>

        <ul class="dropdown-menu" aria-labelledby="dropdownMenuLink">
            <li style="border-bottom: 1px rgb(190, 186, 186) solid;"><a class="dropdown-item" href="../Registration/registration.html">註冊</a></li>
            <li><a class="dropdown-item" href="../Login/login.html">登入</a></li>
        </ul>
    </div>             
            </ul>
          </div>
          <div>
          <img class="headerbackgroudicon" src="../img/headerbackgroudicon.png" alt="">
      </div>
        
    </div>
    
</nav>` +
    `<div id="MemberCentre" class="row text-dark py-3 fs-5 justify-content-center m-0">
<a
  href="./UserInfo.html"
  class="memberlink col-4 col-xl-1 col-md-3 offset-0 btn btn-default"
  >個人頁面</a
>
<a href="./UserFollowing.html" class="memberlink col-4 col-xl-1 col-md-3 btn btn-default"
  >追蹤專案</a
>
<a href="./UserOrder.html" class="memberlink col-4 col-xl-1 col-md-3 btn btn-default"
  >贊助紀錄</a
>
<a href="./UserProject.html" class="memberlink col-4 col-xl-1 col-md-3 btn btn-default"
  >提案紀錄</a
>
<a href="./UserMessage.html" class="memberlink col-4 col-xl-1 col-md-3 btn btn-default"
  >聯絡訊息</a
>
<a href="./UserSetting.html" class="memberlink col-4 col-xl-1 col-md-3 btn btn-default"
  >帳戶設定</a
>
</div>`;

let footer = `<footer class="text-dark pt-5 pb-4">

<div class="footer-container text-center text-md-left">

    <div class="row text-center text-md-left" id="footer-text">

        <div class="col-md-3 col-lg-3 col-xl-3 mx-auto mt-3 footer-item">
            <h4 class=" mb-4 font-weight-bold">關於</h4>
            <p>關於我們</p>
            <p>人才招募</p>
            <p>商標使用規範</p>

        </div>

        <div class="col-md-3 col-lg-3 col-xl-3 mx-auto mt-3 footer-item">
            <h4 class=" mb-4 font-weight-bold">條款</h4>
            <p>使用條款</p>
            <p>提案者合約</p>

        </div>

        <div class="col-md-3 col-lg-3 col-xl-3 mx-auto mt-3 footer-item"> 
            <h4 class=" mb-4 font-weight-bold">協助</h4>
            <p>常見問題</p>
            <p>使用手冊</p>
            <p>提案百科</p>
        </div>

        <div class="col-md-3 col-lg-3 col-xl-3 mx-auto mt-3 footer-item">
            <h4 class=" mb-4 font-weight-bold">更多</h4>
            <p>品牌資源</p>
            <p>群眾觀點</p>

        </div>      

    </div>
    <div class="footericon">
    <a href="" style="text-decoration:none;">
            <img src="../img/fbicon.png" alt="">
        </a>
        <span>&nbsp</span>
        <a href="" style="text-decoration:none;">
            <img src="../img/igicon.png" alt="">
        </a>
        <span>&nbsp</span>
        <a href="" style="text-decoration:none;">
            <img src="../img/yticon.png" alt="">
        </a>
   </div>

    <hr class="mb-4" style="color: black;">

    <div class="row align-items-center footertext">

        <div class="justify-content-center">
            <p style="color: black; font-size: 0.975rem;">Copyright ©2022 Backer-Founder All rights reserved.</p>
        </div>

    </div>

</div>

</footer>`;
let querybody = document.querySelector("body");
let queryheader = document.querySelector("head");
querybody.innerHTML = header + querybody.innerHTML;
querybody.innerHTML += footer;
queryheader.innerHTML += `<link rel="stylesheet" href="../layout/layout.css"/>`;

// searchbar
const icon = document.querySelector(".icon");
const search = document.querySelector(".search");
const clear = document.querySelector(".clear");
const dropdowntoggle = document.querySelector(".dropdowntoggle");
const dropdownmenu = document.querySelector(".dropdownmenu");
icon.onclick = function () {
    search.classList.toggle("active");
    icon.classList.toggle("active");
};

clear.onclick = function () {
    document.getElementById("mysearch").value = "";
};

let mysearchkeydown = document.getElementById("mysearch");

mysearchkeydown.onkeydown = function (e) {
    if (e.keyCode == 13) {
        //触发键盘事件enter
        window.location = "https://www.youtube.com/?themeRefresh=1";
    }
};

document.addEventListener("mousedown", (e) => {
    var apple = e.target.classList.value;
    var bee = String(apple);
    if (bee.indexOf("headernav") == -1) {
        document.getElementById("mysearch").value = "";
        search.classList.remove("active");
    }
});

// get登入的userid，設置頭像
var userid = "";
window.onload = function getuserID() {

    axios.get("/api/Login/getusername")
        .then(res => {
            console.log(res.data);
            if (res.data == "未登入") {
                window.location = "../Login/login.html"
            } else {
                getid = function (callback) {
                    axios
                        .get("/api/Login/getuserid")
                        .then((res) => {
                            callback(res.data);
                            console.log(res.data);
                            // if (res.data != "") {
                            //     alert("login OK")
                            // }
                            axios
                                .get("/api/Login/getuserphoto/" + res.data)
                                .then((res) => {
                                    //console.log(res.data[0].userPhoto)
                                    var headnav = document.getElementById("headicon");
                                    headnav.style.marginTop = "4px";
                                    var setimg = document.getElementById("iconimg");
                                    setimg.setAttribute("src", res.data[0].userPhoto);
                                    setimg.style.width = "30px";
                                    setimg.style.height = "30px";
                                    setimg.style.borderRadius = "15px";
                                    setimg.style.objectFit = "cover";
                                    document.querySelector(
                                        ".dropdown-menu"
                                    ).innerHTML = ` <li style="border-bottom: 1px rgb(190, 186, 186) solid;"><a class="dropdown-item"
                                    href="../MemberCentre/UserInfo.html">個人頁面</a></li>
                            <li style="border-bottom: 1px rgb(190, 186, 186) solid;"><a class="dropdown-item"
                                    href="../MemberCentre/UserFollowing.html">追蹤專案</a></li>
                            <li style="border-bottom: 1px rgb(190, 186, 186) solid;"><a class="dropdown-item"
                                    href="../MemberCentre/UserOrder.html">贊助紀錄</a></li>
                            <li style="border-bottom: 1px rgb(190, 186, 186) solid;"><a class="dropdown-item"
                                    href="../MemberCentre/UserProject.html">提案紀錄</a></li>
                            <li style="border-bottom: 1px rgb(190, 186, 186) solid;"><a class="dropdown-item"
                                    href="../MemberCentre/UserMessage.html">聯絡訊息</a></li>
                            <li style="text-align: center;"><a class="dropdown-item" href="../ProductPage/mymainpage.html" onclick="logout()">登出</a></li>`;
                                });
                        })
                        .catch((error) => {
                            console.log(error.response);
                        });
                };
                getid(function (myuser) {
                    console.log(myuser);
                    userid = myuser;
                });
            }
        })
        .catch(error => {
            console.log(error.response);
        })

};

function logout() {
    axios
        .delete("/api/Login")
        .then((res) => {
            console.log(res);
        })
        .catch((error) => {
            console.log(error.response);
        });
}
