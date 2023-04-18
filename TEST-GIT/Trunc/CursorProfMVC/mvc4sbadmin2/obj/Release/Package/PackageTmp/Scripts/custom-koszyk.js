var status = -1;


var message = "";

function checkIfSaved() {
    if ($('#daneZmienione').attr("value") == 1) {
        $('#alertNiezapisaneZmiany').show();
        window.scrollTo(0, 0);
        $("#btnZamowienieRealizuj").attr('disabled', true);
        return false;
        
    }
    $("#btnZamowienieRealizuj").attr('disabled', false);
    return true;
    
};

function getHiddenData() {
    $('#divInvisible').html('');

    var tbl = $('#zamowieniePozycje').dataTable();
    var nNodes = tbl.fnGetHiddenTrNodes()

    for (var i = 0 ; i < nNodes.length ; i++) {
        var nHidden = document.createElement('input');
        //  nHidden.type = 'hidden';
        nHidden.name = "hidden_input_" + i;
        nHidden.value = $('input', nNodes).val();

        $('#divInvisible').append('<div>' + nNodes[i].outerHTML + '</div>');
    }

    nNodes = tbl.fnGetHiddenTdNodes()
    for (var i = 0 ; i < nNodes.length ; i++) {
        var nHidden = document.createElement('input');
        //nHidden.type = 'hidden';
        nHidden.name = "hidden_input_" + i;
        nHidden.value = $('input', nNodes).val();

        $('#divInvisible').append('<div>' + nNodes[i].outerHTML + '</div>');
    }

    return true;
};

function sumujPozycjeZamowienia() {
    if ($.validator.unobtrusive != undefined) {
        $.validator.unobtrusive.parse($("#formKoszyk"));
    }

    var wartoscSum = 0;
    var koszykTable = $('#zamowieniePozycje').dataTable();
    
    for (var i = 0, len = koszykTable.fnSettings().fnRecordsTotal() ; i < len; i++) {
        var rowData = koszykTable.fnGetData(i);
        var poleID = $('#wierszid_' + i);
        if (poleID.val() != null) {
        
            var iloscPole = $('#ilosc_' + i);
            var wartoscPole = $('#koszt_punktowy_' + i);
        
        var ilosc = parseInt(iloscPole.val());
        var wartosc = $(wartoscPole).val().replace(',', '.');
        wartosc = parseFloat(wartosc);
     
        wartoscSum += parseInt(ilosc) * parseFloat(wartosc);
        if (isNaN(wartoscSum) == true) { wartoscSum = 0; }
        }
    }
    var lbl = wartoscSum.toFixed(2).toString().replace('.', ',');
    $("#wartoscLbl").html(lbl);

};


function beforeKoszykSend() {

    var $btnKoszykSubmit = $('#btnKoszykSubmit');
    
    $btnKoszykSubmit.prop('disabled', true);

    LoadingStart($btnKoszykSubmit);

}

function onKoszykZapisComplete(xhr) {
    IsJsonRedirect(xhr);
    ShowJsonAjaxMessage(xhr);

    checkIfSaved();

    $('#btnKoszykSubmit').prop('disabled', false);

    LoadingStop();
}



function PokazWersjeMobilna(htmlMessage, main, modal) {
    $(main).hide()
    $(modal).html(htmlMessage);
    $(modal).show();
    $('html, body').scrollTop(100);
};
function porwocWersjaMobilna(main, modal) {
    $(modal).hide();
    $(main).show();
};



$("body").on("click", ".deleteMe", function () {
    $("#btnZamowienieRealizuj").attr('disabled', true);
    $("#daneZmienione").attr("value", "1");
  

    var koszykTable = $('#zamowieniePozycje').DataTable();
    var item = $(this).parents('tr');
    
    if (item.hasClass('child') == true)
    {
        var item2 = item.prev();
        koszykTable.row(item2).remove();
    }

    koszykTable.row(item)
        .remove()
        .draw();;

    sumujPozycjeZamowienia();
       
});

$("#divDostawa").on("change", ".radio", function () {

    var id = $(this).attr("id");

    if (id == null)
    { return; };

    var val = $(this).attr("value");
    var dropDownToUnlock = "#sel" + id;
    var fieldAutoFocus = ".af" + id;
    $(".divSelect").hide();
       
    $(dropDownToUnlock).show();
    $(fieldAutoFocus).focus();


    $("#zamowienie_typ_zamowienia").attr("value", val);


});

$("#koszykDane").on("change", ".sprawdzane", function ()
{
    $("#btnZamowienieRealizuj").attr('disabled', true);
   $("#daneZmienione").attr("value", "1");
 });

$(window).bind('beforeunload', function () {
    if ($('#daneZmienione').attr("value") == 1) {
        window.scrollTo(0, 0);
        return "Chciałeś opuścić stronę bez zapisywania zmian. Czy chcesz opuścić stronę? (Wprowadzone miany zostaną utracone)";
    }
});

