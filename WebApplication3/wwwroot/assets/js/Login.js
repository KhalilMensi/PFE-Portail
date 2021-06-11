function SignIn() {
    if ($('#email').val().length > 7 && emailIsValid($('#email').val()) == true && $('#password').val().length > 0) {

        $('#validEmail').text('')
        $('#validPassword').text('')

        var form = document.getElementById('form')
        $.ajax({
            url: '/Account/Login',
            type: 'post',
            data: new FormData(form),
            cache: false,
            contentType: false,
            processData: false,
            success: function (response) {
                console.log(response)
                if (!response.success) {
                    if (response.message == "User Not Found !") {
                        $('#email').val("")
                        $('#validEmail').text(InvalidEmail)
                        $('#password').val("")
                        $('#validPassword').text(InvalidPassword)
                    } else if (response.message == "Password Incorrect") {
                        $('#password').val("")
                        $('#validPassword').text(InvalidPassword)
                    }
                } else {
                    $.ajax({
                        url: '/Account/LoginUser',
                        type: 'post',
                        data: new FormData(form),
                        cache: false,
                        contentType: false,
                        processData: false,
                        success: function (response) {
                            console.log(response)
                            if (response.success) {
                                const Toast = Swal.mixin({
                                    toast: true,
                                    position: 'top-end',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                    didOpen: (toast) => {
                                        toast.addEventListener('mouseenter', Swal.stopTimer)
                                        toast.addEventListener('mouseleave', Swal.resumeTimer)
                                    }
                                })
                                if (cultureInfo == "en-US") {
                                    Toast.fire({
                                        icon: 'success',
                                        title: 'Logged in successfully'
                                    })
                                } else if (cultureInfo == "fr") {
                                    Toast.fire({
                                        icon: 'success',
                                        title: 'Connexion réussie'
                                    })
                                }
                            }
                            setTimeout(function () {
                                window.location.href = "/"; //will redirect to your blog page (an ex: blog.html)
                            }, 1000)
                        },
                        error: function (xhr, error, status) {
                            console.log(error, status);
                        }
                    });

                }
            },
            error: function (xhr, error, status) {
                console.log(error, status);
            }
        });

    } else {
        if ($('#email').val().length < 7) {
            $('#validEmail').text(InvalidInput)
        } else {
            $('#validEmail').text('')
        }
        if (emailIsValid($('#email').val()) == false) {
            $('#validEmail').text(InvalidInput)
        } else {
            $('#validEmail').text('')
        }
        if ($('#password').val().length < 1) {
            $('#validPassword').text(InvalidInput)
        } else {
            $('#validPassword').text('')
        }
    }
}

function emailIsValid(email) {
    return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)
}