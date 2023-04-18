var scroll_y; //Do zapamietywania pozycji strony przy ukrywaniu i pokazywaniu tabeli
var daneZmienione = 0;

function getHiddenData() {
    $('#divInvisible').html('');

    var tbl = $('#awizoPozycje').dataTable();
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
function onAwizoZapisSuccess(msg) {
    try {
        message = msg;
        if (message.status == "0")
        {
            $('body #awizo_AWIZO_ID').val(message.rekord_id);
            $('body #awizo_id').html(message.rekord_id);

        }
    }
    catch (e) {
        generateAlert('error', e.message);
    }
    $('body #btnAwizoSubmit').attr('disabled', true);
    daneZmienione = 0;

};

function onAwizoZapisFailure(err) {

    AwizoDaneZmienione();
};
function onAwizoZapisComplete(xhr) {
    ShowJsonAjaxMessage(xhr);
    IsJsonRedirect(xhr);
    LoadingStop();
    var t = $('#awizoDataTable').DataTable();
    t.ajax.reload();
    checkIfSaved()
    };
function checkIfSaved() {
    if (daneZmienione != 0) {
       
        window.scrollTo(0, 0);
        $("#btnAwizoRealizuj").attr('disabled', true);
        return false;

    }
    $("#btnAwizoRealizuj").attr('disabled', false);
    return true;

};
function porwocWersjaMobilna() {

    if (checkIfSaved() == true) {
        var blokada_id = $('#awizoDetale #blokada_id').attr('value');

        if (blokada_id != null && blokada_id > 0)
        { zdejmijBlokade(blokada_id); }

        $("#awizoDetale").hide();
        $("#listaAwiz").show();


        var t = $('#awizoDataTable').DataTable();
        t.responsive.recalc();

        if (scroll_y != null) {
            $('html, body').scrollTop(scroll_y);
        }
    }
else
{
$('#alertNiezapisaneZmiany').show();
}
};
function AwizoDaneZmienione()
{
    $('body #btnAwizoSubmit').attr('disabled', false);
    $("#btnAwizoRealizuj").attr('disabled', true);
    daneZmienione = 1;
}
function onAwizoDostawcaZapisSuccess(msg) {
    try {
        var message = '';
        message = (msg.trim());
        $("#divDostawca").html(message);
        }
    catch (e) {

        generateAlert('error', e.message);

    }
  };

$("#awizoDetale").on("change", ".sprawdzane", function () {
    AwizoDaneZmienione();
});
$(window).bind('beforeunload', function () {
    if (daneZmienione != 0) {
        window.scrollTo(0, 0);
        return "Chciałeś opuścić stronę bez zapisywania zmian. Czy chcesz opuścić stronę? (Wprowadzone miany zostaną utracone)";
    }
});

$("body").on("click", "#btnDodajSKU", function () {
    $('html, body').animate({
        scrollTop: $("#awizoProduktyListaDataTable").offset().top
    }, 1000);

});
$("body").on("click", "#btnFiltruj", function () {
    idLoading = $(this);
    LoadingStart(idLoading);
    var table = $('#awizoDataTable').DataTable();
    table.ajax.reload();
});
$("body").on("click", "#addMobileAwizo", function () {
    var csrfToken = $("input[name='__RequestVerificationToken']").val();
    scroll_y = $(window).scrollTop(); //Zapamietanie pozycji zeby wrocic w dobre miejsce
    idLoading = $(this);
    LoadingStart(idLoading);

    var dataString = $(this).attr('data-awizoid');

    $.ajax({
        headers: { __RequestVerificationToken: csrfToken },
        url: urlPrefix + '/Awizo/Edit',
        type: "GET",
        data: dataString,
        success: function (msg) {
            var message = '';
            try {
                message = (msg.trim());
                $("#listaAwiz").hide()
                $("#awizoDetale").html(message);

                if ($.validator.unobtrusive != undefined) {
                    $.validator.unobtrusive.parse($("#awizoDetale"));
                }
                $("#awizoDetale").show();
                $('html, body').scrollTop(100);
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
$("body").on("click", "#btnDostawcaEdit", function () {
    $("#btnDostawcaSubmit").attr("disabled", false);
    $("#btnDostawcaDelete").attr("disabled", false);
    $(".dostawcaEdit").attr("disabled", false);
    
});
$("body").on("click", "#btnDostawcaNew", function () {
    $("#btnDostawcaSubmit").attr("disabled", false);
    $(".dostawcaEdit").attr("disabled", false);
    $("#formDostawca #dostawca_DOSTAWCA_ID").val(0);
    $(".dostawcaEdit").text("");
    $(".dostawcaEdit").val("");
});
$("body").on("click", "#btnDostawcaDelete", function () {
   
    scroll_y = $(window).scrollTop(); //Zapamietanie pozycji zeby wrocic w dobre miejsce
    idLoading = $(this);


    var dostawca_id = $("#dostawca_DOSTAWCA_ID").val();
   
    if (dostawca_id == null || dostawca_id == 0) {
        return;
    }

    LoadingStart(idLoading);

    var csrfToken = $("#formDostawca input[name='__RequestVerificationToken']").val();
    var dataString = "awizo_DOSTAWCA_ID=" + dostawca_id;
    
    $.ajax({
        headers: { __RequestVerificationToken: csrfToken },
        url: urlPrefix + '/Awizo/DostawcaDelete',
        type: "POST",
        data: dataString,
        success: function (msg) {
            var message = '';
            try {
                message = (msg.trim());
                $("#divDostawca").html(message);

            } catch (e) {
                generateAlert('error', e.description);
            }
            
        },
        error: function (err) {
            ShowJsonAjaxMessage(err);
        },
        complete: function (xhr) {
            IsJsonRedirect(xhr);
            LoadingStop();
        }


    });
});
$("body").on("click", '#btnAwizoSubmit', function () {
    if (getHiddenData() == true) {
        $('#formAwizo').submit();
        daneZmienione = 1;
    }
});
$("body").on("click", '#btnAwizoRealizuj', function () {
    if (checkIfSaved() == true) {
        $('#formAwizoRealizuj').submit();
    }
else
{
$('#alertNiezapisaneZmiany').show(); }
});
$("body").on("change", "#dostawca_DOSTAWCA_ID", function () {
    var id = $(this).attr("id");
    if (id == null)
    { return; };

    var selectBanks = $(this);
    var selectedOptionId = selectBanks.find("option:selected").val();

    if (selectedOptionId == null || selectedOptionId == 0) {
        return;
    }
    var csrfToken = $("input[name='__RequestVerificationToken']").val();
    scroll_y = $(window).scrollTop(); //Zapamietanie pozycji zeby wrocic w dobre miejsce

    $("#awizo_DOSTAWCA_ID").val(selectedOptionId)

    var dataString = "awizo_DOSTAWCA_ID=" + selectedOptionId;

    idLoading = $(this);
    LoadingStart(idLoading);

    $.ajax({
        headers: { __RequestVerificationToken: csrfToken },
        url: urlPrefix + '/Awizo/DostawcaSzczegoly',
        type: "GET",
        data: dataString,
        success: function (msg) {
            var message = '';

            try {
                message = (msg.trim());
                $("#divDostawca").html(message);
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
        }


    });

    daneZmienione = 1;
});

$("body").on("click", '.add-me', function () {
    AwizoDaneZmienione();

    
    var sku = $(this).attr('data-sku');
    var skuid = $(this).attr('data-skuid');
    var nazwa = $(this).attr('data-nazwa');
    var opis = sku + '<br />' + nazwa + '<br /><a class="text-danger delete-me"><i alt="Usuń" class="fa fa-trash-o fa-2x "></i></a>'
    var indexPozycji = "awizoPozycje_" + skuid + "_SKU";

    var dropDownGrupa = $('#grupyDropDown').html().replace('name="none"', 'name=awizoPozycje[' + indexPozycji + '].GRUPA_ID');
    var usunButton = '<a class="text-danger delete-me"><i alt="Usuń" class="fa fa-trash-o fa-2x "></i></a>'

    var ilosc = '<div class="form-group"><input value="0" name="awizoPozycje[' + indexPozycji + '].ILOSC" type="number" class="wartoscSum sprawdzane form-control ilosc-select" style="min-width:70px;margin-bottom: 15px" min=0 />';
    ilosc = ilosc + '<input type="hidden" value="' + skuid + '" name="awizoPozycje[' + indexPozycji + '].SKU" />';
    ilosc = ilosc + '<input type="hidden" name="awizoPozycje.Index" value="' + indexPozycji + '" />';
    ilosc = ilosc + '<input type="hidden" value="' + nazwa + '" name="awizoPozycje[' + indexPozycji + '].NAZWA" /></div>';

    var currentPos = $(window).scrollTop();
    var przed = $('#awizoPozycje').height();

    var table = $('#awizoPozycje').DataTable();
    table.row.add(
        ["", opis, sku, nazwa, dropDownGrupa + ilosc, usunButton]
        ).draw();

    var po = $('#awizoPozycje').height();
    currentPos = currentPos + (po - przed);
    $(window).scrollTop(currentPos);

    generateAlert('success', 'Dodano: ' + sku);

});
$("body").on("click", ".delete-me", function () {
    AwizoDaneZmienione();

    var table = $('#awizoPozycje').DataTable();
    var item = $(this).parents('tr');

    if (item.hasClass('child') == true) {
        var item2 = item.prev();
        table.row(item2).remove();
    }

    table.row(item)
        .remove()
        .draw();;
});
$("body").on("click", ".scroll-top", function () {
    $('html, body').animate({
        scrollTop: $("#awizoPozycje").offset().top
    }, 1000);

});
$("body").on("click", ".show-me-link", function () {
    var csrfToken = $("input[name='__RequestVerificationToken']").val();
    scroll_y = $(window).scrollTop(); //Zapamietanie pozycji zeby wrocic w dobre miejsce
    idLoading = $(this);
    LoadingStart(idLoading);

    var dataString = $(this).attr('data-awizoid');

    $.ajax({
        headers: { __RequestVerificationToken: csrfToken },
        url: urlPrefix + '/Awizo/Preview',
        type: "GET",
        cache: false,
        data: dataString,
        success: function (msg) {
            var message = '';

            try {
                message = (msg.trim());
                $("#listaAwiz").hide()
                $("#awizoDetale").html(message);

                if ($.validator.unobtrusive != undefined) {
                    $.validator.unobtrusive.parse($("#awizoDetale"));
                }
                $("#awizoDetale").show();
                $('html, body').scrollTop(100);
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
            var t = $('#awizoProduktyListaDataTable').DataTable();
            t.responsive.recalc();
        }


    });
});
$("body").on("click", ".edit-me-link", function () {
    var csrfToken = $("input[name='__RequestVerificationToken']").val();
    scroll_y = $(window).scrollTop(); //Zapamietanie pozycji zeby wrocic w dobre miejsce
    idLoading = $(this);
    LoadingStart(idLoading);

    var dataString = $(this).attr('data-awizoid');

    $.ajax({
        headers: { __RequestVerificationToken: csrfToken },
        url: urlPrefix + '/Awizo/edit',
        type: "GET",
        cache: false,
        data: dataString,
        success: function (msg) {
            var message = '';

            try {
                message = (msg.trim());
                $("#listaAwiz").hide()
                $("#awizoDetale").html(message);

                if ($.validator.unobtrusive != undefined) {
                    $.validator.unobtrusive.parse($("#awizoDetale"));
                }
                $("#awizoDetale").show();
                $('html, body').scrollTop(100);
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
        }
    });
});
$("body").on("click", '.zapomnijZmiany', function () {
    daneZmienione = 0;
    $('#alertNiezapisaneZmiany').hide();

});
$("body").on("click", ".detail-link-return", function () {
    porwocWersjaMobilna();
});



