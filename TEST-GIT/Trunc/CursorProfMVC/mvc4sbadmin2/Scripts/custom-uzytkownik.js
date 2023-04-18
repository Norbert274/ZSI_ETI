// Uzytkownik
var scroll_y; //Do zapamietywania pozycji strony przy ukrywaniu i pokazywaniu tabeli


$(function () {
    $("form").validate();
});



$("body").on("click", "#btnNotyfikacjeEdit", function () {
     
    var csrfToken = $("input[name='__RequestVerificationToken']").val();
//    var dataString = $(this).attr('data-adresid');;


    $.ajax({
        headers: { __RequestVerificationToken: csrfToken },
        url: urlPrefix + '/Uzytkownik/Notyfikacje', //url: link + "koszyk?action=load_stany",
        type: "GET",
  //      data: dataString,
        cache: false, //Inaczej po edycji dane nie byly brane z serwera a z cache na dysku
        success: function (msg) {
            var message = '';
            try {
                message = (msg.trim());
                LoadingStop();

                WyswietlMobilnaWersje(msg);

            }
            catch (e) {
                var trescKomunikatu = 'Exception: ' + e.message;
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

function WyswietlMobilnaWersje(msg) {
    var message = '';
    try {
        message = (msg.trim());
        $("#divDetale").html(message);

        $("#divUzytkownik").hide()
        $("#divDetale").show();
        $('html, body').scrollTop(100);
    }
    catch (e) {
        generateAlert('error', e.message);
    }
};

function porwocWersjaMobilna() {
   
    $("#divDetale").hide();
    $("#divUzytkownik").show();

    if (scroll_y != null) {
        $('html, body').scrollTop(scroll_y);
    }


}


$("body").on("click", ".detail-link-return", function () {
    porwocWersjaMobilna();
});

function onNotyfikacjeCompleted(xhr) {
    IsJsonRedirect(xhr);
    ShowJsonAjaxMessage(xhr);

    porwocWersjaMobilna();


};
function onNotyfikacjeFailure(err) {

};


function getCheckedValues(user_id)
{
    var checkboxy = $(".notyfikacjeClass");
    var dane = '';

    checkboxy.each(function ()
    {
        var zaznaczony = this.checked;

        dane = dane + '&' + user_id + '&notyfikacja="' + this.name + '"&wlacz=' + zaznaczony + '|';

    }
    );

}