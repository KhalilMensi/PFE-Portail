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
            "order": [[0, "desc"]],
            "ajax": {
                "url": "/Purchase/getAll",
                "dataSrc": ""
            },
            "columns": [
                { "data": "purchaseNumber" },
                { "data": "idUser" },
                { "data": "purchaseDate" },
                { "data": "amountTTC", render: function (data, type, full, meta) { return data + " DT" } },
                { "data": "state" },
                {
                    "data": "id",

                    "render": function (data) {
                        return `<div class="row justify-content-center">
                        <div class="col-md-3 text-center">
                             <a href="/PurchaseLine/ListPurchaseLine?id=${data}">
                                <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#0e49b5" class="bi bi-eye-fill" viewBox="0 0 16 16">
                                  <path d="M10.5 8a2.5 2.5 0 1 1-5 0 2.5 2.5 0 0 1 5 0z"/>
                                  <path d="M0 8s3-5.5 8-5.5S16 8 16 8s-3 5.5-8 5.5S0 8 0 8zm8 3.5a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7z"/>
                                </svg>
                              </a>
                        </div>
                         <div class="col-md-3 text-center">
                             <a href="/Purchase/UpsertPurchase?id=${data}" >  
                                 <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#9088d4" class="bi bi-tools" viewBox="0 0 16 16">
                                 <path d="M1 0L0 1l2.2 3.081a1 1 0 0 0 .815.419h.07a1 1 0 0 1 .708.293l2.675 2.675-2.617 2.654A3.003 3.003 0 0 0 0 13a3 3 0 1 0 5.878-.851l2.654-2.617.968.968-.305.914a1 1 0 0 0 .242 1.023l3.356 3.356a1 1 0 0 0 1.414 0l1.586-1.586a1 1 0 0 0 0-1.414l-3.356-3.356a1 1 0 0 0-1.023-.242L10.5 9.5l-.96-.96 2.68-2.643A3.005 3.005 0 0 0 16 3c0-.269-.035-.53-.102-.777l-2.14 2.141L12 4l-.364-1.757L13.777.102a3 3 0 0 0-3.675 3.68L7.462 6.46 4.793 3.793a1 1 0 0 1-.293-.707v-.071a1 1 0 0 0-.419-.814L1 0zm9.646 10.646a.5.5 0 0 1 .708 0l3 3a.5.5 0 0 1-.708.708l-3-3a.5.5 0 0 1 0-.708zM3 11l.471.242.529.026.287.445.445.287.026.529L5 13l-.242.471-.026.529-.445.287-.287.445-.529.026L3 15l-.471-.242L2 14.732l-.287-.445L1.268 14l-.026-.529L1 13l.242-.471.026-.529.445-.287.287-.445.529-.026L3 11z"/>
                                 </svg>
                             </a>
                        </div>
                        <div class="col-md-3 text-center">
                            <a href="#" OnClick="deletePurchase(id=${data})">  
                                <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#dc2f02" class="bi bi-cart-x" viewBox="0 0 16 16">
                                    <path d="M7.354 5.646a.5.5 0 1 0-.708.708L7.793 7.5 6.646 8.646a.5.5 0 1 0 .708.708L8.5 8.207l1.146 1.147a.5.5 0 0 0 .708-.708L9.207 7.5l1.147-1.146a.5.5 0 0 0-.708-.708L8.5 6.793 7.354 5.646z"/>
                                    <path d="M.5 1a.5.5 0 0 0 0 1h1.11l.401 1.607 1.498 7.985A.5.5 0 0 0 4 12h1a2 2 0 1 0 0 4 2 2 0 0 0 0-4h7a2 2 0 1 0 0 4 2 2 0 0 0 0-4h1a.5.5 0 0 0 .491-.408l1.5-8A.5.5 0 0 0 14.5 3H2.89l-.405-1.621A.5.5 0 0 0 2 1H.5zm3.915 10L3.102 4h10.796l-1.313 7h-8.17zM6 14a1 1 0 1 1-2 0 1 1 0 0 1 2 0zm7 0a1 1 0 1 1-2 0 1 1 0 0 1 2 0z"/>
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
    } else if (cultureInfo == "en-US") {
        dataTable = $('#myTable').dataTable({
            "order": [[0, "desc"]],
            "ajax": {
                "url": "/Purchase/getAll",
                "dataSrc": ""
            },
            "columns": [
                { "data": "purchaseNumber" },
                { "data": "idUser" },
                { "data": "purchaseDate" },
                { "data": "amountTTC", render: function (data, type, full, meta) { return data + " DT" } },
                { "data": "state" },
                {
                    "data": "id",

                    "render": function (data) {
                        return `<div class="row justify-content-center">
                        <div class="col-md-3 text-center">
                             <a href="/PurchaseLine/ListPurchaseLine?id=${data}">
                                <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#0e49b5" class="bi bi-eye-fill" viewBox="0 0 16 16">
                                  <path d="M10.5 8a2.5 2.5 0 1 1-5 0 2.5 2.5 0 0 1 5 0z"/>
                                  <path d="M0 8s3-5.5 8-5.5S16 8 16 8s-3 5.5-8 5.5S0 8 0 8zm8 3.5a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7z"/>
                                </svg>
                              </a>
                        </div>
                         <div class="col-md-3 text-center">
                             <a href="/Purchase/UpsertPurchase?id=${data}" >  
                                 <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#9088d4" class="bi bi-tools" viewBox="0 0 16 16">
                                 <path d="M1 0L0 1l2.2 3.081a1 1 0 0 0 .815.419h.07a1 1 0 0 1 .708.293l2.675 2.675-2.617 2.654A3.003 3.003 0 0 0 0 13a3 3 0 1 0 5.878-.851l2.654-2.617.968.968-.305.914a1 1 0 0 0 .242 1.023l3.356 3.356a1 1 0 0 0 1.414 0l1.586-1.586a1 1 0 0 0 0-1.414l-3.356-3.356a1 1 0 0 0-1.023-.242L10.5 9.5l-.96-.96 2.68-2.643A3.005 3.005 0 0 0 16 3c0-.269-.035-.53-.102-.777l-2.14 2.141L12 4l-.364-1.757L13.777.102a3 3 0 0 0-3.675 3.68L7.462 6.46 4.793 3.793a1 1 0 0 1-.293-.707v-.071a1 1 0 0 0-.419-.814L1 0zm9.646 10.646a.5.5 0 0 1 .708 0l3 3a.5.5 0 0 1-.708.708l-3-3a.5.5 0 0 1 0-.708zM3 11l.471.242.529.026.287.445.445.287.026.529L5 13l-.242.471-.026.529-.445.287-.287.445-.529.026L3 15l-.471-.242L2 14.732l-.287-.445L1.268 14l-.026-.529L1 13l.242-.471.026-.529.445-.287.287-.445.529-.026L3 11z"/>
                                 </svg>
                             </a>
                        </div>
                        <div class="col-md-3 text-center">
                            <a href="#" OnClick="deletePurchase(id=${data})">  
                                <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#dc2f02" class="bi bi-cart-x" viewBox="0 0 16 16">
                                    <path d="M7.354 5.646a.5.5 0 1 0-.708.708L7.793 7.5 6.646 8.646a.5.5 0 1 0 .708.708L8.5 8.207l1.146 1.147a.5.5 0 0 0 .708-.708L9.207 7.5l1.147-1.146a.5.5 0 0 0-.708-.708L8.5 6.793 7.354 5.646z"/>
                                    <path d="M.5 1a.5.5 0 0 0 0 1h1.11l.401 1.607 1.498 7.985A.5.5 0 0 0 4 12h1a2 2 0 1 0 0 4 2 2 0 0 0 0-4h7a2 2 0 1 0 0 4 2 2 0 0 0 0-4h1a.5.5 0 0 0 .491-.408l1.5-8A.5.5 0 0 0 14.5 3H2.89l-.405-1.621A.5.5 0 0 0 2 1H.5zm3.915 10L3.102 4h10.796l-1.313 7h-8.17zM6 14a1 1 0 1 1-2 0 1 1 0 0 1 2 0zm7 0a1 1 0 1 1-2 0 1 1 0 0 1 2 0z"/>
                                </svg>
                            </a>
                        </div>
                        </div>`;
                    },
                }
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

    } else if (cultureInfo == "ar") {
        dataTable = $('#myTable').dataTable({
            "order": [[0, "desc"]],
            "ajax": {
                "url": "/Purchase/getAll",
                "dataSrc": ""
            },
            "columns": [
                { "data": "purchaseNumber" },
                { "data": "idUser" },
                { "data": "purchaseDate" },
                { "data": "amountTTC", render: function (data, type, full, meta) { return data + " DT" } },
                { "data": "state" },
                {
                    "data": "id",

                    "render": function (data) {
                        return `<div class="row justify-content-center">
                        <div class="col-md-3 text-center">
                             <a href="/PurchaseLine/ListPurchaseLine?id=${data}">
                                <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#0e49b5" class="bi bi-eye-fill" viewBox="0 0 16 16">
                                  <path d="M10.5 8a2.5 2.5 0 1 1-5 0 2.5 2.5 0 0 1 5 0z"/>
                                  <path d="M0 8s3-5.5 8-5.5S16 8 16 8s-3 5.5-8 5.5S0 8 0 8zm8 3.5a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7z"/>
                                </svg>
                              </a>
                        </div>
                         <div class="col-md-3 text-center">
                             <a href="/Purchase/UpsertPurchase?id=${data}" >  
                                 <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#9088d4" class="bi bi-tools" viewBox="0 0 16 16">
                                 <path d="M1 0L0 1l2.2 3.081a1 1 0 0 0 .815.419h.07a1 1 0 0 1 .708.293l2.675 2.675-2.617 2.654A3.003 3.003 0 0 0 0 13a3 3 0 1 0 5.878-.851l2.654-2.617.968.968-.305.914a1 1 0 0 0 .242 1.023l3.356 3.356a1 1 0 0 0 1.414 0l1.586-1.586a1 1 0 0 0 0-1.414l-3.356-3.356a1 1 0 0 0-1.023-.242L10.5 9.5l-.96-.96 2.68-2.643A3.005 3.005 0 0 0 16 3c0-.269-.035-.53-.102-.777l-2.14 2.141L12 4l-.364-1.757L13.777.102a3 3 0 0 0-3.675 3.68L7.462 6.46 4.793 3.793a1 1 0 0 1-.293-.707v-.071a1 1 0 0 0-.419-.814L1 0zm9.646 10.646a.5.5 0 0 1 .708 0l3 3a.5.5 0 0 1-.708.708l-3-3a.5.5 0 0 1 0-.708zM3 11l.471.242.529.026.287.445.445.287.026.529L5 13l-.242.471-.026.529-.445.287-.287.445-.529.026L3 15l-.471-.242L2 14.732l-.287-.445L1.268 14l-.026-.529L1 13l.242-.471.026-.529.445-.287.287-.445.529-.026L3 11z"/>
                                 </svg>
                             </a>
                        </div>
                        <div class="col-md-3 text-center">
                            <a href="#" OnClick="deletePurchase(id=${data})">  
                                <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#dc2f02" class="bi bi-cart-x" viewBox="0 0 16 16">
                                    <path d="M7.354 5.646a.5.5 0 1 0-.708.708L7.793 7.5 6.646 8.646a.5.5 0 1 0 .708.708L8.5 8.207l1.146 1.147a.5.5 0 0 0 .708-.708L9.207 7.5l1.147-1.146a.5.5 0 0 0-.708-.708L8.5 6.793 7.354 5.646z"/>
                                    <path d="M.5 1a.5.5 0 0 0 0 1h1.11l.401 1.607 1.498 7.985A.5.5 0 0 0 4 12h1a2 2 0 1 0 0 4 2 2 0 0 0 0-4h7a2 2 0 1 0 0 4 2 2 0 0 0 0-4h1a.5.5 0 0 0 .491-.408l1.5-8A.5.5 0 0 0 14.5 3H2.89l-.405-1.621A.5.5 0 0 0 2 1H.5zm3.915 10L3.102 4h10.796l-1.313 7h-8.17zM6 14a1 1 0 1 1-2 0 1 1 0 0 1 2 0zm7 0a1 1 0 1 1-2 0 1 1 0 0 1 2 0z"/>
                                </svg>
                            </a>
                        </div>
                        </div>`;
                    },
                }
            ],
            "language": {
                "emptyTable": "ليست هناك بيانات متاحة في الجدول",
                "loadingRecords": "جارٍ التحميل...",
                "processing": "جارٍ التحميل...",
                "lengthMenu": "أظهر _MENU_ مدخلات",
                "zeroRecords": "لم يعثر على أية سجلات",
                "info": "إظهار _START_ إلى _END_ من أصل _TOTAL_ مدخل",
                "infoEmpty": "يعرض 0 إلى 0 من أصل 0 سجل",
                "infoFiltered": "(منتقاة من مجموع _MAX_ مُدخل)",
                "search": "ابحث:",
                "paginate": {
                    "first": "الأول",
                    "previous": "السابق",
                    "next": "التالي",
                    "last": "الأخير"
                },
                "aria": {
                    "sortAscending": ": تفعيل لترتيب العمود تصاعدياً",
                    "sortDescending": ": تفعيل لترتيب العمود تنازلياً"
                },
            },
        });
    }
}

function deletePurchase(PurchaseId) {
    // alert(PuchaseId)
    if (cultureInfo == "en-US") {
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
                $.post('/Purchase/delete', { id: PurchaseId }, function (response) {
                    if (response.success) {
                        Swal.fire(
                            'Deleted !',
                            'Purchase has been deleted.',
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
    } else if (cultureInfo == "fr") {
        Swal.fire({
            title: 'êtes-vous sûr ?',
            text: "Vous ne pourrez pas revenir sur cela !",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Oui, Supprimez-le !'
        }).then((result) => {
            if (result.isConfirmed) {
                $.post('/Purchase/delete', { id: PurchaseId }, function (response) {
                    if (response.success) {
                        Swal.fire(
                            'Supprimé !',
                            'La commande a été supprimée !',
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
    } else if (cultureInfo == "ar") {
        Swal.fire({
            title: 'هل انت متأكد ؟',
            text: "لا يمكنك التراجع عن ذلك!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'نعم ، احذفها!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.post('/Purchase/delete', { id: PurchaseId }, function (response) {
                    if (response.success) {
                        Swal.fire(
                            'تم الحذف!',
                            'تم حذف الطلب!',
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
}


// save the Purchase SUBMIT the Form
function savePurchase() {
    if ($('#userId').val().length > 0 && $('#State').val().length > 0 && $('#PurchaseNumber').val().length > 0 && $('#PurchaseDate').val().length > 0 && $('#Type').val().length > 0 && $('#DiscountPercent').val().length > 0 && $('#Discount').val().length > 0 && $('#VatPercent').val().length > 0 && $('#Vat').val().length > 0 && $('#AmountHT').val().length > 0 && $('#AmountTTC').val().length > 0) {

        $('#validIdUser').text('')
        $('#validState').text('')
        $('#validPurchaseNumber').text('')
        $('#validPurchaseDate').text('')
        $('#validType').text('')
        $('#validDiscountPercent').text('')
        $('#validDiscount').text('')
        $('#validVatPercent').text('')
        $('#validVat').text('')
        $('#validAmountHT').text('')
        $('#validAmountTTC').text('')

        var form = document.getElementById('form')
        var formData = new FormData(form)

        $.ajax({
            url: '/Purchase/UpsertPurchase',
            type: 'post',
            data: formData,
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
                    if (response.message = "Ajout avec succes") {
                        Toast.fire({
                            icon: 'success',
                            title: 'Purchase Added successfully',
                        })
                    } else {
                        Toast.fire({
                            icon: 'success',
                            title: 'Purchase Updated successfully',
                        })
                    }
                    setTimeout(function () {
                        window.location.href = "/Purchase"; //will redirect to your blog page (an ex: blog.html)
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
            error: function (response) {
                console.log(response);
            }
        });
    }
    else {
        if ($('#State').val().length < 1) {
            $('#validState').text(InvalidInput)
        } else {
            $('#validState').text('')
        }
        if ($('#userId').val().length < 1) {
            $('#validIdUser').text(InvalidInput)
        } else {
           $('#validIdUser').text('')
        }
        if ($('#PurchaseNumber').val() < 1) {
            $('#validPurchaseNumber').text(InvalidInput)
        } else {
            $('#validPurchaseNumber').text('')
        }
        if ($('#PurchaseDate').val().length < 1) {
            $('#validPurchaseDate').text(InvalidInput)
        } else {
            $('#validPurchaseDate').text('')
        }
        if ($('#Type').val().length < 1) {
            $('#validType').text(InvalidInput)
        } else {
            $('#validType').text('')
        }
        if ($('#DiscountPercent').val().length < 1) {
            $('#validDiscountPercent').text(InvalidInput)
        } else {
            $('#validDiscountPercent').text('')
        }
        if ($('#Discount').val().length < 1) {
            $('#validDiscount').text(InvalidInput)
        } else {
            $('#validDiscount').text('')
        }
        if ($('#VatPercent').val().length < 1) {
            $('#validVatPercent').text(InvalidInput)
        } else {
            $('#validVatPercent').text('')
        }
        if ($('#Vat').val().length < 1) {
            $('#validVat').text(InvalidInput)
        } else {
            $('#validVat').text('')
        }
        if ($('#AmountHT').val().length < 1) {
            $('#validAmountHT').text(InvalidInput)
        } else {
            $('#validAmountHT').text('')
        }
        if ($('#AmountTTC').val().length < 1) {
            $('#validAmountTTC').text(InvalidInput)
        } else {
            $('#validAmountTTC').text('')
        }
    }
}
