let header = `<nav class="headernav navbar navbar-expand-lg navbar-light bg-white border-bottom" id="header">

<div class="container-fluid headernav" id="header-container">
    <!--LOGO -->
    <a class="navbar-brand fs-2 headernav" id="header-brand" href="#">咱的募資平台</a>

    <!-- Bar -->

    <div class="collapse navbar-collapse ms-5 headernav" id="headerlinkbar">
        <!-- LEFT -->
        <ul class="navbar-nav me-auto fs-5 headernav">
            <li class="nav-item headernav">
                <a class="nav-link headernav" href="#" id="header-home">首頁</a>
            </li>
            <li class="nav-item headernav" id="header-items">
                <a class="nav-link headernav" href="./myproposal.html">提案</a>
            </li>
            <li class="nav-item headernav" id="header-items">
                <a class="nav-link headernav" href="#">探索</a>
            </li>

        </ul>

    </div>

    <div class="headernav">
        <ul class="navbar-nav me-5 headernav">
            <li class="headernav" style="padding-right: 10px;">
                <div class="search headernav">                
                    <a class="icon headernav" href="#" role="button"></a>
                    <div class="input headernav">
                        <input class="headernav" type="text" placeholder="請輸入關鍵字" id="mysearch">
                    </div>
                    <span class="clear headernav"></span>
                </div>
            </li>
            <div class="dropdown headernav">
            <a class="btn dropdown-toggle headernav" href="#" role="button" id="dropdownMenuLink" data-bs-toggle="dropdown" aria-expanded="false">
            <img style="width:55%" class="headernav loginicon" src="../img/loginicon.png" alt="">
            </a>
          
            <ul class="dropdown-menu headernav" aria-labelledby="dropdownMenuLink">
            <div class="dropdown-div headernav">
            <div>
            <a style="border-bottom: solid 1px rgb(188, 185, 185);" class="dropdown-item headernav" href="#">註冊</a>
            </div>
            <div>
            <a class="dropdown-item headernav" href="#">登入</a>
            </div>
            </div>              
            </ul>
          </div>
            
        </ul>
    </div>

</div>
</nav>`+ `<div id="MemberCentre" class="row text-dark py-3 fs-5 justify-content-center m-0">
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
</div>`

let footer = `<footer class="text-dark pt-5 pb-4" style="background-color: #0a2647">

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

    <div class="row align-items-center">

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
const icon = document.querySelector('.icon');
const search = document.querySelector('.search');
const clear = document.querySelector('.clear');
const dropdowntoggle = document.querySelector('.dropdowntoggle');
const dropdownmenu = document.querySelector('.dropdownmenu');
icon.onclick = function () {
    search.classList.toggle('active');
    icon.classList.toggle('active');
}

clear.onclick = function () {
    document.getElementById('mysearch').value = ''
}


$("#mysearch").keydown(function (e) {
    if (e.keyCode == 13) {//触发键盘事件enter
        window.location = "https://www.youtube.com/?themeRefresh=1";
    }
});

document.addEventListener('mousedown', (e) => {
    var apple = e.target.classList.value
    var bee = String(apple)
    if (bee.indexOf("headernav") == -1) {
        document.getElementById('mysearch').value = '';
        search.classList.remove('active');
        // icon.classList.remove('active');
    }
})
