var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    if ($.fn.DataTable.isDataTable('#myTable')) {
        dataTable.fnDestroy();
    }
    dataTable = $('#myTable').dataTable({
        responsive: true,
        "ajax": {
            "url": "/Document/getAll",
            "dataSrc": ""
        },
        "columns": [
            { "data": "editor" },
            { "data": "collection" },
            { "data": "theme" },
            { "data": "catalogue" },
            { "data": "doi" },
            {
                "data": "id",

                "render": function (data) {
                        return `<div class="row justify-content-center">
                        <div class="col-md-3 text-center">
                             <a href="/Document/Details?id=${data}">
                                <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#0e49b5" class="bi bi-file-earmark-text-fill" viewBox="0 0 16 16">
                                    <path d="M9.293 0H4a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h8a2 2 0 0 0 2-2V4.707A1 1 0 0 0 13.707 4L10 .293A1 1 0 0 0 9.293 0zM9.5 3.5v-2l3 3h-2a1 1 0 0 1-1-1zM4.5 9a.5.5 0 0 1 0-1h7a.5.5 0 0 1 0 1h-7zM4 10.5a.5.5 0 0 1 .5-.5h7a.5.5 0 0 1 0 1h-7a.5.5 0 0 1-.5-.5zm.5 2.5a.5.5 0 0 1 0-1h4a.5.5 0 0 1 0 1h-4z"/>
                                </svg>
                             </a>
                        </div>
                         <div class="col-md-3 text-center">
                             <a href="/Document/UpsertDocument?id=${data}" >  
                                 <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#9088d4" class="bi bi-tools" viewBox="0 0 16 16">
                                 <path d="M1 0L0 1l2.2 3.081a1 1 0 0 0 .815.419h.07a1 1 0 0 1 .708.293l2.675 2.675-2.617 2.654A3.003 3.003 0 0 0 0 13a3 3 0 1 0 5.878-.851l2.654-2.617.968.968-.305.914a1 1 0 0 0 .242 1.023l3.356 3.356a1 1 0 0 0 1.414 0l1.586-1.586a1 1 0 0 0 0-1.414l-3.356-3.356a1 1 0 0 0-1.023-.242L10.5 9.5l-.96-.96 2.68-2.643A3.005 3.005 0 0 0 16 3c0-.269-.035-.53-.102-.777l-2.14 2.141L12 4l-.364-1.757L13.777.102a3 3 0 0 0-3.675 3.68L7.462 6.46 4.793 3.793a1 1 0 0 1-.293-.707v-.071a1 1 0 0 0-.419-.814L1 0zm9.646 10.646a.5.5 0 0 1 .708 0l3 3a.5.5 0 0 1-.708.708l-3-3a.5.5 0 0 1 0-.708zM3 11l.471.242.529.026.287.445.445.287.026.529L5 13l-.242.471-.026.529-.445.287-.287.445-.529.026L3 15l-.471-.242L2 14.732l-.287-.445L1.268 14l-.026-.529L1 13l.242-.471.026-.529.445-.287.287-.445.529-.026L3 11z"/>
                                 </svg>
                             </a>
                        </div>
                            <div class="col-md-3 text-center">
                                <a href="#" OnClick="deleteDocument(id=${data})">  
                                    <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#d62828" class="bi bi-trash-fill" viewBox="0 0 16 16">
                                        <path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1H2.5zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5zM8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5zm3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0z"/>
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

function deleteDocument(DocumentId) {
    // alert(DocumentId)
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
            $.post('/Document/delete', { id: DocumentId }, function (response) {
                if (response.success) {
                    Swal.fire(
                        'Deleted!',
                        'Document has been deleted.',
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
function saveDocument(bool) {
    console.log(bool)
    if ($('#Editor').val().length > 0 && $('#Collection').val().length > 0 && $('#Theme').val().length > 0 && $('#Catalogue').val().length > 0 && $('#Doi').val().length > 0 && $('#OriginalTitle').val().length > 0 && newFunction(bool) && newFunction2(bool) && $('#DocumentType').val().length > 0 && $('#OriginalLanguage').val().length > 0 && $('#State').val().length > 0 && $('#PublicationDate').val().length > 0) {

        $('#validEditor').text('')
        $('#validCollection').text('')

        $('#validTheme').text('')
        $('#validCatalogue').text('')
        $('#validDoi').text('')
        $('#validOriginalTitle').text('')

        $('#validFile').text('')
        $('#validCoverPage').text('')
        $('#validDocumentType').text('')
        $('#validOriginalLanguage').text('')

        $('#validState').text('')
        $('#validPublicationDate').text('')

        var form = document.getElementById('form')

        $.ajax({
            url: '/Document/UpsertDocument',
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
                            title: 'Document added successfully',
                        })
                    } else {
                        Toast.fire({
                            icon: 'success',
                            title: 'Document Updated successfully',
                        })
                    }
                    setTimeout(function () {
                        window.location.href = "/Document"; //will redirect to your blog page (an ex: blog.html)
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
        if ($('#Editor').val().length < 1) {
            $('#validEditor').text('Champ obligatoire')
        } else {
            $('#validEditor').text('')
        }
        if ($('#Collection').val().length < 1) {
            $('#validCollection').text('Champ obligatoire')
        } else {
            $('#validCollection').text('')
        }
        if ($('#Theme').val().length < 1) {
            $('#validTheme').text('Champ obligatoire')
        } else {
            $('#validTheme').text('')
        }
        if ($('#Catalogue').val().length < 1) {
            $('#validCatalogue').text('Champ obligatoire')
        } else {
            $('#validCatalogue').text('')
        }
        if ($('#Doi').val().length < 1) {
            $('#validDoi').text('Champ obligatoire')
        } else {
            $('#validDoi').text('')
        }
        if ($('#OriginalTitle').val().length < 1) {
            $('#validOriginalTitle').text('Champ obligatoire')
        } else {
            $('#validOriginalTitle').text('')
        }
        if (!newFunction(bool)) {
            $('#validFile').text('Champ obligatoire')
        } else {
            $('#validFile').text('')
        }
        if (!newFunction2(bool)) {
            $('#validCoverPage').text('Champ obligatoire')
        } else {
            $('#validCoverPage').text('')
        }
        if ($('#DocumentType').val().length < 1) {
            $('#validDocumentType').text('Champ obligatoire')
        } else {
            $('#validDocumentType').text('')
        }
        if ($('#OriginalLanguage').val().length < 1) {
            $('#validOriginalLanguage').text('Champ obligatoire')
        } else {
            $('#validOriginalLanguage').text('')
        }
        if ($('#State').val().length < 1) {
            $('#validState').text('Champ obligatoire')
        } else {
            $('#validState').text('')
        }
        if ($('#PublicationDate').val().length < 1) {
            $('#validPublicationDate').text('Champ obligatoire')
        } else {
            $('#validPublicationDate').text('')
        }
    }
}

function newFunction(bool) {
    if (bool == "True")
        return true;
    return $('#File').val().length > 0 ;
}

function newFunction2(bool) {
    if (bool == "True")
        return true;
    return $('#CoverPage').val().length > 0;
}

