
$(document).ready(function () {
    $('.owl-carousel').owlCarousel({
        loop: false,
        margin: 20,
        center: false,
        nav: true,
        navText: ['<i class="icon-arrow"></i>', '<i class="icon-arrow"></i>'],
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 3
            },
            1000: {
                items: 5
            }
        }

    });
});

function addProduct(id) {

    var userId;
    var email = AppGlobal.user.Name

    if (email != "") {
        $.ajax({
            url: '/User/getUserId',
            type: 'post',
            dataType: 'json',
            data: {
                Email: email,
            },
            cache: false,
            success: function (response) {
                userId = response;
                addPurchase(userId, id);
            },
            error: function (xhr, error, status) {
                console.log(error, status);
            }
        });
    } else {
        window.location.href = "/Account/Login"; //will redirect to your blog page (an ex: blog.html)
    }
}

function addPurchase(userId, id) {
    var insert = false;
    $.ajax({
        url: '/Purchase/addPurchase',
        type: 'post',
        dataType: 'json',
        data: {
            IdUser: userId, IdDocument: id,
        },
        cache: false,
        success: function (response) {
            if (response.success) {

                $.ajax({
                    url: '/Purchase/getMax',
                    type: 'get',
                    dataType: 'json',
                    cache: false,
                    success: function (response) {
                        PurchaseId = response;

                        $.ajax({
                            url: '/PurchaseLine/UpsertPurchaseLine',
                            type: 'post',
                            dataType: 'json',
                            data: {
                                IdPurchase: PurchaseId, IdDocument: id,
                            },
                            cache: false,
                            success: function (response) {

                                if (AppGlobal.user.role == "User" || AppGlobal.user.role == "Admin") {

                                    $.ajax({
                                        url: '/PurchaseLine/NbPurchaseLine',
                                        type: 'post',
                                        dataType: 'json',
                                        cache: false,
                                        success: function (response) {
                                            $('#NbPurchaseLine').text(response)
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
                    },
                    error: function (xhr, error, status) {
                        console.log(error, status);
                    }
                });
            } else {
                console.log("AddPurchase Failed !")
            }
        },
        error: function (xhr, error, status) {
            console.log(error, status);
        }
    });
}
