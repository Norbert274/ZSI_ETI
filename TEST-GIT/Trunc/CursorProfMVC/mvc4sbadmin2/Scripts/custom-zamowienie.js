// ZAMOWIENIA
var scroll_y; //Do zapamietywania pozycji strony przy ukrywaniu i pokazywaniu tabeli

$('#zamowieniaDataTable tbody tr td:not(:first-child)').click(function ()
{ 
    var link = $(this).closest('tr');
    zamowienieDetale(link);
        
});


function zamowienieWyswietlMobilnaWersje(msg) {
    try {
        var message = '';
        message = (msg.trim());
        $("#zamowienieDetale").html(message);
        $("#listaZamowien").hide()
        $("#zamowienieDetale").show();
        $('html, body').scrollTop(0);
        var t = $('#zamowieniePozycje').DataTable();
        t.responsive.recalc();

    }
    catch (e) {

        generateAlert('error', e.message);

    }
};

function zamowienieWyswietlModal(msg) {
    try {
        var message = '';
        message = (msg.trim());
        $("#zamowienieShowDiv").html(message);
        $("#zamowienieShowModal").modal({ "backdrop": "static" });
        $("#zamowienieShowModal").modal('show');
        var t = $('#zamowieniePozycje').DataTable();
        t.responsive.recalc();

    }
    catch (e) {
        generateAlert('error', e.message);
    }

};

function zamowienieDetale(wiersz) {
    var csrfToken = $("input[name='__RequestVerificationToken']").val();
    scroll_y = $(window).scrollTop(); //Zapamietanie pozycji zeby wrocic w dobre miejsce
    idLoading = $('#zamowieniaDataTable');
    LoadingStart(idLoading);

    var dataString = wiersz.attr('data-link');
   
    $.ajax({
        headers: { __RequestVerificationToken: csrfToken },
        url: urlPrefix + '/Zamowienie/ViewZamDetails',
        type: "GET",
        data: dataString,
        cache: false,
        success: function (msg) {
            
            LoadingStop();

            var modal = $("#zamowienieShowModal").html();
            if (modal != null) {
                zamowienieWyswietlModal(msg);
            }
            else {
                zamowienieWyswietlMobilnaWersje(msg);
            };

            
          
        },
        error: function (err) {
            ShowJsonAjaxMessage(err);
        },
        complete: function (xhr, status) {
            LoadingStop()
            IsJsonRedirect(xhr);
        }

    });
};


$("body").on("click", ".detail-link-return", function () {
    $("#zamowienieDetale").hide();
    $("#zamowienieDetale").html("");

    $("#listaZamowien").show();
    var t = $('#zamowieniaDataTable').DataTable();
    t.responsive.recalc();

    if (scroll_y != null) {
        $('html, body').scrollTop(scroll_y);
    }
});

//Przelicza szerokosc tabeli po pokazaniu okienka
$('#zamowienieShowModal').on('shown.bs.modal', function () {
  
    var t = $('#zamowieniePozycje').DataTable();
    t.responsive.recalc();
    
});
