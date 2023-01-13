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
            <li style="border-bottom: 1px rgb(190, 186, 186) solid;"><a class="dropdown-item" href="#">註冊</a></li>
            <li><a class="dropdown-item" href="#">登入</a></li>
        </ul>
    </div>             
            </ul>
          </div>
            
        </ul>
    </div>

</div>
</nav>`

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

    <div style="width:99%" class="row align-items-center">

        <div class="justify-content-center">
            <p style="color: black; font-size: 0.975rem;">Copyright ©2022 Backer-Founder All rights reserved.</p>
        </div>

    </div>

</div>

</footer>`;
let querybody = document.querySelector("body");
let queryheader = document.querySelector("head");
var bootstrapjs = `<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js"></script>`;
querybody.innerHTML = header + querybody.innerHTML + footer;
queryheader.innerHTML += `<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js"></script>`;
let queryheader2 = document.querySelector("head");
queryheader2.innerHTML += `<link rel="stylesheet" href="../layout/layout.css"/>`


// searchbar
const icon = document.querySelector('.icon');
const search = document.querySelector('.search');
const clear = document.querySelector('.clear');
// const dropdowntoggle = document.querySelector('#dropdowntoggle');
// const dropdownmenu = document.querySelector('#dropdownmenu');
icon.onclick = function () {
    search.classList.toggle('active');
    icon.classList.toggle('active');
}

clear.onclick = function () {
    document.getElementById('mysearch').value = ''
}


let mysearchkeydown = document.getElementById("mysearch");

mysearchkeydown.onkeydown = function (e) {
    if (e.keyCode == 13) {
        //触发键盘事件enter
        window.location = "https://www.youtube.com/?themeRefresh=1";
    }
}

window.onclick = function (event) {
    if (!event.target.matches('.headernav')) {
        document.getElementById('mysearch').value = '';
        search.classList.remove('active');
    }
}

// get登入的userid，設置頭像
var userid = "";
window.onload = function getuserID() {
    axios.get("http://localhost:51701/api/Login/getuserid")
        .then(res => {
            console.log(res.data);
            // if (res.data != "") {
            //     alert("login OK")
            // }
            axios.get("http://localhost:51701/api/Login/getuserphoto/"+res.data)
            .then(res =>{
                console.log(res.data[0].userPhoto)
                var setimg= document.getElementById("iconimg");
                setimg.setAttribute("src", res.data[0].userPhoto)
                setimg.style.width="30px";
                setimg.style.height="30px";
                setimg.style.borderRadius="15px";
            })
        })
        .catch(error => {
            console.log(error.response);
        })
}

