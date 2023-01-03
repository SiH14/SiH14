
// 導覽列
document.getElementById("myheader").innerHTML = `
<nav class="navbar navbar-expand navbar-light bg-white topbar mb-4 static-top  container">
<div class="px-5">
<h1><a href="./index.html">募資平台</a></h1>
</div>
<!-- 導覽列右邊 -->
<ul class="navbar-nav ml-auto">

<!-- 導覽收尋欄-->
<form class="d-none d-sm-inline-block form-inline  mt-3 mw-100 navbar-search ">
<div class="input-group">
        <input type="text" class="form-control bg-light border-0 small" placeholder="Search for..."
            aria-label="Search" aria-describedby="basic-addon2">
        <div class="input-group-append">
            <button class="btn btn-primary" type="button">
                <i class="fas fa-search fa-sm"></i>
            </button>
        </div>
    </div>
</form>
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
    <li class="nav-item dropdown no-arrow">
        <a class="nav-link dropdown-toggle" href="#" id="userDropdown" role="button"
            data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
            <span class="mr-2 d-none d-lg-inline text-gray-600 small">管理者名稱</span>
            <img class="img-profile rounded-circle" src="img/undraw_profile.svg">
        </a>
        <!-- 下拉清單 -->
        <div class="dropdown-menu dropdown-menu-right shadow animated--grow-in"
            aria-labelledby="userDropdown">
            <a class="dropdown-item" href="#">
                <i class="fas fa-user fa-sm fa-fw mr-2 text-gray-400"></i>
                Profile
            </a>
            <div class="dropdown-divider"></div>
            <a class="dropdown-item" href="#" data-toggle="modal" data-target="#logoutModal">
                <i class="fas fa-sign-out-alt fa-sm fa-fw mr-2 text-gray-400"></i>
                Logout
            </a>
        </div>
    </li>

</ul>
</div>
</div>
</nav>`;

// 側邊
document.getElementById("mysider").innerHTML = `<div class="sidebar-heading">
控制面板
</div>

<li class="nav-item ">
<a class="nav-link active" href="index.html">
    <i class="fas fa-fw fa-tachometer-alt"></i>
    <span>後台總攬</span></a>
</li>


<hr class="sidebar-divider">
<div class="sidebar-heading">
產品相關
</div>
<li class="nav-item ">
<a class="nav-link" href="./Plist.html">
<i class="fa fa-list" aria-hidden="true"></i>
    <span>提案列表</span></a>
<a class="nav-link" href="./Preview.html">
<i class="fa fa-tags" aria-hidden="true"></i>
    <span>提案審核</span><span class="btn btn-danger p-0 ml-1" >3</span></a>
</li>
<hr class="sidebar-divider">
<div class="sidebar-heading">
訂單中心
</div>
<li class="nav-item ">
<a class="nav-link" href="./Olist.html">
<i class="fa fa-list-alt" aria-hidden="true"></i>
    <span>訂單列表</span></a>
<a class="nav-link" href="./Orefund.html">
<i class="fa fa-inbox" aria-hidden="true"></i>
    <span>退訂管理</span><span class="btn btn-danger p-0 ml-1" >3</span></a>
</li>


<hr class="sidebar-divider">
<div class="sidebar-heading">
會員相關
</div>
<li class="nav-item">
<a class="nav-link collapsed w-100" href="#" data-toggle="collapse"
    data-target="#collapsePages" aria-expanded="true" aria-controls="collapsePages">
    <i class="fas fa-fw fa-folder"></i>
    <span>會員管理</span>
</a>
<div id="collapsePages" class="collapse" aria-labelledby="headingPages"
    data-parent="#accordionSidebar">
    <div class="bg-white py-2 collapse-inner rounded">
        <a class="collapse-item" href="./menber.html">會員列表</a>
        <a class="collapse-item" href="./bandlist.html">黑名單</a>
    </div>
</div>
</li>
<li class="nav-item ">
<a class="nav-link" href="./message.html">
<i class="fa fa-comments" aria-hidden="true"></i>
    <span>留言管理</span></a>
</li>
<hr class="sidebar-divider">
<div class="sidebar-heading">
後台管理
</div>
<li class="nav-item ">
<a class="nav-link" href="./profile.html">
<i class="fa fa-user" aria-hidden="true"></i>
    <span>個人資料</span></a>

</li>`;

// 頁尾
document.getElementById("myfooter").innerHTML = ` <footer class="sticky-footer bg-white item-style">
<div class="container my-auto">
    <div class="copyright text-center my-auto">
        <span>Copyright &copy;咱的募資平台 2023</span>
    </div>
</div>
</footer>`;
