var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    if ($.fn.DataTable.isDataTable('#myTable')) {
        dataTable.fnDestroy();
    }
    if (cultureInfo == "fr") {
        dataTable = $('#myTable').dataTable({
            "ajax": {
                "url": "PurchaseLine/getAll",
                "dataSrc": ""
            },
            "columns": [
                { "data": "idPurchase" },
                { "data": "idDocument" },
                { "data": "unitPrice" },
                { "data": "quantity" },
                { "data": "discount" }
            ],
            "language": {
                "processing": "Traitement en cours...",
                "search": "Recherche&nbsp;:",
                "lengthMenu": "Afficher _MENU_ &eacute;l&eacute;ments",
                "info": "Affichage de l'&eacute;lement _START_ &agrave; _END_ sur _TOTAL_ &eacute;l&eacute;ments",
                "infoEmpty": "Affichage de l'&eacute;lement 0 &agrave; 0 sur 0 &eacute;l&eacute;ments",
                "infoFiltered": "(filtr&eacute; de _MAX_ &eacute;l&eacute;ments au total)",
                "infoPostFix": "",
                "loadingRecords": "Chargement en cours...",
                "zeroRecords": "Aucun &eacute;l&eacute;ment &agrave; afficher",
                "emptyTable": "Aucune donnée disponible dans le tableau",
                "paginate": {
                    "first": "Premier",
                    "previous": "Pr&eacute;c&eacute;dent",
                    "next": "Suivant",
                    "last": "Dernier"
                },
                "aria": {
                    "sortAscending": ": activer pour trier la colonne par ordre croissant",
                    "sortDescending": ": activer pour trier la colonne par ordre décroissant"
                }
            },
        });
    } else if (cultureInfo == "en-US") {
        dataTable = $('#myTable').dataTable({
            "ajax": {
                "url": "PurchaseLine/getAll",
                "dataSrc": ""
            },
            "columns": [
                { "data": "idPurchase" },
                { "data": "idDocument" },
                { "data": "unitPrice" },
                { "data": "quantity" },
                { "data": "discount" }
            ],
            "language": {
                "decimal": "",
                "emptyTable": "No data available in table",
                "info": "Showing _START_ to _END_ of _TOTAL_ entries",
                "infoEmpty": "Showing 0 to 0 of 0 entries",
                "infoFiltered": "(filtered from _MAX_ total entries)",
                "infoPostFix": "",
                "thousands": ",",
                "lengthMenu": "Show _MENU_ entries",
                "loadingRecords": "Loading...",
                "processing": "Processing...",
                "search": "Search:",
                "zeroRecords": "No matching records found",
                "paginate": {
                    "first": "First",
                    "last": "Last",
                    "next": "Next",
                    "previous": "Previous"
                },
                "aria": {
                    "sortAscending": ": activate to sort column ascending",
                    "sortDescending": ": activate to sort column descending"
                }
            },
        });
    }
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
                $('#validIdPurchase').text(InvalidInput)
            } else {
                $('#validIdPurchase').text('')
            }
            if ($('#IdDocument').val() < 1) {
                $('#validIdDocument').text(InvalidInput)
            } else {
                $('#validIdDocument').text('')
            }
        if ($('#UnitPrice').val() < 1) {
            $('#validUnitPrice').text(InvalidInput)
        } else {
            $('#validUnitPrice').text('')
        }
        if ($('#Quantity').val() < 1) {
            $('#validQuantity').text(InvalidInput)
        } else {
            $('#validQuantity').text('')
        }
        if ($('#DiscountPercent').val() < 1) {
            $('#validDiscountPercent').text(InvalidInput)
        } else {
            $('#validDiscountPercent').text('')
        }
        if ($('#Discount').val() < 1) {
            $('#validDiscount').text(InvalidInput)
        } else {
            $('#validDiscount').text('')
        }
    }
}
