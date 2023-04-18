
var pobieramDane = 0;
var blockedButton = '';
var bylyZmiany = 0;
var status;
var message;

//Funckje pomocnicze
function getAdresBlokadaID(data) {

    var frmData = {};
    var adresData = {};

    for (var i = 0; i < data.length; i++) {
        var ni = data[i].name;

        if (ni == "blokada_id") {
            frmData[data[i].name] = data[i].value;
            break;
        };
    }
};
function getAdresModel(data) {

    var frmData = {};
    var adresData = {};

    for (var i = 0; i < data.length; i++) {

        var ni = data[i].name;

        switch (ni) {
            case "adres.Nazwa":
                adresData["Nazwa"] = data[i].value;
                break;
            case "adres.Ulica":
                adresData["Ulica"] = data[i].value;
                break;
            case "adres.Kod":
                adresData["Kod"] = data[i].value;
                break;
            case "adres.Miasto":
                adresData["Miasto"] = data[i].value;
                break;
            case "adres.Adres_Id":
                adresData["Adres_Id"] = data[i].value;
                break;
            case "adres.AdresTyp_Id":
                adresData["AdresTyp_Id"] = data[i].value;
                break;
            case "user_id":
            case "blokada_id":
                frmData[data[i].name] = data[i].value;
                break;
            default:
                var x = 0
                break;
        };
    };

    frmData["adres"] = adresData;

    return frmData;
};
function czyPobieraneDane(buttonID) {
    if (pobieramDane > 0) {
        pobieramDane++;

        generateAlert('warning', 'Podczas dodawania adresu wystąpił błąd: Aktualnie wykonywana jest poprzednia operacja - poczekaj');

        return false;
    }
    else {
        pobieramDane++;
        $(buttonID).attr("disabled", "disabled");
        blockedButton = blockedButton + "," + buttonID;
        setTimeout(
            function () {
                var tempId = blockedButton.split(',');
                for (i = 1; i < tempId.length; i++) {
                    $(tempId[i]).removeAttr('disabled');
                };

            }, 1500);
        return true;
    }
};
function odswiezJeliTrzeba() {
    if (bylyZmiany != 0) {
        // window.location.reload();
        var t = $('#adresyDataTable').DataTable();
        t.ajax.reload();
        bylyZmiany = 0;
    };
    var t = $('#adresyDataTable').DataTable();
    t.responsive.recalc();
};

function zdejmijBlokade(blokada_id) {
    var csrfToken = $("input[name='__RequestVerificationToken']").val();

    if (blokada_id == null || blokada_id == 0 || isNaN(blokada_id) == true) {
        return;
    }

    var dataStr = "blokada_id=" + blokada_id;
    var jsonStr = JSON.stringify(dataStr);

    $.ajax({
        headers: { __RequestVerificationToken: csrfToken },
        url: urlPrefix + '/api/ustawieniaapi',
        async: false,
        type: "POST",
        cache: false, //Inaczej po edycji dane nie byly brane z serwera a z cache na dysku
        data: jsonStr,
        contentType: 'application/json; charset=utf-8',
        success: function (results) {
        },
        error: function (xhr) {
            ShowJsonAjaxMessage(xhr);
        },
        complete: function (xhr, status) {
            LoadingStop()
            IsJsonRedirect(xhr);
        }
    });

}


//Pozwala na zmiane zachowania form submit
$(function () {
    $('form').submit(function () {
        if ($(this).valid()) {
            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize(),
                success: function (result) {
                    //generateAlert('success', 'Zapisano dane');
                },
                error: function (xhr, status, error) {
                    
                },
                complete: function (xhr, status) {
                    LoadingStop()
                    ShowJsonAjaxMessage(xhr);
                    IsJsonRedirect(xhr);
                }
            });
        }
        return false;
    });
});

//Funkcje obslugi okienek - focus i refresh
$('#AdresModal').on('shown.bs.modal', function () {
    $("#AdresModal #adres_Nazwa").focus();
});
$('#AdresModal').on('hidden.bs.modal', function () {
    var blokada_id = $('#modalBodyAdresDiv #blokada_id').attr('value');
    zdejmijBlokade(blokada_id);
    odswiezJeliTrzeba();
});

function onAdresPrepare(xhr) {

    securityToken = $('#editAdresModal [name=__RequestVerificationToken]').val();
    xhr.setRequestHeader('__RequestVerificationToken', securityToken);
    onAdresBegin()
};

$('body').on('click', '.btn-danger', LoadingStop);

function onAdresBegin() {
    LoadingStart('.btn-danger')
    $('#btnAdresSubmit').attr('disabled', true);
    $('#btnUstawieniaAdresSubmit').attr('disabled', true);
    
}

