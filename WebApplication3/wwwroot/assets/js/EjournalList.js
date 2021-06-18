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
            "order": [[4, "desc"]],
            "ajax": {
                "url": "/Ejournal/getAll",
                "dataSrc": ""
            },
            "columns": [
                { "data": "originalTitle" },
                { "data": "issn" },
                { "data": "doi" },
                { "data": "price", render: function (data, type, full, meta) { return data + " DT" } },
                { "data": "publicationDate" },
                {
                    "data": "coverPageName", "render": function (data) {
                        return '<img class="rounded-circle" src="uploads/CoverPage/' + data + '" style="width:40px;height:40px;"/>';
                    }
                },
                {
                    "data": "id",

                    "render": function (data) {
                        return `<div class="row justify-content-center">
                        <div class="col-md-3 text-center">
                             <a href="/Ejournal/Details?id=${data}" target="_blank">
                                <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#0e49b5" class="bi bi-book-half" viewBox="0 0 16 16">
                                    <path d="M8.5 2.687c.654-.689 1.782-.886 3.112-.752 1.234.124 2.503.523 3.388.893v9.923c-.918-.35-2.107-.692-3.287-.81-1.094-.111-2.278-.039-3.213.492V2.687zM8 1.783C7.015.936 5.587.81 4.287.94c-1.514.153-3.042.672-3.994 1.105A.5.5 0 0 0 0 2.5v11a.5.5 0 0 0 .707.455c.882-.4 2.303-.881 3.68-1.02 1.409-.142 2.59.087 3.223.877a.5.5 0 0 0 .78 0c.633-.79 1.814-1.019 3.222-.877 1.378.139 2.8.62 3.681 1.02A.5.5 0 0 0 16 13.5v-11a.5.5 0 0 0-.293-.455c-.952-.433-2.48-.952-3.994-1.105C10.413.809 8.985.936 8 1.783z"/>
                                </svg>
                              </a>
                        </div>
                        <div class="col-md-3 text-center">
                             <a href="/Ejournal/UpsertEjournal?id=${data}" target="_blank">  
                                 <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#9088d4" class="bi bi-tools" viewBox="0 0 16 16">
                                 <path d="M1 0L0 1l2.2 3.081a1 1 0 0 0 .815.419h.07a1 1 0 0 1 .708.293l2.675 2.675-2.617 2.654A3.003 3.003 0 0 0 0 13a3 3 0 1 0 5.878-.851l2.654-2.617.968.968-.305.914a1 1 0 0 0 .242 1.023l3.356 3.356a1 1 0 0 0 1.414 0l1.586-1.586a1 1 0 0 0 0-1.414l-3.356-3.356a1 1 0 0 0-1.023-.242L10.5 9.5l-.96-.96 2.68-2.643A3.005 3.005 0 0 0 16 3c0-.269-.035-.53-.102-.777l-2.14 2.141L12 4l-.364-1.757L13.777.102a3 3 0 0 0-3.675 3.68L7.462 6.46 4.793 3.793a1 1 0 0 1-.293-.707v-.071a1 1 0 0 0-.419-.814L1 0zm9.646 10.646a.5.5 0 0 1 .708 0l3 3a.5.5 0 0 1-.708.708l-3-3a.5.5 0 0 1 0-.708zM3 11l.471.242.529.026.287.445.445.287.026.529L5 13l-.242.471-.026.529-.445.287-.287.445-.529.026L3 15l-.471-.242L2 14.732l-.287-.445L1.268 14l-.026-.529L1 13l.242-.471.026-.529.445-.287.287-.445.529-.026L3 11z"/>
                                 </svg>
                             </a>
                        </div>
                        <div class="col-md-3 text-center">
                            <a href="#" OnClick="deleteEjournal(id=${data})">  
                                 <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#dc2f02" class="bi bi-trash" viewBox="0 0 16 16">
                                    <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6z"/>
                                    <path fill-rule="evenodd" d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1v1zM4.118 4L4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4H4.118zM2.5 3V2h11v1h-11z"/>
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
            "order": [[4, "desc"]],
            "ajax": {
                "url": "/Ejournal/getAll",
                "dataSrc": ""
            },
            "columns": [
                { "data": "originalTitle" },
                { "data": "issn" },
                { "data": "doi" },
                { "data": "price", render: function (data, type, full, meta) { return data + " DT" } },
                { "data": "publicationDate" },
                {
                    "data": "coverPageName", "render": function (data) {
                        return '<img class="rounded-circle" src="uploads/CoverPage/' + data + '" style="width:40px;height:40px;"/>';
                    }
                },
                {
                    "data": "id",

                    "render": function (data) {
                        return `<div class="row justify-content-center">
                        <div class="col-md-3 text-center">
                             <a href="/Ejournal/Details?id=${data}" target="_blank">
                                <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#0e49b5" class="bi bi-book-half" viewBox="0 0 16 16">
                                    <path d="M8.5 2.687c.654-.689 1.782-.886 3.112-.752 1.234.124 2.503.523 3.388.893v9.923c-.918-.35-2.107-.692-3.287-.81-1.094-.111-2.278-.039-3.213.492V2.687zM8 1.783C7.015.936 5.587.81 4.287.94c-1.514.153-3.042.672-3.994 1.105A.5.5 0 0 0 0 2.5v11a.5.5 0 0 0 .707.455c.882-.4 2.303-.881 3.68-1.02 1.409-.142 2.59.087 3.223.877a.5.5 0 0 0 .78 0c.633-.79 1.814-1.019 3.222-.877 1.378.139 2.8.62 3.681 1.02A.5.5 0 0 0 16 13.5v-11a.5.5 0 0 0-.293-.455c-.952-.433-2.48-.952-3.994-1.105C10.413.809 8.985.936 8 1.783z"/>
                                </svg>
                              </a>
                        </div>
                        <div class="col-md-3 text-center">
                             <a href="/Ejournal/UpsertEjournal?id=${data}" target="_blank">  
                                 <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#9088d4" class="bi bi-tools" viewBox="0 0 16 16">
                                 <path d="M1 0L0 1l2.2 3.081a1 1 0 0 0 .815.419h.07a1 1 0 0 1 .708.293l2.675 2.675-2.617 2.654A3.003 3.003 0 0 0 0 13a3 3 0 1 0 5.878-.851l2.654-2.617.968.968-.305.914a1 1 0 0 0 .242 1.023l3.356 3.356a1 1 0 0 0 1.414 0l1.586-1.586a1 1 0 0 0 0-1.414l-3.356-3.356a1 1 0 0 0-1.023-.242L10.5 9.5l-.96-.96 2.68-2.643A3.005 3.005 0 0 0 16 3c0-.269-.035-.53-.102-.777l-2.14 2.141L12 4l-.364-1.757L13.777.102a3 3 0 0 0-3.675 3.68L7.462 6.46 4.793 3.793a1 1 0 0 1-.293-.707v-.071a1 1 0 0 0-.419-.814L1 0zm9.646 10.646a.5.5 0 0 1 .708 0l3 3a.5.5 0 0 1-.708.708l-3-3a.5.5 0 0 1 0-.708zM3 11l.471.242.529.026.287.445.445.287.026.529L5 13l-.242.471-.026.529-.445.287-.287.445-.529.026L3 15l-.471-.242L2 14.732l-.287-.445L1.268 14l-.026-.529L1 13l.242-.471.026-.529.445-.287.287-.445.529-.026L3 11z"/>
                                 </svg>
                             </a>
                        </div>
                        <div class="col-md-3 text-center">
                            <a href="#" OnClick="deleteEjournal(id=${data})">  
                                 <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#dc2f02" class="bi bi-trash" viewBox="0 0 16 16">
                                    <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6z"/>
                                    <path fill-rule="evenodd" d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1v1zM4.118 4L4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4H4.118zM2.5 3V2h11v1h-11z"/>
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
            "order": [[4, "desc"]],
            "ajax": {
                "url": "/Ejournal/getAll",
                "dataSrc": ""
            },
            "columns": [
                { "data": "originalTitle" },
                { "data": "issn" },
                { "data": "doi" },
                { "data": "price", render: function (data, type, full, meta) { return data + " DT" } },
                { "data": "publicationDate" },
                {
                    "data": "coverPageName", "render": function (data) {
                        return '<img class="rounded-circle" src="uploads/CoverPage/' + data + '" style="width:40px;height:40px;"/>';
                    }
                },
                {
                    "data": "id",

                    "render": function (data) {
                        return `<div class="row justify-content-center">
                        <div class="col-md-3 text-center">
                             <a href="/Ejournal/Details?id=${data}" target="_blank">
                                <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#0e49b5" class="bi bi-book-half" viewBox="0 0 16 16">
                                    <path d="M8.5 2.687c.654-.689 1.782-.886 3.112-.752 1.234.124 2.503.523 3.388.893v9.923c-.918-.35-2.107-.692-3.287-.81-1.094-.111-2.278-.039-3.213.492V2.687zM8 1.783C7.015.936 5.587.81 4.287.94c-1.514.153-3.042.672-3.994 1.105A.5.5 0 0 0 0 2.5v11a.5.5 0 0 0 .707.455c.882-.4 2.303-.881 3.68-1.02 1.409-.142 2.59.087 3.223.877a.5.5 0 0 0 .78 0c.633-.79 1.814-1.019 3.222-.877 1.378.139 2.8.62 3.681 1.02A.5.5 0 0 0 16 13.5v-11a.5.5 0 0 0-.293-.455c-.952-.433-2.48-.952-3.994-1.105C10.413.809 8.985.936 8 1.783z"/>
                                </svg>
                              </a>
                        </div>
                        <div class="col-md-3 text-center">
                             <a href="/Ejournal/UpsertEjournal?id=${data}" target="_blank">  
                                 <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#9088d4" class="bi bi-tools" viewBox="0 0 16 16">
                                 <path d="M1 0L0 1l2.2 3.081a1 1 0 0 0 .815.419h.07a1 1 0 0 1 .708.293l2.675 2.675-2.617 2.654A3.003 3.003 0 0 0 0 13a3 3 0 1 0 5.878-.851l2.654-2.617.968.968-.305.914a1 1 0 0 0 .242 1.023l3.356 3.356a1 1 0 0 0 1.414 0l1.586-1.586a1 1 0 0 0 0-1.414l-3.356-3.356a1 1 0 0 0-1.023-.242L10.5 9.5l-.96-.96 2.68-2.643A3.005 3.005 0 0 0 16 3c0-.269-.035-.53-.102-.777l-2.14 2.141L12 4l-.364-1.757L13.777.102a3 3 0 0 0-3.675 3.68L7.462 6.46 4.793 3.793a1 1 0 0 1-.293-.707v-.071a1 1 0 0 0-.419-.814L1 0zm9.646 10.646a.5.5 0 0 1 .708 0l3 3a.5.5 0 0 1-.708.708l-3-3a.5.5 0 0 1 0-.708zM3 11l.471.242.529.026.287.445.445.287.026.529L5 13l-.242.471-.026.529-.445.287-.287.445-.529.026L3 15l-.471-.242L2 14.732l-.287-.445L1.268 14l-.026-.529L1 13l.242-.471.026-.529.445-.287.287-.445.529-.026L3 11z"/>
                                 </svg>
                             </a>
                        </div>
                        <div class="col-md-3 text-center">
                            <a href="#" OnClick="deleteEjournal(id=${data})">  
                                 <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#dc2f02" class="bi bi-trash" viewBox="0 0 16 16">
                                    <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6z"/>
                                    <path fill-rule="evenodd" d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1v1zM4.118 4L4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4H4.118zM2.5 3V2h11v1h-11z"/>
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

function deleteEjournal(EjournalId) {
    // alert(EbookId)
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
                $.post('/Ejournal/delete', { id: EjournalId }, function (response) {
                    if (response.success) {
                        Swal.fire(
                            'Deleted!',
                            'Ejournal has been deleted.',
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
                $.post('/Ejournal/delete', { id: EjournalId }, function (response) {
                    if (response.success) {
                        Swal.fire(
                            'Supprimé !',
                            'La revue a été supprimée !',
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
            text: "لن تتمكن من عكس هذا!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'نعم ، احذفها!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.post('/Ejournal/delete', { id: EjournalId }, function (response) {
                    if (response.success) {
                        Swal.fire(
                            'تم الحذف!',
                            'تم حذف المراجعة!',
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

// save the Ebook SUBMIT the Form
function saveEjournal(bool) {
    if ($('#ISSN').val().length > 0 && $('#Editor').val().length > 0 && $('#Collection').val().length > 0 && $('#Theme').val().length > 0 && $('#Catalogue').val().length > 0 && $('#Doi').val().length > 0 && $('#OriginalTitle').val().length > 0 && newFunction(bool) && newFunction2(bool) && $('#CoverPage').val().length > 0 && $('#OriginalLanguage').val().length > 0 && $('#State').val().length > 0 && $('#PublicationDate').val().length > 0) {

        $('#validISSN').text('')
        $('#validEditor').text('')
        $('#validCollection').text('')

        $('#validTheme').text('')
        $('#validCatalogue').text('')
        $('#validDoi').text('')
        $('#validOriginalTitle').text('')

        $('#validFile').text('')
        $('#validCoverPage').text('')
        $('#validOriginalLanguage').text('')

        $('#validState').text('')
        $('#validPublicationDate').text('')

        var form = document.getElementById('form')

        $.ajax({
            url: '/Ejournal/UpsertEjournal',
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
                    if (bool == "False") {
                        Toast.fire({
                            icon: 'success',
                            title: 'Ejournal added successfully',
                        })
                        setTimeout(function () { window.location = "/Ejournal"; }, 2000);

                    } else {
                        Toast.fire({
                            icon: 'success',
                            title: 'Ejournal Updated successfully',
                        })
                        setTimeout(function () { window.location = "/Ejournal"; }, 2000);

                    }
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
        if ($('#ISSN').val().length < 1) {
            $('#validISSN').text(InvalidInput)
        } else {
            $('#validISSN').text('')
        }
        if ($('#Editor').val().length < 1) {
            $('#validEditor').text(InvalidInput)
        } else {
            $('#validEditor').text('')
        }
        if ($('#Collection').val().length < 1) {
            $('#validCollection').text(InvalidInput)
        } else {
            $('#validCollection').text('')
        }
        if ($('#Theme').val().length < 1) {
            $('#validTheme').text(InvalidInput)
        } else {
            $('#validTheme').text('')
        }
        if ($('#Catalogue').val().length < 1) {
            $('#validCatalogue').text(InvalidInput)
        } else {
            $('#validCatalogue').text('')
        }
        if ($('#Doi').val().length < 1) {
            $('#validDoi').text(InvalidInput)
        } else {
            $('#validDoi').text('')
        }
        if ($('#OriginalTitle').val().length < 1) {
            $('#validOriginalTitle').text(InvalidInput)
        } else {
            $('#validOriginalTitle').text('')
        }
        if (!newFunction(bool)) {
            $('#validFile').text(InvalidInput)
        } else {
            $('#validFile').text('')
        }
        if (!newFunction2(bool)) {
            $('#validCoverPage').text(InvalidInput)
        } else {
            $('#validCoverPage').text('')
        }
        if ($('#OriginalLanguage').val().length < 1) {
            $('#validOriginalLanguage').text(InvalidInput)
        } else {
            $('#validOriginalLanguage').text('')
        }
        if ($('#State').val().length < 1) {
            $('#validState').text(InvalidInput)
        } else {
            $('#validState').text('')
        }
        if ($('#PublicationDate').val().length < 1) {
            $('#validPublicationDate').text(InvalidInput)
        } else {
            $('#validPublicationDate').text('')
        }
    }
}

function newFunction(bool) {
    if (bool == "True")
        return true;
    else
        return $('#File').val().length > 0;
}

function newFunction2(bool) {
    if (bool == "True")
        return true;
    else
        return $('#CoverPage').val().length > 0;
}