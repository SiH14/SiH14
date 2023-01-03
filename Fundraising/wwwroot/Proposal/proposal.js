// 頁籤顏色切換
$("button").css({ "color": "black" })
$("li .active").css({ "background-color": "#047c51", "color": "white" });

$("button").click(function () {
    $("button").css({ "background-color": "", "color": "black" })
    if ($(this).hasClass("active")) {
        $(this).css({ "background-color": "#047c51", "color": "white" });
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
$("#titletext").keyup(function () {
    $("#textlimit").text(40 - $("#titletext").val().length);
    $("#cardtitle").val($("#titletext").val());
})


// 圖片上傳預覽
function readURL(input) {

    if (input.files && input.files[0]) {

        var imageTagID = input.getAttribute("targetID");

        var reader = new FileReader();

        reader.onload = function (e) {

            var img = document.getElementById(imageTagID);

            img.setAttribute("src", e.target.result)
        }
        reader.readAsDataURL(input.files[0]);
    }
}

//常見問題新增
var x = 0;
$(".QAaddbtn").click(function () {
    $("#QAdiv").append(`<div class="qadiv">
    <div style="position: relative; float: right; bottom:5px;">
        <input style="border: none;" class="QAdelbtn btn btn-primary" onclick="del(this)"
            type="button" value="✖">
    </div>
    <div>    
        <div>
            <textarea placeholder="請輸入此專案常見問題" class="QAtext form-control" name="questiontext${x += 1}" id="questiontext${x}" rows="3" required></textarea>            
            <div class="invalid-feedback">請輸入常見問題！</div>
        <div>
        <br>
        </div>        
            <textarea placeholder="請輸入上述問題的正確答覆" class="QAtext form-control" name="answertext${x}" id="answertext${x}" rows="5" required></textarea>
            <div class="invalid-feedback">請輸入正確答覆！</div>
        </div>
        <hr>        
    </div>
    <br>
</div>`)
})

// 方案新增
var y = 0;
$("#addplan").click(function () {
    y += 1;
    $("#plandiv").append(`  <div>
    <div style="position: relative; float: right; bottom:5px;">
    <input style="border: none;" class="QAdelbtn btn btn-primary" type="button"
        value="✖" onclick="del(this)">
</div>
    <div style="display: flex; width:100%; border:solid gray 1px; background-color: white;">
<div>
    <div>
        <img id="preview_plan_img${y}" style="width: 325px; height: 195px;"
            class="selectedimg" src="../img/coverimg.png" alt="">
    </div>
</div>

<div style="padding: 5px 10px;">
<div style="text-align: left;">
    <span style="color: #047c51; font-size: 28px;">✎ $</span>
    <input style="width: 250px; color: #047c51; font-size: 28px; border: none;"
        name="feedbackmoney${y}" id="feedbackmoney${y}" type="number"
        value="100">
</div>
<div style="padding: 5px;">
<textarea style="border: none; resize: none;" name="plantext${y}" id="plantext${y}"
    cols="48" rows="3" placeholder='請在此簡單介紹您的方案內容 ✎'class="form-control"
    required></textarea>
</div>
<div style="padding-top: 3px; text-align: left; height: 30px;">
<form action="/somewhere/to/upload" enctype="multipart/form-data">
    <a style="text-align: center;" href="javascript:;" class="file2">上傳封面圖片
        <input style="cursor: pointer;" type="file" onchange="readURL(this)"
            targetID="preview_plan_img${y}"
            accept="image/gif, image/jpeg, image/png" name="plancoverimg${y}"
            id="plancoverimg${y}">
    </a>
    <p
        style="font-size: 12px; text-align: left; position: relative; bottom: 21px; left: 110px;">
        JPEG或PNG，尺寸
        5:3； 2MB 以內。</p>
</form>
</div>
</div>
</div>
<hr>
</div>
</div>`)
})

//常見問題刪除
function del(obj) {
    $(obj).parent().parent().remove();
}


// This sample still does not showcase all CKEditor 5 features (!)
// Visit https://ckeditor.com/docs/ckeditor5/latest/features/index.html to browse all the features.
CKEDITOR.ClassicEditor.create(document.getElementById("editor"), {
    // https://ckeditor.com/docs/ckeditor5/latest/features/toolbar/toolbar.html#extended-toolbar-configuration-format
    toolbar: {
        items: [

            'selectAll', '|',
            'heading', '|', 'undo', 'redo', 'fontSize',
            'bold', 'italic', 'strikethrough', 'underline', 'removeFormat', '|',
            'bulletedList', 'numberedList', 'todoList', '|',


            'fontFamily', 'fontColor', 'fontBackgroundColor', 'highlight', '|',
            'alignment', '|',
            'link', 'insertImage', 'blockQuote', 'insertTable', 'mediaEmbed', '|',
            'specialCharacters', 'horizontalLine'

        ],
        shouldNotGroupWhenFull: true
    },
    // Changing the language of the interface requires loading the language file using the <script> tag.
    // language: 'es',
    list: {
        properties: {
            styles: true,
            startIndex: true,
            reversed: true
        }
    },
    // https://ckeditor.com/docs/ckeditor5/latest/features/headings.html#configuration
    heading: {
        options: [
            { model: 'paragraph', title: 'Paragraph', class: 'ck-heading_paragraph' },
            { model: 'heading1', view: 'h1', title: 'Heading 1', class: 'ck-heading_heading1' },
            { model: 'heading2', view: 'h2', title: 'Heading 2', class: 'ck-heading_heading2' },
            { model: 'heading3', view: 'h3', title: 'Heading 3', class: 'ck-heading_heading3' },
            { model: 'heading4', view: 'h4', title: 'Heading 4', class: 'ck-heading_heading4' },
            { model: 'heading5', view: 'h5', title: 'Heading 5', class: 'ck-heading_heading5' },
            { model: 'heading6', view: 'h6', title: 'Heading 6', class: 'ck-heading_heading6' }
        ]
    },
    // https://ckeditor.com/docs/ckeditor5/latest/features/editor-placeholder.html#using-the-editor-configuration
    placeholder: '輸入一些內容吧，幫助人們迅速了解你的專案吧！',
    // https://ckeditor.com/docs/ckeditor5/latest/features/font.html#configuring-the-font-family-feature
    fontFamily: {
        options: [
            'default',
            'Arial, Helvetica, sans-serif',
            'Courier New, Courier, monospace',
            'Georgia, serif',
            'Lucida Sans Unicode, Lucida Grande, sans-serif',
            'Tahoma, Geneva, sans-serif',
            'Times New Roman, Times, serif',
            'Trebuchet MS, Helvetica, sans-serif',
            'Verdana, Geneva, sans-serif'
        ],
        supportAllValues: true
    },
    // https://ckeditor.com/docs/ckeditor5/latest/features/font.html#configuring-the-font-size-feature
    fontSize: {
        options: [10, 12, 14, 'default', 18, 20, 22],
        supportAllValues: true
    },
    // Be careful with the setting below. It instructs CKEditor to accept ALL HTML markup.
    // https://ckeditor.com/docs/ckeditor5/latest/features/general-html-support.html#enabling-all-html-features
    htmlSupport: {
        allow: [
            {
                name: /.*/,
                attributes: true,
                classes: true,
                styles: true
            }
        ]
    },
    // Be careful with enabling previews
    // https://ckeditor.com/docs/ckeditor5/latest/features/html-embed.html#content-previews
    htmlEmbed: {
        showPreviews: true
    },
    // https://ckeditor.com/docs/ckeditor5/latest/features/link.html#custom-link-attributes-decorators
    link: {
        decorators: {
            addTargetToExternalLinks: true,
            defaultProtocol: 'https://',
            toggleDownloadable: {
                mode: 'manual',
                label: 'Downloadable',
                attributes: {
                    download: 'file'
                }
            }
        }
    },
    // https://ckeditor.com/docs/ckeditor5/latest/features/mentions.html#configuration
    mention: {
        feeds: [
            {
                marker: '@',
                feed: [
                    '@apple', '@bears', '@brownie', '@cake', '@cake', '@candy', '@canes', '@chocolate', '@cookie', '@cotton', '@cream',
                    '@cupcake', '@danish', '@donut', '@dragée', '@fruitcake', '@gingerbread', '@gummi', '@ice', '@jelly-o',
                    '@liquorice', '@macaroon', '@marzipan', '@oat', '@pie', '@plum', '@pudding', '@sesame', '@snaps', '@soufflé',
                    '@sugar', '@sweet', '@topping', '@wafer'
                ],
                minimumCharacters: 1
            }
        ]
    },
    // The "super-build" contains more premium features that require additional configuration, disable them below.
    // Do not turn them on unless you read the documentation and know how to configure them and setup the editor.
    removePlugins: [
        // These two are commercial, but you can try them out without registering to a trial.
        // 'ExportPdf',
        // 'ExportWord',
        'CKBox',
        'CKFinder',
        'EasyImage',
        // This sample uses the Base64UploadAdapter to handle image uploads as it requires no configuration.
        // https://ckeditor.com/docs/ckeditor5/latest/features/images/image-upload/base64-upload-adapter.html
        // Storing images as Base64 is usually a very bad idea.
        // Replace it on production website with other solutions:
        // https://ckeditor.com/docs/ckeditor5/latest/features/images/image-upload/image-upload.html
        // 'Base64UploadAdapter',
        'RealTimeCollaborativeComments',
        'RealTimeCollaborativeTrackChanges',
        'RealTimeCollaborativeRevisionHistory',
        'PresenceList',
        'Comments',
        'TrackChanges',
        'TrackChangesData',
        'RevisionHistory',
        'Pagination',
        'WProofreader',
        // Careful, with the Mathtype plugin CKEditor will not load when loading this sample
        // from a local file system (file://) - load this site via HTTP server if you enable MathType
        'MathType'
    ]
});

//表單驗證
(function () {
    'use strict'

    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.querySelectorAll('.needs-validation')

    // Loop over them and prevent submission
    Array.prototype.slice.call(forms)
        .forEach(function (form) {
            form.addEventListener('submit', function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault()
                    event.stopPropagation()
                }

                form.classList.add('was-validated')
            }, false)
        })
})()