function onAdresCompleted(xhr) {
    LoadingStop();
    IsJsonRedirect(xhr);
    ShowJsonAjaxMessage(xhr);
    $('#btnAdresSubmit').attr('disabled', false);
    $('#btnUstawieniaAdresSubmit').attr('disabled', false);
    $("#AdresModal").modal('hide');
   
    odswiezJeliTrzeba();
    bylyZmiany = 1;
};
function onAdresFailure(xhr) {
    LoadingStop();
    $('#btnAdresSubmit').attr('disabled', false);
    $('#btnUstawieniaAdresSubmit').attr('disabled', false);
    ShowJsonAjaxMessage(xhr);
};
function onAdresMobileCompleted(xhr) {
    ShowJsonAjaxMessage(xhr);

    $("#adresDetale").hide()
    $("#listaAdresow").show()
    bylyZmiany = 1;
    odswiezJeliTrzeba();
    
};
//Dodawanie adresu - MODAL

$("#addUstawieniaAdres").on("click", function () {
    if (czyPobieraneDane('#addUstawieniaAdres') == false) { return false };
    var csrfToken = $("input[name='__RequestVerificationToken']").val();

    $.ajax({
        headers: { __RequestVerificationToken: csrfToken },
        url: urlPrefix + '/ustawienia/create',
        type: "GET",
        cache: false, //Inaczej po edycji dane nie byly brane z serwera a z cache na dysku
        success: function (msg) {
            pobieramDane = 0
            var message = '';
            try {
                message = (msg.trim());
                $("#modalBodyAdresDiv").html(message);

                if ($.validator.unobtrusive != undefined) {
                    $.validator.unobtrusive.parse($("#AdresModal"));
                }
                $("#AdresModal").modal({ "backdrop": "static" });
                $("#AdresModal").modal('show');
                $("#AdresModal #adres_Nazwa").focus();
            }
            catch (e) {
                var trescKomunikatu = 'Podczas dodawania adresu wystąpił błąd: ' + e.message;

                generateAlert('error', trescKomunikatu);
            }
        },
        error: function (err) {
            pobieramDane = 0
            ShowJsonAjaxMessage(err);
        },
        complete: function (xhr, status) {
            LoadingStop()
            IsJsonRedirect(xhr);
        }
    });

});

//Edycja adresu - MODAL
$("body").on("click", ".edit-me-link-modal", function () {

    if (czyPobieraneDane($(this).attr('id')) == false) { return false };

    var csrfToken = $("input[name='__RequestVerificationToken']").val();
    var dataString = $(this).attr('data-adresid');;

    // if (czyPobieraneDane($(this)) == false) { return false };
   
    $.ajax({
        headers: { __RequestVerificationToken: csrfToken },
        url: urlPrefix + '/ustawienia/edit', //url: link + "koszyk?action=load_stany",
        type: "GET",
        data: dataString,
        cache: false, //Inaczej po edycji dane nie byly brane z serwera a z cache na dysku
        success: function (msg) {
            pobieramDane = 0;
            var message = '';
            try {
                message = (msg.trim());
                $("#modalBodyAdresDiv").html(message);
                if ($.validator.unobtrusive != undefined) {
                    $.validator.unobtrusive.parse($("#modalBodyAdresDiv"));
                }
                $("#AdresModal").modal({ "backdrop": "static" });
                $("#AdresModal").modal('show');

            }
            catch (e) {
                var trescKomunikatu = 'Exception: ' + msg.trim();
                generateAlert('error', trescKomunikatu);
            }
        },

        error: function (xhr) {
            pobieramDane = 0;
            ShowJsonAjaxMessage(xhr);

        },
        complete: function (xhr, status) {
            LoadingStop()
            IsJsonRedirect(xhr);
        }

    });

});

//Delete adres
$("body").on("click", ".delete-me-link-modal", function () {
    if (czyPobieraneDane($(this).attr('id')) == false) { return false };

    var csrfToken = $("input[name='__RequestVerificationToken']").val();
    var dataString = $(this).attr('data-adresid');;
    
    $.ajax({
        headers: { __RequestVerificationToken: csrfToken },
        url: urlPrefix + '/ustawienia/delete',
        type: "GET",
        data: dataString,
        cache: false, //Inaczej po edycji dane nie byly brane z serwera a z cache na dysku
        success: function (msg) {
            pobieramDane = 0;
            var message = '';
            try {
                message = (msg.trim());
                $("#modalBodyAdresDiv").html(message);
                $("#AdresModal").modal({ "backdrop": "static" });
                $("#AdresModal").modal('show');
            }
            catch (e) {
                var trescKomunikatu = 'Exception: ' + msg.trim();
                generateAlert('error', trescKomunikatu);
            }
        },
        error: function (xhr) {
            pobieramDane = 0;
            ShowJsonAjaxMessage(xhr);
        },
        complete: function (xhr, status) {
            LoadingStop()
            IsJsonRedirect(xhr);
        }
    });

});
$("body").on("click", "#btnUstawieniaOdswiez", function () {
    var csrfToken = $("input[name='__RequestVerificationToken']").val();

    $.ajax({
        headers: { __RequestVerificationToken: csrfToken },
        url: urlPrefix + 'api/UstawieniaApi',
        type: 'GET',
        cache: false, //Inaczej po edycji dane nie byly brane z serwera a z cache na dysku
        success: function (r) {
            try {
                var json = $.parseJSON(r);
                var otable = $('#ustawieniaDataTable').dataTable(); //.DataTable();

                var oSettings = otable.fnSettings();
                var j = 0;

                otable.fnClearTable(oSettings);

                for (var i = 0; i < json.length; i++) { //r.aaData.length; i++) {
                    otable.oApi._fnAddData(oSettings, json[i]); //r.aaData[i]);
                    j++;
                }
                otable.fnDraw();
                $('#myDataTable').DataTable().responsive.recalc(); //.columns.adjust().responsive.recalc();

            }
            catch (e) {
                 var trescKomunikatu = 'Podczas odświeżania wystąpił błąd: ' + e.message;
                 generateAlert('error', trescKomunikatu);
            }

        },
        error: function (err) {
            ShowJsonAjaxMessage(err);
            //var trescKomunikatu = 'Podczas odświeżania wystąpił błąd: ' + err.Description;
            //generateAlert('error', trescKomunikatu);
        },
        complete: function (xhr, status) {
            LoadingStop()
            IsJsonRedirect(xhr);
        }
    });

});

