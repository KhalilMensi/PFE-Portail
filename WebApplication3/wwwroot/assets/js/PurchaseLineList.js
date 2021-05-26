var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    if ($.fn.DataTable.isDataTable('#myTable')) {
        dataTable.fnDestroy();
    }
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
            { "data": "discount" },
            {
                "data": "id",

                "render": function (data) {
                    return `<div class="row justify-content-center">
                        <div class="col-md-3 text-center">
                            <a href="#" OnClick="deletePurchaseLine(id=${data})">  
                               <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#dc2f02" class="bi bi-cart-x-fill" viewBox="0 0 16 16">
                                  <path d="M.5 1a.5.5 0 0 0 0 1h1.11l.401 1.607 1.498 7.985A.5.5 0 0 0 4 12h1a2 2 0 1 0 0 4 2 2 0 0 0 0-4h7a2 2 0 1 0 0 4 2 2 0 0 0 0-4h1a.5.5 0 0 0 .491-.408l1.5-8A.5.5 0 0 0 14.5 3H2.89l-.405-1.621A.5.5 0 0 0 2 1H.5zM6 14a1 1 0 1 1-2 0 1 1 0 0 1 2 0zm7 0a1 1 0 1 1-2 0 1 1 0 0 1 2 0zM7.354 5.646L8.5 6.793l1.146-1.147a.5.5 0 0 1 .708.708L9.207 7.5l1.147 1.146a.5.5 0 0 1-.708.708L8.5 8.207 7.354 9.354a.5.5 0 1 1-.708-.708L7.793 7.5 6.646 6.354a.5.5 0 1 1 .708-.708z"/>
                               </svg>
                            </a>
                        </div>
                        </div>`;
                },
            }
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
}

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
