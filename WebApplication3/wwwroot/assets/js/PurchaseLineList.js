function deletePurchaseLine(PurchaseLineId) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.post('/PurchaseLine/delete', { id: PurchaseLineId }, function (response) {
                if (response.success) {
                    Swal.fire(
                        'Deleted !',
                        'PurchaseLine has been deleted.',
                        'success'
                    );
                    loadDataTable();
                }
                else {
                    Swal.fire(
                        'Error!',
                        response.message,
                        'error'
                    )
                }
            })
        }
    })
}

// save the Ebook SUBMIT the Form
function savePurchaseLine() {
    if ($('#IdPurchase').val() > 0 && $('#IdDocument').val() > 0 && $('#UnitPrice').val() > 0 && $('#Quantity').val() > 0 && $('#DiscountPercent').val() > 0 && $('#Discount').val() > 0) {

        $('#validIdPurchase').text('')
        $('#validIdDocument').text('')

        $('#validUnitPrice').text('')
        $('#validQuantity').text('')
        $('#validDiscountPercent').text('')
        $('#validDiscount').text('')

        var form = document.getElementById('form')

        $.ajax({
            url: '/PurchaseLine/UpsertPurchaseLine',
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
                        },
                    })
                    if (response.message == "Ajout PurchaseLine avec succes") {
                        Toast.fire({
                            icon: 'success',
                            title: 'PurchaseLine added successfully',
                        })
                    } else {
                        Toast.fire({
                            icon: 'success',
                            title: 'PurchaseLine Updated successfully',
                        })
                    }
                    setTimeout(function () {
                        window.location.href = "/PurchaseLine"; //will redirect to your blog page (an ex: blog.html)
                    }, 1500);

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
            },
            error: function (xhr, error, status) {
                console.log(error, status);
            }
        });
    }
    else {
            if ($('#IdPurchase').val() < 1) {
                $('#validIdPurchase').text('Champ obligatoire')
            } else {
                $('#validIdPurchase').text('')
            }
            if ($('#IdDocument').val() < 1) {
                $('#validIdDocument').text('Champ obligatoire')
            } else {
                $('#validIdDocument').text('')
            }
        if ($('#UnitPrice').val() < 1) {
            $('#validUnitPrice').text('Champ obligatoire')
        } else {
            $('#validUnitPrice').text('')
        }
        if ($('#Quantity').val() < 1) {
            $('#validQuantity').text('Champ obligatoire')
        } else {
            $('#validQuantity').text('')
        }
        if ($('#DiscountPercent').val() < 1) {
            $('#validDiscountPercent').text('Champ obligatoire')
        } else {
            $('#validDiscountPercent').text('')
        }
        if ($('#Discount').val() < 1) {
            $('#validDiscount').text('Champ obligatoire')
        } else {
            $('#validDiscount').text('')
        }
    }
}
