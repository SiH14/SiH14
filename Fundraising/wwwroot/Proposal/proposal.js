// 頁籤顏色切換
$(".needs-validation button").css({ "color": "black", "border-bottom": "none" })
$("li .active").css({ "background-color": "#047c51", "color": "white", "border-bottom": "1px solid #047c51" });

$(".needs-validation button").click(function () {
    $(".needs-validation button").css({ "background-color": "", "color": "black", "border-bottom": "none" })
    if ($(this).hasClass("active")) {
        $(this).css({ "background-color": "#047c51", "color": "white", "border-bottom": "1px solid #047c51" });
    }
})

//上一頁(1)
$("#prepage1").click(function () {
    $("#outline-tab").click();
    $("#outline-tab").css({ "background-color": "#047c51", "color": "white" });
})

//上一頁(2)
$("#prepage2").click(function () {
    $("#content-tab").click();
    $("#content-tab").css({ "background-color": "#047c51", "color": "white" });
})

//上一頁(3)
$("#prepage3").click(function () {
    $("#setting-tab").click();
    $("#setting-tab").css({ "background-color": "#047c51", "color": "white" });
})

//下一頁(1)
$("#nextpage1").click(function () {
    $("#content-tab").click();
    $("#content-tab").css({ "background-color": "#047c51", "color": "white" });
})

//下一頁(2)
$("#nextpage2").click(function () {
    $("#setting-tab").click();
    $("#setting-tab").css({ "background-color": "#047c51", "color": "white" });
})

//下一頁(3)
$("#nextpage3").click(function () {
    $("#infor-tab").click();
    $("#infor-tab").css({ "background-color": "#047c51", "color": "white" });
})

// 標題內容字數倒數
$("#ProductTitle").keyup(function () {
    $("#textlimit").text(40 - $("#ProductTitle").val().length);
    $("#cardtitle").val($("#ProductTitle").val());
})

// 提案封面圖片上傳預覽
var coverimgdata;
function readURL(input) {
    // console.log(input.files[0]);
    if (input.files && input.files[0]) {

        var imageTagID = input.getAttribute("targetID");

        var reader = new FileReader();

        reader.onload = function (e) {

            coverimgdata = reader.result;

            var img = document.getElementById(imageTagID);

            img.setAttribute("src", e.target.result)
        }
        reader.readAsDataURL(input.files[0]);
        // reader.readAsArrayBuffer(input.files[0]);
        // reader.readAsBinaryString(input.files[0]);
    }
}

// function test() {
//     console.log(userid);
// }

// 方案封面圖片上傳
const planinputarray = [];
function readPlanURL(input) {

    if (input.files && input.files[0]) {

        var imageTagID = input.getAttribute("targetID");

        var reader = new FileReader();

        reader.onload = function (e) {
            planinputarray.push(reader.result);
            // plancoverimgdata = reader.result;
            var img = document.getElementById(imageTagID);

            img.setAttribute("src", e.target.result)
        }
        reader.readAsDataURL(input.files[0]);
    }
}



// 取得username
$(document).ready(function () {

    axios.get("/api/Login/getusername")
        .then(res => {
            // console.log(res.data);
            if (res.data == "未登入") {
                window.location = "../Login/login.html"
            } else {
                axios.get("/api/Login/getuserid/")
                    .then(res => {
                        // console.log(res.data);
                        axios.get("/api/Login/getuserphoto/" + res.data)
                            .then(res => {
                                // console.log(res.data[0]);
                                // console.log(document.querySelector(".designby").innerHTML);
                                document.querySelector("#PrincipalName").value = res.data[0].userName;
                                document.querySelector("#PrincipalEmail").value = res.data[0].userEmail;
                                document.querySelector("#PrincipalPhone").value = res.data[0].userPhone;

                            })
                    })
                    .catch(error => {
                        console.log(error.response);
                    })
            }
        })
        .catch(error => {
            console.log(error.response);
        })


})

