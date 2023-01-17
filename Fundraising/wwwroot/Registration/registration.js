
// 顯示密碼
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



