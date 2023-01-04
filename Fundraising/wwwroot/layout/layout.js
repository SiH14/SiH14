let header = `<nav class="navbar navbar-expand-lg navbar-light bg-white border-bottom" id="header">

<div class="container-fluid" id="header-container">
    <!--LOGO -->
    <a class="navbar-brand fs-2" id="header-brand" href="#">咱的募資平台</a>

    <!-- Bar -->

    <div class="collapse navbar-collapse ms-5" id="headerlinkbar">
        <!-- LEFT -->
        <ul class="navbar-nav me-auto fs-5">
            <li class="nav-item">
                <a class="nav-link" href="#" id="header-home">首頁</a>
            </li>
            <li class="nav-item" id="header-items">
                <a class="nav-link" href="#">提案</a>
            </li>
            <li class="nav-item" id="header-items">
                <a class="nav-link" href="#">探索</a>
            </li>

        </ul>

    </div>

    <div>
        <ul class="navbar-nav me-5">
            <li style="padding-right: 30px;">
                <div class="search">
                    <div class="icon"></div>
                    <div class="input">
                        <input type="text" placeholder="搜尋" id="mysearch">
                    </div>
                    <span class="clear" onclick="document.getElementById('mysearch').value=''"></span>
                </div>
            </li>
            <li>

                <div>
                    <button class="btn btn-success">登入</button>
                </div>

            </li>
        </ul>
    </div>

</div>
</nav>`

let footer = `<footer class="text-dark pt-5 pb-4" style="background-color: #0a2647">

<div class="container text-center text-md-left">

    <div class="row text-center text-md-left" id="footer-text">

        <div class="col-md-3 col-lg-3 col-xl-3 mx-auto mt-3">
            <h4 class="text-uppercase mb-4 font-weight-bold">關於</h4>
            <p>關於我們</p>
            <p>人才招募</p>
            <p>商標使用規範</p>

        </div>

        <div class="col-md-3 col-lg-2 col-xl-2 mx-auto mt-3">
            <h4 class="text-uppercase mb-4 font-weight-bold">條款</h4>
            <p>使用條款</p>
            <p>提案者合約</p>

        </div>

        <div class="col-md-3 col-lg-2 col-xl-2 mx-auto mt-3">
            <h4 class="text-uppercase mb-4 font-weight-bold">協助</h4>
            <p>常見問題</p>
            <p>使用手冊</p>
            <p>提案百科</p>
        </div>

        <div class="col-md-3 col-lg-3 col-xl-3 mx-auto mt-3">
            <h4 class="text-uppercase mb-4 font-weight-bold">更多</h4>
            <p>品牌資源</p>
            <p>群眾觀點</p>

        </div>

    </div>

    <hr class="mb-4" style="color: #fff;">

    <div class="row align-items-center">

        <div class="justify-content-center">
            <h6 style="color: #fff;">Copyright ©2022 Backer-Founder All rights reserved.</h6>
        </div>

    </div>

</div>

</footer>`;
let querybody = document.querySelector("body");
let queryheader = document.querySelector("body");
querybody.innerHTML = header + querybody.innerHTML;
querybody.innerHTML += footer;
queryheader.innerHTML += `<link rel="stylesheet" href="../layout/layout.css" />`;

// searchbar
const icon = document.querySelector('.icon');
const search = document.querySelector('.search');
icon.onclick = function () {
    search.classList.toggle('active');
    icon.classList.toggle('active');
}

