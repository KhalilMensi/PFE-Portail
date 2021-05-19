
function Update() {
    var email = AppGlobal.user.Name

    console.log(email)
    $.ajax({
        url: '/User/UpsertUser',
        type: 'post',
        dataType: 'json',
        data: {
            Email: email,
        },
        cache: false,
        success: function (response) {
            window.location.href = "/User/UpsertUser?id=" + response; //will redirect to your blog page (an ex: blog.html)
        },
        error: function (xhr, error, status) {
            console.log(error, status);
        }
    });
}

function getId() {

    var email = AppGlobal.user.Name

    $.ajax({
        url: '/User/getUserId',
        type: 'post',
        dataType: 'json',
        data: {
            Email: email,
        },
        cache: false,
        success: function (response) {
            window.location.href = "/User/Details?id=" + response; //will redirect to your blog page (an ex: blog.html)
        },
        error: function (xhr, error, status) {
            console.log(error, status);
        }
    });
}

function saveUser() {
    if ($('#password').val().length > 0 && $('#profil').val().length > 0 && $('#email').val().length > 0 && emailIsValid($('#email').val()) == true && $('#Name').val().length > 0 && $('#LastName').val().length > 0) {
        $('#validPassword').text('')
        $('#validProfil').text('')
        $('#validEmail').text('')
        $('#validName').text('')
        $('#validLastName').text('')

        var form = document.getElementById('form')

        $.ajax({
            url: '/User/UpsertUser',
            type: 'post',
            data: new FormData(form),
            cache: false,
            contentType: false,
            processData: false,
            success: function (response) {
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
                        },
                    })
                    if (response.message == "User Updated successfuly") {
                        Toast.fire({
                            icon: 'success',
                            title: 'User Updated successfully',
                        })
                    } else {
                        Toast.fire({
                            icon: 'success',
                            title: 'Signed in successfully',
                        })
                        setTimeout(function () {
                            window.location.href = "/Account/Login"; //will redirect to your blog page (an ex: blog.html)
                        }, 1500);
                    }

                } else {
                    if (response.message == "User already Exists !") {
                        $('#validEmail').text('User already Exists !')
                    } else {
                        const Toast = Swal.mixin({
                            toast: true,
                            position: 'top-end',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                            didOpen: (toast) => {
                                toast.addEventListener('mouseenter', Swal.stopTimer)
                                toast.addEventListener('mouseleave', Swal.resumeTimer)
                            },
                        })
                        Toast.fire({
                            icon: 'error',
                            title: response.message
                        })
                    }

                }
            },
            error: function (xhr, error, status) {
                console.log(error, status);
            }
        });
    }
    else {
        if ($('#password').val().length < 1) {
            $('#validPassword').text('Champ obligatoire')
        } else {
            $('#validPassword').text('')
        }
        if ($('#profil').val().length < 1) {
            $('#validProfil').text('Champ obligatoire')
        } else {
            $('#validProfil').text('')
        }
        if (emailIsValid($('#email').val()) == false || $('#email').val().length < 7) {
            $('#validEmail').text('Invalid Email')
        } else {
            $('#validEmail').text('')
        }

        if ($('#Name').val().length < 1) {
            $('#validName').text('Champ obligatoire')
        } else {
            $('#validName').text('')
        }
        if ($('#LastName').val().length < 1) {
            $('#validLastName').text('Champ obligatoire')
        } else {
            $('#validLastName').text('')
        }
    }
}


function emailIsValid(email) {
    return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)
}