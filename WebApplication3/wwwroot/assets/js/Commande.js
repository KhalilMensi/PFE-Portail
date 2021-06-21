var dataTable;
$(document).ready(function () {
    loadDataTable();
    $('#myPurchases').on('click', 'tbody tr', function () {
        var row = dataTable.api().row($(this)).data();
        console.log(row["id"]);

        location.href = "/PurchaseLine/ListPurchaseLine/" + row["id"];
    })
});
function loadDataTable() {
    if ($.fn.DataTable.isDataTable('#myPurchases')) {
        dataTable.fnDestroy();
    }
    if (cultureInfo == "fr") {
        dataTable = $('#myPurchases').dataTable({
            "order": [[0, "desc"]],
            "ajax": {
                "url": "/Purchase/UserPurchases",
                "dataSrc": ""
            },
            "columns": [
                { "data": "purchaseNumber" },
                { "data": "purchaseDate" },
                { "data": "amountHT", render: function (data, type, full, meta) { return data + " DT" } },
                { "data": "amountTTC", render: function (data, type, full, meta) { return data + " DT" } },
                { "data": "state" },
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
        dataTable = $('#myPurchases').dataTable({
            "order": [[0, "desc"]],
            "ajax": {
                "url": "/Purchase/UserPurchases",
                "dataSrc": ""
            },
            "columns": [
                { "data": "purchaseNumber" },
                { "data": "purchaseDate" },
                { "data": "amountHT", render: function (data, type, full, meta) { return data + " DT" } },
                { "data": "amountTTC", render: function (data, type, full, meta) { return data + " DT" } },
                { "data": "state" },
            ],
            "language": {
                "emptyTable": "No data available in table",
                "info": "Showing _START_ to _END_ of _TOTAL_ entries",
                "infoEmpty": "Showing 0 to 0 of 0 entries",
                "infoFiltered": "(filtered from _MAX_ total entries)",
                "infoThousands": ",",
                "lengthMenu": "Show _MENU_ entries",
                "loadingRecords": "Loading...",
                "processing": "Processing...",
                "search": "Search:",
                "zeroRecords": "No matching records found",
                "thousands": ",",
                "paginate": {
                    "first": "First",
                    "last": "Last",
                    "next": "Next",
                    "previous": "Previous"
                },
                "aria": {
                    "sortAscending": ": activate to sort column ascending",
                    "sortDescending": ": activate to sort column descending"
                },
            },
        });

    } else if (cultureInfo == "ar") {
        dataTable = $('#myPurchases').dataTable({
            "order": [[0, "desc"]],
            "ajax": {
                "url": "/Purchase/UserPurchases",
                "dataSrc": ""
            },
            "columns": [
                { "data": "purchaseNumber" },
                { "data": "purchaseDate" },
                { "data": "amountHT", render: function (data, type, full, meta) { return data + " DT" } },
                { "data": "amountTTC", render: function (data, type, full, meta) { return data + " DT" } },
                { "data": "state" },
            ],
            "language": {
                "decimal": "",
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
                }
            },
        });
    }
}

