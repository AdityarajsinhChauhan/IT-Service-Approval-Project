function validateForm() {

    var password = document.getElementById("password").value;
    var confirmPassword = document.getElementById("re-pwd").value;
    var passwordError = document.getElementById("password-error");

    if (password !== confirmPassword) {
        passwordError.style.visibility = "visible"; 
        document.getElementById("password").focus();
        return false;
    } else {
        passwordError.style.visibility = "hidden"; 
    }

    return true;
}