$('#btnDodajDoKoszyka').on('click', function () {
    if (checkIfSaved() == true) {
        var url = urlPrefix + '/Stany';
        window.location.href = url;
    }
});

$("body").on("click", '#btnZamowienieRealizuj', function () {
    
    if (checkIfSaved() == true) {
        LoadingStart('#formRealizuj');
            $('#formRealizuj').submit();
        }
});

$("body").on("click", "#btnKoszykSubmitLeave", submitCart)

$("body").on("click", '#btnKoszykSubmit', submitCart);

function submitCart() {

    if (getHiddenData() == true) {
        $("#daneZmienione").attr("value", "0");
        $('#formKoszyk').submit();
    }
}

$('#pozostanNaStronie').on('click', function () {
    $('#alertNiezapisaneZmiany').hide();
});

$('.zapomnijZmiany').on('click', function () {
    $("#daneZmienione").attr("value", "0");
    $('#alertNiezapisaneZmiany').hide();
});

$('.wartoscSum').on('change', function () {
sumujPozycjeZamowienia();
});


$("#divDostawa").on("change", "#zamowienie_kod", function () {

    var kod = $("#zamowienie_kod").val();


    if (kod != null && $.validator.unobtrusive != undefined && kod.length == 6)
    {
        //IF form valid

    var csrfToken = $("input[name='__RequestVerificationToken']").val();
    scroll_y = $(window).scrollTop(); //Zapamietanie pozycji zeby wrocic w dobre miejsce
    idLoading = $(this);
    LoadingStart(idLoading);

    var dataString = "kod=" + kod;

    $.ajax({
        headers: { __RequestVerificationToken: csrfToken },
        url: urlPrefix + '/Ustawienia/MiastoDlaKoduPocztowego',
        type: "GET",
        data: dataString,
        success: function (msg) {

            try {
               // var json = $.parseJSON(msg);
                var miasto = msg.Miasto;
                $("#zamowienie_miasto").text(miasto)
                $("#zamowienie_miasto").val(miasto)
                if (miasto.length == 0)
                {
                    $("#zamowienie_kod").text("");
                    $("#zamowienie_kod").val("");
                    $("#zamowienie_kod").focus();
                    generateAlert('error', "Nieprawidłowy kod pocztowy!");
                 }
                

            } catch (e) {
                generateAlert('error', msg);
                generateAlert('error', e.description);
            }
        },
        error: function (err) {
            ShowJsonAjaxMessage(err);
        },
        complete: function (xhr, status) {
            LoadingStop()
            IsJsonRedirect(xhr);
        }
    });
    }


});

$("body").on("click", "#btnSzukajAdresu", function () {
    var csrfToken = $("input[name='__RequestVerificationToken']").val();
    scroll_y = $(window).scrollTop();
    idLoading = $(this);
    LoadingStart(idLoading);
   // var dataString = $(this).attr('data-awizoid');
    $.ajax({
        headers: { __RequestVerificationToken: csrfToken },
        url: urlPrefix + '/Koszyk/AdresyZdefiniowaneUzytkownika',
        type: "GET",
      //  data: dataString,
        success: function (msg) {
            var message = '';
            try {
                message = (msg.trim());
                PokazWersjeMobilna(message, ".toHide", "#divListaAdresow")
            }
            catch (e) {
                generateAlert('error', e.message);
            }

        },
        error: function (err) {
            ShowJsonAjaxMessage(err);
        },
        complete: function (xhr) {
            IsJsonRedirect(xhr);
            LoadingStop();
        },
    });

});
$("body").on("click", ".detail-link-return", function () {
    porwocWersjaMobilna(".toHide", "#divListaAdresow");
});
$("body").on("click", ".AddMe", function () {
    var csrfToken = $("input[name='__RequestVerificationToken']").val();
    scroll_y = $(window).scrollTop();
    idLoading = $(this);
    LoadingStart(idLoading);

    var zamowienieId = $("#zamowienie_zamowienie_id").attr('value');
    var adresId = $(this).attr('data-adresid');
    var typZamowienia = $("#zamowienie_typ_zamowienia").attr('value');

    $("#btnZamowienieRealizuj").attr('disabled', true);
    $("#daneZmienione").attr("value", "1");
    var dataString = 'zamowienie_id=' + zamowienieId  + '&adres_id=' + adresId + '&typ_zamowienia='  + typZamowienia;
               
    $.ajax({
        headers: { __RequestVerificationToken: csrfToken },
        url: urlPrefix + '/Koszyk/ZamowienieAdresZdefiniowany',
        type: "GET",
        dataType: "html",
        cache: false,
        data: dataString,
        success: function (msg) {
            var message = '';
            try {
                message = (msg.trim());
                
                $("#divAdresyZdefiniowane").html(message);
                porwocWersjaMobilna(".toHide", "#divListaAdresow");
                
            }
            catch (e) {
                generateAlert('error', e.message);
            }

        },
        error: function (err) {
            ShowJsonAjaxMessage(err);
        },
        complete: function (xhr) {
            IsJsonRedirect(xhr);
            LoadingStop();
        },
    });

});