// 送出提案function
var dataDB;
var egg;
var good;
function confirmsubmit() {
    dataDB = CKEDITOR.instances.ProductContent.getData();

    // 提送表單大部分內容
    productsubmit = function (callback) {
        // console.log(dataDB);
        // console.log(coverimgdata);
        var bankCode = document.querySelector("#bankcode").value
        // console.log(bankCode.substr(0, 5))
        var proposaldata = {
            coverphoto: coverimgdata,
            ProductTitle: document.getElementById("ProductTitle").value,
            ProductContent: dataDB,
            TargetAmount: document.getElementById("TargetAmount").value,
            ProductVedio: document.getElementById("ProductVedio").value,
            PrincipalId: document.getElementById("PrincipalId").value,
            PrincipalName: document.getElementById("PrincipalName").value,
            PrincipalPhone: document.getElementById("PrincipalPhone").value,
            PrincipalEmail: document.getElementById("PrincipalEmail").value,
            PrincipalBankAccount: bankCode.substr(0, 5) + document.getElementById("PrincipalBankAccount").value,
            UserID: userid,
            ProductStateID: 1
        }
        axios.post("/api/Products", proposaldata)
            .then(res => {
                callback(res.data.productId);
                // console.log(res.data.productId);
            })
            .catch(error => {
                console.log(error.response);
            });
    }
    productsubmit(function (xxx) {
        // console.log(xxx)

        // 取得ProductID，送常見問題表單
        egg = document.querySelectorAll('div[class=QAQA] textarea')
        for (let index = 0, j = 1; index < egg.length, j < egg.length; index += 2, j += 2) {
            // console.log(egg[index].value)
            // console.log(egg[j].value)
            axios.post("/api/Questions", {
                ProductID: xxx,
                QuestionTitle: egg[index].value,
                QuestionContent: egg[j].value
            })
                // .then(res => {
                //     console.log(res);
                // })
                .catch(error => {
                    console.log(error.response);
                });
        }

        good = document.querySelectorAll('div[class=planinputdiv] .inputplan')
        for (let w = 0, x = 1, y = 2, z = 0; w < good.length, x < good.length, y < good.length, z < good.length; w += 3, x += 3, y += 3, z++) {
            // console.log(good[w].value)
            // console.log(good[x].value)
            // console.log(good[y].value)
            // console.log(planinputarray[z])

            axios.post("/api/Plans", {
                ProductID: xxx,
                PlanTitle: good[x].value,
                PlanContent: good[y].value,
                PlanPrice: good[w].value,
                PlanPhoto: planinputarray[z]
            })
                // .then(res => {
                //     console.log(res);
                // })
                .catch(error => {
                    console.log(error.response);
                });
        }
    });
}

//常見問題新增
var x = 0;
function add() {
    $("#QAdiv").append(`<div class="qadiv">
    <div style="position: relative; float: right; bottom:5px;">
        <input style="border: none;" class="QAdelbtn btn btn-primary" onclick="del(this)"
            type="button" value="✖">
    </div>
    <div class="QAQA">    
        <div class="question">
            <textarea style="border-radius: 5px;border: black 1px solid" placeholder="✎ 請輸入此專案常見問題" class="QAtext form-control " name="questiontext${x += 1}" id="questiontext${x}" rows="3" required></textarea>            
            <div class="invalid-feedback">請輸入常見問題！</div>
        </div>
        <br>
        <div class="answer">        
            <textarea style="border-radius: 5px;border: black 1px solid" placeholder="✎ 請輸入上述問題的正確答覆" class="QAtext form-control" name="answertext${x}" id="answertext${x}" rows="5" required></textarea>
            <div class="invalid-feedback">請輸入正確答覆！</div>
        </div>
        <hr>        
    </div>
    <br>
</div>`)
}

