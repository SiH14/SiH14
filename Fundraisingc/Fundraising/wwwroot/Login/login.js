const forms = document.querySelector(".forms"),
    pwShowHide = document.querySelectorAll(".eye-icon"),
    links = document.querySelectorAll(".link");

pwShowHide.forEach(
    eyeIcon => {
        eyeIcon.addEventListener("click", () => {
            let pwFields = eyeIcon.parentElement.parentElement.querySelectorAll(".password");

            pwFields.forEach(password => {
                if (password.type === "password") {
                    password.type = "text";
                    eyeIcon.classList.replace("uil-eye-slash", "uil-eye")
                    return;
                }
                password.type = "password";
                eyeIcon.classList.replace("uil-eye", "uil-eye-slash")
            })
        })
    }
)


// post

//var account;
//var password;
//function OK() {
//    account = document.getElementById("UserEmail").value;
//    password = document.getElementById("UserPassword").value;
//    console.log(account, password)

//    axios.post("https://localhost:44398/api/Login", {
//        UserEmail: account,
//        UserPassword: password
//    })
//        .then(res => {

//            console.log(res);
//        })
//        .catch(error => {
//            console.log(error.response);
//        });
//    window.location = "https://www.youtube.com/";
//}