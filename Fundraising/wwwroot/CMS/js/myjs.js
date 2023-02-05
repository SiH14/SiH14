
//<div>
//<form class="d-none d-sm-inline-block form-inline  mt-3 mw-100 navbar-search ">
//<div class="input-group">
//<select name="" id="" class="">
         //   <option value="">提案編號</option>
         //   <option value="">贊助編號</option>
           // <option value="">會員編號</option>
       // </select>
       // <input type="text" class="" style="width: 60% ;" placeholder="Search for..."
        //    aria-label="Search" aria-describedby="basic-addon2">
       // <div class="input-group-append">
         //   <button class="btn btn-primary" type="button">
         //       <i class="fas fa-search fa-sm"></i>
         //   </button>
       // </div>
   // </div>
//</form>
//</div>

// 導覽列
document.getElementById("myheader").innerHTML = `
<nav class="navbar navbar-expand navbar-light  static-top " style="margin: auto;
max-width: 80%;">
<div class="px-5 mr-auto d-flex">
<a href="./index.html" style="text-decoration: none;display:block;"><img src="./img/brand.png" style="width:200px"></a>
<span class="mt-auto color-theme">後臺管理系統</span >
</div>
<!-- 導覽列右邊 -->
<!-- 導覽收尋欄-->
<div class="row">
<ul class="navbar-nav mx-5">
<div>
    <li class="nav-item dropdown no-arrow d-sm-none">
        <a class="nav-link dropdown-toggle" href="#" id="searchDropdown" role="button"
            data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
            <i class="fas fa-search fa-fw"></i>
        </a>
        <div class="dropdown-menu dropdown-menu-right p-3 shadow animated--grow-in"
            aria-labelledby="searchDropdown">
            <form class="form-inline mr-auto w-100 navbar-search">
                <div class="input-group">
                    <input type="text" class="form-control bg-light border-0 small"
                        placeholder="Search for..." aria-label="Search" aria-describedby="basic-addon2">
                    <div class="input-group-append">
                        <button class="btn btn-primary" type="button">
                            <i class="fas fa-search fa-sm"></i>
                        </button>
                    </div>
                </div>
            </form>
        </div>
    </li>
    <div class="topbar-divider d-none d-sm-block"></div>

    <!-- 使用者資訊 -->
    <div class="nav-item dropdown no-arrow">
        <a class="nav-link dropdown-toggle" href="#" id="userDropdown" role="button"
            data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
            <span class="mr-2 d-none d-lg-inline ">{{employeeinfo.name}}</span>
            <img class="img-profile rounded-circle imgset" :src="employeeinfo.employeephoto" style="border: 1px solid white;">
        </a>
        <!-- 下拉清單 -->
        <div class="dropdown-menu  shadow animated--grow-in"
            aria-labelledby="userDropdown">
            <a class="dropdown-item" href="./profile.html">
                <i class="fas fa-user fa-sm fa-fw mr-2 text-gray-400"></i>
                個人資料
            </a>
            <div class="dropdown-divider"></div>
            <a href="" class="dropdown-item" v-on:click="logout()">
                <i class="fas fa-sign-out-alt fa-sm fa-fw mr-2 text-gray-400"></i>
                登出
            </a>
        </div>
    </div>

</ul>
</div>
</div>
</div>
</div>
</nav>`;
// <a class="nav-link" href="./Preview.html">
// <i class="fa fa-tags" aria-hidden="true"></i>
//     <span>提案審核</span></a>
// <a class="nav-link" href="./Orefund.html">
// <i class="fa fa-inbox" aria-hidden="true"></i>
//     <span>贊助退款</span></a>
// 側邊
document.getElementById("mysider").innerHTML = `<div class="sidebar-heading">
控制面板
</div>

<li class="nav-item ">
<a class="nav-link" href="index.html">
    <i class="fas fa-fw fa-tachometer-alt"></i>
    <span>後台總覽</span></a>
</li>


<hr class="sidebar-divider">
<div class="sidebar-heading">
提案相關
</div>
<li class="nav-item ">
<a class="nav-link" href="./Plist.html">
<i class="fa fa-list" aria-hidden="true"></i>
    <span>提案管理</span></a>

</li>
<hr class="sidebar-divider">
<div class="sidebar-heading">
贊助中心
</div>
<li class="nav-item ">
<a class="nav-link" href="./Olist.html">
<i class="fa fa-list-alt" aria-hidden="true"></i>
    <span>贊助管理</span></a>
</li>

<hr class="sidebar-divider">
<div class="sidebar-heading">
會員相關
</div>
<li class="nav-item ">
<a class="nav-link" href="./message.html">
<i class="fa fa-comments" aria-hidden="true"></i>
    <span>留言管理</span></a>
</li>
<li class="nav-item">
<a class="nav-link collapsed w-100" href="./member.html" >
    <i class="fas fa-fw fa-folder"></i>
    <span>會員列表</span>
</a>
</li>
<hr class="sidebar-divider">
<div class="sidebar-heading">
後台管理
</div>
<li class="nav-item ">
<a class="nav-link" href="./Employee.html">
<i class="fa fa-solid fa-file"></i>
    <span>員工管理</span></a>
    </li>
    <li class="nav-item ">
<a class="nav-link" href="./profile.html">
<i class="fa fa-user" aria-hidden="true"></i>
    <span>個人資料</span></a>
    </li>
    `;

// 頁尾
document.getElementById("myfooter").innerHTML = ` <footer class="p-4">
<div class="container my-auto">
    <div class="copyright text-center my-auto">
        <span class="color-theme fs-b">Copyright &copy;needU募資平台 2023</span>
    </div>
</div>
</footer>`;


