let header =
  `<nav class="headernav navbar navbar-expand-lg navbar-light bg-white border-bottom" id="header">

<div class="container-fluid headernav" id="header-container">
    <!--LOGO -->
    <a class="navbar-brand fs-2 headernav" id="header-brand" href="../ProductPage/mymainpage.html">
    <img id="brand" src="../img/brand.png" alt="">
    </a>

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
                <a class="nav-link headernav" href="../productpage/filter.html">探索</a>
            </li>

        </ul>

    </div>

    <div class="headernav">
        <ul id="headicon" class="navbar-nav me-5 headernav">
            <button type="button" class="btn voicesearch" data-bs-toggle="modal" data-bs-target="#myModal">
            <img id="mic" src="../img/microphone.png" alt="">
            </button>

            <!-- 語音搜尋窗口 -->
            <div class="modal" id="myModal">
                <div class="modal-dialog">
                    <div class="modal-content">

                        <!-- Modal Header -->
                        <div class="modal-header">
                            <h4 class="modal-title">語音偵測</h4>
                            <button type="button" class="theX btn-close" data-bs-dismiss="modal"></button>
                        </div>

                        <!-- Modal body -->
                        <div class="modal-body">
                            <span class = "voicesearchtext">請開始說話...</span>
                        </div>
                    </div>
                </div>
            </div>
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
            <img id="iconimg" class="headernav loginicon" src="" alt="">
        </a>

        <ul class="dropdown-menu" aria-labelledby="dropdownMenuLink">
            <li style="border-bottom: 1px rgb(190, 186, 186) solid;"><a class="dropdown-item" href="../Registration/registration.html">註冊</a></li>
            <li><a class="dropdown-item" href="../Login/login.html">登入</a></li>
        </ul>
    </div>             
            </ul>
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

let footer = `<footer class="pt-5 " style="margin-top:50px">

<div class=" text-center text-md-left">

    <div class="row text-center text-md-left container m-auto justify-content-center" id="">

        <div class="col-md-2 mx-auto footer-item">
            <h4 class=" mb-4 font-weight-bold">關於</h4>
            <p>關於我們</p>
            <p>人才招募</p>
            <p>商標使用規範</p>

        </div>

        <div class="col-md-2 mx-auto  footer-item">
            <h4 class=" mb-4 font-weight-bold">條款</h4>
            <p>使用條款</p>
            <p>提案者合約</p>

        </div>

        <div class="col-md-2 mx-auto footer-item"> 
            <h4 class=" mb-4 font-weight-bold">協助</h4>
            <p>常見問題</p>
            <p>使用手冊</p>
            <p>提案百科</p>
        </div>

        <div class="col-md-2 mx-auto footer-item">
            <h4 class=" mb-4 font-weight-bold">更多</h4>
            <p>品牌資源</p>
            <p>群眾觀點</p>

        </div>      
<div class="col-md-3 mx-auto footer-item">
    <div>
    <a href="../ProductPage/mymainpage.html">
    <img style="margin-top:-18px" id="brand" src="../img/brand.png" alt="">
    </a>
    </div>
<div>
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
   </div>
    </div>


        <div class="p-2" style="border-top: 1px #afaeae solid;">
Copyright ©2023 NeedU募資平台.
        </div>



