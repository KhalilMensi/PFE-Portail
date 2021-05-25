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
            "url": "/User/getAll",
            "dataSrc": ""
           },
            "columns": [
                { "data": "code" },
                { "data": "email" },
                { "data": "password" },
                { "data": "profil" },
                {
                    "data": "filename",
                    "render": function (data) {
                        if (data != "") {
                            return '<img class="rounded-circle" src="uploads/user/' + data + '" style="width:40px;height:40px;"/>';
                        } else {
                            return '<img class="rounded-circle" src="uploads/user/unknown.png" style="width:40px;height:40px;"/>';
                        }
                    }
                },
                {
                    "data": "id",

                    "render": function (data) {
                        return `<div class="row justify-content-center">
                        <div class="col-md-3 text-center">
                             <a href="/User/Details?id=${data}">
                                 <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#0e49b5" class="bi bi-person-lines-fill" viewBox="0 0 16 16">
                                 <path d="M6 8a3 3 0 1 0 0-6 3 3 0 0 0 0 6zm-5 6s-1 0-1-1 1-4 6-4 6 3 6 4-1 1-1 1H1zM11 3.5a.5.5 0 0 1 .5-.5h4a.5.5 0 0 1 0 1h-4a.5.5 0 0 1-.5-.5zm.5 2.5a.5.5 0 0 0 0 1h4a.5.5 0 0 0 0-1h-4zm2 3a.5.5 0 0 0 0 1h2a.5.5 0 0 0 0-1h-2zm0 3a.5.5 0 0 0 0 1h2a.5.5 0 0 0 0-1h-2z"/>
                                 </svg>
                              </a>
                        </div>
                         <div class="col-md-3 text-center">
                             <a href="/User/UpsertUser?id=${data}" >  
                                 <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#9088d4" class="bi bi-tools" viewBox="0 0 16 16">
                                 <path d="M1 0L0 1l2.2 3.081a1 1 0 0 0 .815.419h.07a1 1 0 0 1 .708.293l2.675 2.675-2.617 2.654A3.003 3.003 0 0 0 0 13a3 3 0 1 0 5.878-.851l2.654-2.617.968.968-.305.914a1 1 0 0 0 .242 1.023l3.356 3.356a1 1 0 0 0 1.414 0l1.586-1.586a1 1 0 0 0 0-1.414l-3.356-3.356a1 1 0 0 0-1.023-.242L10.5 9.5l-.96-.96 2.68-2.643A3.005 3.005 0 0 0 16 3c0-.269-.035-.53-.102-.777l-2.14 2.141L12 4l-.364-1.757L13.777.102a3 3 0 0 0-3.675 3.68L7.462 6.46 4.793 3.793a1 1 0 0 1-.293-.707v-.071a1 1 0 0 0-.419-.814L1 0zm9.646 10.646a.5.5 0 0 1 .708 0l3 3a.5.5 0 0 1-.708.708l-3-3a.5.5 0 0 1 0-.708zM3 11l.471.242.529.026.287.445.445.287.026.529L5 13l-.242.471-.026.529-.445.287-.287.445-.529.026L3 15l-.471-.242L2 14.732l-.287-.445L1.268 14l-.026-.529L1 13l.242-.471.026-.529.445-.287.287-.445.529-.026L3 11z"/>
                                 </svg>
                             </a>
                        </div>
                        <div class="col-md-3 text-center">
                             <a href="#" OnClick="deleteUser(id=${data})">  
                                 <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#dc2f02" class="bi bi-person-x-fill" viewBox="0 0 16 16">
                                     <path fill-rule="evenodd" d="M1 14s-1 0-1-1 1-4 6-4 6 3 6 4-1 1-1 1H1zm5-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6zm6.146-2.854a.5.5 0 0 1 .708 0L14 6.293l1.146-1.147a.5.5 0 0 1 .708.708L14.707 7l1.147 1.146a.5.5 0 0 1-.708.708L14 7.707l-1.146 1.147a.5.5 0 0 1-.708-.708L13.293 7l-1.147-1.146a.5.5 0 0 1 0-.708z"/>
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

function deleteUser(UserId) {
   // alert(UserId)
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
                $.post('/User/delete', { id: UserId }, function (response) {
                    if (response.success) {
                        Swal.fire(
                            'Deleted!',
                            'User has been deleted.',
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