// 方案新增
var y = 0;
$("#addplan").click(function () {
    y += 1;
    $("#plandiv").append(`<hr><div>
    <div style="position: relative; float: right; bottom:5px;">
    <input style="border: none;" class="QAdelbtn btn btn-primary" type="button"
        value="✖" onclick="del(this)">
</div>
<div class="planinputdiv"
style="display: flex; width:100%; border:solid gray 1px; background-color: white;">
<div>
    <div>
        <img id="preview_plan_img${y}"
            style=" width: 270px; height: 90px; padding-left: 5px; padding-top: 5px;"
            class="selectedimg" src="../img/coverimg.png" alt="">
        <div
            style="position: relative; display: flex; text-align: left; left: 5px; padding: 5px; width: 100%;">
            <div style="height: 30px;">
                <span style="color: #047c51; font-size: 20px;">$</span>
                <span>
                    <input
                        style="width:120px; color: #047c51; font-size: 20px; border: none;"
                        name="PlanPrice${y}" id="PlanPrice${y}" type="number"
                        placeholder="✎ 方案金額" class="inputplan">
                </span>
            </div>
            <div
                style="text-align: left; height: 30px; position: relative; left: 31px;">
                <a style="text-align: center; font-size: 14px;"
                    href="javascript:;" class="file2">上傳圖片
                    <input style="cursor: pointer;" type="file"
                        onchange="readPlanURL(this)" targetID="preview_plan_img${y}"
                        accept="image/gif, image/jpeg, image/png"
                        name="PlanPhoto${y}" id="PlanPhoto${y}">
                </a>
            </div>
        </div>
        <div style="text-align: left; padding-left: 5px; padding: 5px;">
            <textarea style="text-align: left; border: none; resize: none;"
                cols="30" rows="2" name="PlanTitle${y}" id="PlanTitle${y}"
                class="form-control inputplan" required
                placeholder="✎ 請提入方案標題"></textarea>
        </div>

    </div>
</div>

<div style="padding: 5px 10px;">

    <div style="padding: 5px;">
        <textarea style="border: none; resize: none;" name="plantext${y}"
            id="plantext${y}" cols="48" rows="7" placeholder='✎ 請在此簡單介紹您的方案內容'
            class="form-control inputplan" required></textarea>
    </div>

</div>
</div>
<br>`)
})

//常見問題刪除
function del(obj) {
    $(obj).parent().parent().remove();
}

// CKEditor 4
CKEDITOR.replace('ProductContent', {
    height: 500,
    removeButtons: 'PasteFromWord',
    toolbarGroups: [
        { name: 'clipboard', groups: ['clipboard', 'undo'] },
        { name: 'editing', groups: ['find', 'selection', 'spellchecker'] },
        { name: 'links' },
        { name: 'insert' },
        { name: 'forms' },
        { name: 'tools' },
        { name: 'document', groups: ['mode', 'document', 'doctools'] },
        { name: 'others' },
        // '/',
        { name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
        { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi'] },
        { name: 'styles' },
        { name: 'colors' },
        { name: 'about' }
    ]
});
var dataDB;


//表單驗證
(function () {
    'use strict'

    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.querySelectorAll('.needs-validation')

    // Loop over them and prevent submission
    Array.prototype.slice.call(forms)
        .forEach(function (form) {
            form.querySelector("#proposalsubmit").addEventListener('click', function (event) {
                if (!form.checkValidity()) {
                    swal("提交失敗", "尚有必填欄位未輸入", "error", { button: "確定", dangerMode: true });
                    event.preventDefault()
                    event.stopPropagation()
                } else if (form.checkValidity()) {
                    swal("提交表單", "資料確認無誤?", "warning", {
                        buttons: {
                            確定: true,
                            取消: true
                        },
                    })
                        .then((value) => {
                            switch (value) {
                                case "確定":
                                    confirmsubmit();
                                    swal("表單已送出!", "", "success", {
                                        buttons: {
                                            確定: true
                                        },
                                    })
                                        .then((value) => {
                                            switch (value) {
                                                case "確定":
                                                    window.location = "../productpage/mymainpage.html"
                                                    break;
                                            }
                                        })
                                    break;
                                case "取消":
                                    break;
                            }
                        });
                }

                form.classList.add('was-validated')
            }, false)
        })
})()