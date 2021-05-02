$(document).ready(function () {
    $.post('/Document/DocumentNb', function (response) {
        $('#DocumentsNumber').text(response)
    })

    $.post('/User/UserNb', function (response) {
        $('#UsersNumber').text(response)
    })

    $.post('/Ebook/EbookNb', function (response) {
        $('#BooksNumber').text(response)
    })

    //$.post('/Ejournal/EjournalNb', function (response) {
    //    $('#EjournalsNumber').text(response)
    //})

    $.post('/Purchase/PurchaseNb', function (response) {
        $('#PurchasesNumber').text(response)
    })

    //$.post('/PurchaseLine/PurchaseLineNb', function (response) {
    //    $('#PurchaseLineNumber').text(response)
    //})

});