</div>
<div style="display:none;">< script src = "microsoft.cognitiveservices.speech.sdk.bundle.js" ></script ></div>
</footer>`;
let querybody = document.querySelector("body");
let queryheader = document.querySelector("head");
querybody.innerHTML = header + querybody.innerHTML;
querybody.innerHTML += footer;
queryheader.innerHTML += `<link rel="stylesheet" href="../layout/layout.css"/>`;

let mic = 0;
let voicesearch = document.querySelector(".voicesearch");
let theX = document.querySelector(".theX");
voicesearch.onclick = function () {
    //if (document.querySelector(".voicesearchtext").innerText == "請再說一次.") {
    //    alert("請開麥克風")
    //}
    document.querySelector(".voicesearchtext").innerHTML = "請開始說話..."
    mic = 0
    setTimeout(wangaa, 4500);
}

theX.onclick = function () {
    mic = 1;
    window.location.reload();
}

function wangaa() {
    if (mic == 1) {
    }
    else {
        if (document.querySelector(".voicesearchtext").innerHTML == "檢查麥克風 再說一次" || document.querySelector(".voicesearchtext").innerHTML == "請開始說話...") {
            document.querySelector(".voicesearchtext").innerHTML = "檢查麥克風 再說一次"
            mic = 1;
        }
        else {
            mic = 0;
            sessionStorage.setItem("filterans", document.querySelector(".voicesearchtext").innerHTML);
            window.location = "/productpage/filterans.html";
        }
    }
}

/*語音*/
// status fields and start button in UI
var phraseDiv;
var startRecognizeOnceAsyncButton;

// subscription key and region for speech services.
var subscriptionKey, serviceRegion;
var authorizationToken;
var SpeechSDK;
var recognizer;
document.addEventListener("DOMContentLoaded", function () {
    startRecognizeOnceAsyncButton = document.querySelector('.voicesearch');
    subscriptionKey = document.getElementById("subscriptionKey");
    serviceRegion = document.getElementById("serviceRegion");
    phraseDiv = document.getElementById("phraseDiv");


    // startRecognizeOnceAsyncButton
    startRecognizeOnceAsyncButton.addEventListener("click", function () {
        startRecognizeOnceAsyncButton.disabled = true;
        /*   phraseDiv.innerHTML = "";*/

        // if we got an authorization token, use the token. Otherwise use the provided subscription key
        var speechConfig;
        speechConfig = SpeechSDK.SpeechConfig.fromSubscription("f433aba72ed643f19bd014887e3ad78d", "southeastasia");
        //if (authorizationToken) {
        //    speechConfig = SpeechSDK.SpeechConfig.fromAuthorizationToken(authorizationToken, serviceRegion.value);
        //} else {
        //    if (subscriptionKey.value === "" || subscriptionKey.value === "subscription") {
        //        alert("Please enter your Microsoft Cognitive Services Speech subscription key!");
        //        return;
        //    }
        //    speechConfig = SpeechSDK.SpeechConfig.fromSubscription("f433aba72ed643f19bd014887e3ad78d", "southeastasia");
        //}

        // speechConfig.speechRecognitionLanguage = "en-US";
        speechConfig.speechRecognitionLanguage = "zh-TW";
        var audioConfig = SpeechSDK.AudioConfig.fromDefaultMicrophoneInput();
        recognizer = new SpeechSDK.SpeechRecognizer(speechConfig, audioConfig);

        recognizer.recognizeOnceAsync(
            function (result) {
                startRecognizeOnceAsyncButton.disabled = false;
                /*          phraseDiv.innerHTML += result.text;*/
                window.console.log(result);
                const textresult = result.text
                /*    document.getElementById("mysearch").value = result.text.substring(0, textresult.length - 1);*/
                if (mic == 0) {
                    document.querySelector(".voicesearchtext").innerHTML = result.text.substring(0, textresult.length - 1);
                }
                recognizer.close();
                recognizer = undefined;
            },
            function (err) {
                startRecognizeOnceAsyncButton.disabled = false;
                /*    phraseDiv.innerHTML += err;*/
                window.console.log(err);
                /*   document.getElementById("mysearch").value = "請再說一次.";*/
                document.querySelector(".voicesearchtext").innerHTML = "檢查麥克風 再說一次";
                recognizer.close();
                recognizer = undefined;
            });
    });

    if (!!window.SpeechSDK) {
        SpeechSDK = window.SpeechSDK;
        startRecognizeOnceAsyncButton.disabled = false;

        document.getElementById('content').style.display = 'block';
        /* document.getElementById('warning').style.display = 'none';*/

        // in case we have a function for getting an authorization token, call it.
        if (typeof RequestAuthorizationToken === "function") {
            RequestAuthorizationToken();
        }
    }
});
/*語音*/

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
  axios
    .get("/api/Login/getusername")
    .then((res) => {
      if (res.data == "未登入") {
        window.location = "../Login/login.html";
      } else {
        getid = function (callback) {
          axios
            .get("/api/Login/getuserid")
            .then((res) => {
              callback(res.data);
              // if (res.data != "") {
              //     alert("login OK")
              // }
              axios.get("/api/Login/getuserphoto/" + res.data).then((res) => {
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
                                    <li style="border-bottom: 1px rgb(190, 186, 186) solid;"><a class="dropdown-item"
                                    href="../MemberCentre/UserSetting.html">帳戶設定</a></li>
                            <li style="text-align: center;"><a class="dropdown-item" href="../ProductPage/mymainpage.html" onclick="logout()">登出</a></li>`;
              });
            })
            .catch((error) => {
              console.log(error.response);
              var nologinsetimg = document.getElementById("iconimg");
              nologinsetimg.setAttribute("src", "../img/loginicon.png");
            });
        };
        getid(function (myuser) {
          // console.log(myuser);
          userid = myuser;
        });
      }
    })
    .catch((error) => {
      console.log(error.response);
    });
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
// 背景圖片
document.querySelector("body").style.backgroundImage = "url('../img/mbbg.png')";
document.querySelector("body").style.backgroundRepeat = "no-repeat";
document.querySelector("body").style.backgroundAttachment = "fixed";
document.querySelector("body").style.backgroundSize = "100%";
