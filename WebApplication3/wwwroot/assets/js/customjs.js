
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
                    url: '/PurchaseLine/UpsertPurchaseLine',
                    type: 'post',
                    dataType: 'json',
                    data: {
                        IdPurchase: response.message, IdDocument: id,
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
                                    $("#dropdown-content").empty()
                                    $.ajax({
                                        url: '/PurchaseLine/PurchaseLineUser',
                                        type: 'post',
                                        dataType: 'json',
                                        cache: false,
                                        success: function (response) {
                                            if (response != null) {
                                                for (var j = 0; j < response.listDocumentPurchased.length; j++) {
                                                    for (var i = 0; i < response.listPurchaseLine.length; i++) {
                                                        if (response.listDocumentPurchased[j].id == response.listPurchaseLine[i].idDocument) {
                                                            $('#dropdown-content').append(`
                                                <div class="row justify-content-center align-content-center p-2">
                                                    <div class="col-md-3 text-center">
                                                        <div class="row justify-content-start align-content-center" style="width:80px;">
                                                            <img src="${"../../uploads/CoverPage/" + response.listDocumentPurchased[j].coverPageName}" class="col-md-12" style="border-radius: 50%; height:50px">
                                                        </div>
                                                    </div>
                                                    <div class="col-md-9">
                                                        <div class="row justify-content-start align-content-center" style="height:25px">
                                                            <span style="font-family: 'Roboto', sans-serif;font-size:15px"><b>${response.listDocumentPurchased[j].originalTitle} </b></span>
                                                        </div>
                                                        <div class="row justify-content-start align-content-center" style="height:25px">
                                                            <span style="font-family: 'Roboto', sans-serif;font-size:12px">${response.listPurchaseLine[i].unitPrice} DT * ${response.listPurchaseLine[i].quantity}</span>
                                                        </div>
                                                    </div>
                                                </div>`
                                                            )
                                                        }
                                                    }
                                                }
                                                $('#dropdown-content').append(`
                                    <div class="row justify-content-center align-content-center p-2">
                                        <div class="col-md-12">
                                            <div class="row justify-content-start align-content-center" style="height:25px">
                                                <span style="font-family: 'Roboto Condensed', sans-serif;font-size:15px;color:coral">
                                                    <b style="color:#7868e6">Total TTC : </b>${response.purchase.amountTTC} DT
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row justify-content-center align-content-center p-2">
                                        <div class="col-md-12 pb-2">
                                            <div class="row justify-content-center align-content-center pt-2 pb-2" style="height:25px ">
                                                <a class="text-center btn btn-light" style="font-size:15px;cursor:pointer;" href="/PurchaseLine/ListPurchaseLine">
                                                     ${ConfirmPurchase}
                                                </a>
                                            </div>
                                        </div>
                                    </div>
`)
                                            } else {
                                                $('#dropdown-content').append(`
                                    <div class="row justify-content-center align-content-center p-2">
                                        <div class="col-md-12">
                                            <div class="row justify-content-center align-content-center" style="height:25px">
                                                <span style="font-family: 'Roboto Condensed', sans-serif;font-size:15px;color:coral">
                                                    <b>@localizer["EmptyCart"]</b>
                                                </span>
                                            </div>
                                        </div>
                                    </div>`)
                                            }
                                            const Toast = Swal.mixin({
                                                toast: true,
                                                position: 'top-end',
                                                showConfirmButton: false,
                                                timer: 1000,
                                                timerProgressBar: true,
                                                didOpen: (toast) => {
                                                    toast.addEventListener('mouseenter', Swal.stopTimer)
                                                    toast.addEventListener('mouseleave', Swal.resumeTimer)
                                                }
                                            })
                                            if (cultureInfo == "fr") {
                                                Toast.fire({
                                                    icon: 'success',
                                                    title: 'Mise a jours du Panier'
                                                })
                                            } else {
                                                Toast.fire({
                                                    icon: 'success',
                                                    title: 'Shopping cart was updated'
                                                })
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
                        }
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