//Dla mobilnych
$("body").on("click", "#addMobileUstawieniaAdres", function () {
    var csrfToken = $("input[name='__RequestVerificationToken']").val();
    scroll_y = $(window).scrollTop(); //Zapamietanie pozycji zeby wrocic w dobre miejsce
    idLoading = $(this);
    LoadingStart(idLoading);

    var dataString = $(this).attr('data-adresid');

    $.ajax({
        headers: { __RequestVerificationToken: csrfToken },
        url: urlPrefix + '/Ustawienia/create',
        type: "GET",
        data: dataString,
        success: function (msg) {
            var message = '';
            try {
                message = (msg.trim());
                $("#listaAdresow").hide()
                $("#adresDetale").html(message);

                if ($.validator.unobtrusive != undefined) {
                    $.validator.unobtrusive.parse($("#adresDetale"));
                }
                $("#adresDetale").show();
                $('html, body').scrollTop(100);
            }
            catch (e) {
                generateAlert('error', e.message);
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

});

$(window).unload(function () {
    var blokada_id = $('#blokada_id').attr('value');
    zdejmijBlokade(blokada_id)
});

$("body").on("click", ".delete-me-link", function () {
    var csrfToken = $("input[name='__RequestVerificationToken']").val();
    scroll_y = $(window).scrollTop(); //Zapamietanie pozycji zeby wrocic w dobre miejsce
    idLoading = $(this);
    LoadingStart(idLoading);

    var dataString = $(this).attr('data-adresid');

    $.ajax({
        headers: { __RequestVerificationToken: csrfToken },
        url: urlPrefix + '/Ustawienia/delete',
        type: "GET",
        data: dataString,
        success: function (msg) {
            var message = '';
            try {
                message = (msg.trim());
                $("#adresDetale").html(message);
                $("#listaAdresow").hide();
                $("#adresDetale").show();
                $('html, body').scrollTop(100);
            }
            catch (e) {
                generateAlert('error', e.message);
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
});
$("body").on("click", ".edit-me-link", function () {
    var csrfToken = $("input[name='__RequestVerificationToken']").val();
    scroll_y = $(window).scrollTop(); //Zapamietanie pozycji zeby wrocic w dobre miejsce
    idLoading = $(this);
    LoadingStart(idLoading);

    var dataString = $(this).attr('data-adresid');

    $.ajax({
        headers: { __RequestVerificationToken: csrfToken },
        url: urlPrefix + '/Ustawienia/edit',
        type: "GET",
        data: dataString,
        success: function (msg) {
            var message = '';
                   
            try {
                message = (msg.trim());
                $("#listaAdresow").hide()
                $("#adresDetale").html(message);

                if ($.validator.unobtrusive != undefined) {
                    $.validator.unobtrusive.parse($("#adresDetale"));
                }
                $("#adresDetale").show();
                $('html, body').scrollTop(100);
            }
            catch (e) {
                generateAlert('error', e.message);
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
});

$("body").on("click", ".detail-link-return", function () {
    porwocWersjaMobilna();
});

function porwocWersjaMobilna() {
    var blokada_id = $('#adresDetale #blokada_id').attr('value');
       
    if (blokada_id != null && blokada_id > 0)
    { zdejmijBlokade(blokada_id); }

    $("#adresDetale").hide();
    $("#listaAdresow").show();


    var t = $('#adresyDataTable').DataTable();
    t.responsive.recalc();

    if (scroll_y != null) {
        $('html, body').scrollTop(scroll_y);
    }


}


$("body").on("change", "#adres_Kod", function () {

    var kod = $("#adres_Kod").val();


    if (kod != null && $.validator.unobtrusive != undefined && kod.length == 6) {
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
                    $("#adres_Miasto").text(miasto)
                    $("#adres_Miasto").val(miasto)
                    if (miasto.length == 0) {
                        $("#adres_Kod").text("");
                        $("#adres_Kod").val("");
                        $("#adres_Kod").focus();
                        generateAlert('error', "Nieprawidłowy kod pocztowy!");
                    }


                } catch (e) {
                    generateAlert('error', msg);
                    generateAlert('error', e.message);
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

