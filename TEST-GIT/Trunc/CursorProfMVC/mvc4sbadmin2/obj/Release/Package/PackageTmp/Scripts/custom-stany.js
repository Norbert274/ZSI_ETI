var scroll_y; //Do zapamietywania pozycji strony przy ukrywaniu i pokazywaniu tabeli


$("body").on("click", "#btnFiltruj", function () {
    idLoading = $(this);
    LoadingStart(idLoading);
    var table = $('#stanyDataTable').DataTable();
    table.ajax.reload();
});

//Funkcja nieuzywana
jQuery("body").on("click", "#dodajDoKoszykaStany", function () {
    var csrfToken = $("input[name='__RequestVerificationToken']").val();
    var productsTable = $('#stanyDataTable').dataTable();
    var toAdd = new Array();
    var rowcollection = productsTable.$(".stany_checkbox:checked", { "page": "all" });
    rowcollection.each(function () {
        var tempIdArr = $(this).attr('id').split('_');
        toAdd.push(tempIdArr[1]);
    });

    toAdd.sort(function (a, b) { return a - b });

    if (toAdd.length < 1) {
        generateAlert('warning', 'Nie zaznaczono żadnego produktu!');
        //alert();
        return false;
    }

    

    var dataString = '';
    for (i = 0; i < toAdd.length; i++) {
        var dt = productsTable.fnGetData(toAdd[i]);
     
        var skinp2 = $.parseHTML(dt[1])[2];
      
        var sku = skinp2['value'];
        var grupa = dt[6];

        dataString += '&sku_id[' + i + ']=' + sku + '&ilosc[' + i + ']=0' + '&grupa[' + i + ']=' + grupa + "|";
        var jsonStr = JSON.stringify(dataString);
       // alert(dataString);
    }



    jQuery.ajax({
        headers: { __RequestVerificationToken: csrfToken },
        url: urlPrefix + '/api/ZamowieniaApi',
        type: "POST",
        data: jsonStr,
        contentType: 'application/json; charset=utf-8',

        success: function (results) {
            
        },
        error: function (err) {
            
        },
        complete: function (xhr, status) {
            LoadingStop()
            IsJsonRedirect(xhr);
            ShowJsonAjaxMessage(xhr);
        }

    });

});


function produktDetale(wiersz) {
    var csrfToken = $("input[name='__RequestVerificationToken']").val();

    scroll_y = $(window).scrollTop();
    idLoading = $('#stanyDataTable');
    LoadingStart(idLoading);
    var dataString = wiersz.attr('data-link');



    $.ajax({
        headers: { __RequestVerificationToken: csrfToken },
        url: urlPrefix + '/Stany/ViewProductDetails',
        type: "GET",
        data: dataString,
        success: function (msg) {

            LoadingStop();

            var modal = $("#photoShowModal").html();
            if (modal != null) {
                produktWyswietlModal(msg);
            }
            else {
                produktWyswietlMobilnaWersje(msg);
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
function produktWyswietlModal(msg) {
    var message = '';
    try {
        message = (msg.trim());
        $("#photoShowDiv").html(message);
        $("#photoShowModal").modal({ "backdrop": "static" });
        $("#photoShowModal").modal('show');

        $("#photoPrev").prop('disabled', true);
        var photosCount = parseInt($("#photo_count").val());

        if (photosCount > 1) {
            $("#photoNext").prop('disabled', false);
        }
        else { $("#photoNext").prop('disabled', true); }

        var photo = $("#photo_0");
        $("#currPhoto").attr('src', photo.attr('src'));
    }
    catch (e) {
        generateAlert('error', e.message);
        }

};
function produktWyswietlMobilnaWersje(msg) {
    var message = '';
    try {
        message = (msg.trim());
        $("#produktDetale").html(message);
        $("#photoPrev").prop('disabled', true);
        var photosCount = parseInt($("#photo_count").val());

        if (photosCount > 1) {
            $("#photoNext").prop('disabled', false);
        }
        else { $("#photoNext").prop('disabled', true); }

        var photo = $("#photo_0");
        $("#currPhoto").attr('src', photo.attr('src'));

        $("#listaProduktow").hide()
        $("#produktDetale").show();
        $('html, body').scrollTop(100);
    }
    catch (e) {
        generateAlert('error', e.message);
    }
};


jQuery("body").on("click", "#dodajJedenDoKoszykaStany", function () {
    var csrfToken = $("input[name='__RequestVerificationToken']").val();
   
    var modelData = $("#formDodajDoKoszyka").serialize();
    var $addToCartBtn = $('#dodajJedenDoKoszykaStany');

    if ($("#formDodajDoKoszyka").valid() == false) { return };

    
    $addToCartBtn.prop('disabled', true);

    LoadingStart($addToCartBtn);


    jQuery.ajax({
        headers: { __RequestVerificationToken: csrfToken },
        url: urlPrefix + '/api/ZamowieniaApi',
        type: "POST",
        data: modelData,
        success: function (results) {
            $('#photoShowModal').modal('hide');
            $("#produktDetale").hide();
            $("#listaProduktow").show();
        },
        error: function (err) {
     
        },
        complete: function (xhr, status) {
            LoadingStop()
            IsJsonRedirect(xhr);
            ShowJsonAjaxMessage(xhr);
            $addToCartBtn.prop('disabled', false);
        }

    });

   
   
   if (scroll_y != -1) {
       $('html, body').scrollTop(scroll_y);
   };
 
});
$("body").on("click", ".detail-link-return", function () {
    $("#produktDetale").hide();
    $("#listaProduktow").show();
  
    if (scroll_y != null)
    {
        $('html, body').scrollTop(scroll_y);
    }
});
$("#photoShowModal").on("show", function () {
    $("body").addClass("modal-open");
}).on("hidden", function () {
    $("body").removeClass("modal-open")
});


$('#photoShowModal').on('hidden.bs.modal', function () {
    if (scroll_y != null) {
        $('html, body').scrollTop(scroll_y);
    }
});


$("body").on('click', '.spinQty', function () {
    var idWiersza = $(this).attr('value');

    //ID inputow
    var sposobPakowaniaCol = "#Sposob_pakowania";
    var iloscDostepna = "#product_dostepne";
    var iloscZamawiana = "#produkt_ILOSC";

    //Odczytywanie wartości pól
    var ilosc = $(sposobPakowaniaCol).attr('value'); //Gdyby w przyszlosci bylo to pole
    var max = $(iloscDostepna).attr('value'); //Nie pozwoli dodac wiecej niz dostepne
    var spinner = $(iloscZamawiana);

    if (isNaN(ilosc) == true) {
        ilosc = 1;
    }

    if ($(this).hasClass('up') == true) {
        ilosc = parseInt(spinner.val(), 10) + parseInt(ilosc)
    }
    else if ($(this).hasClass('down') == true) {
        ilosc = parseInt(spinner.val(), 10) - parseInt(ilosc);
    }
    if (ilosc < parseFloat(0)) {
        ilosc = 0;
    };
    if (ilosc > parseFloat(max)) {
        ilosc = max;
    };

    spinner.val(ilosc);
    spinner.attr('value', ilosc);
});


$('#stanyDataTable').on('column-visibility.dt', function (e, settings, column, state) {
    var table = $('#stanyDataTable').DataTable();
    var col = table.column(column)
        .nodes()
        .to$(); // Convert to a jQuery object

    if (col.hasClass('detale') == true && state == false) {
        $('.detale_zdjecie').show();
    }
    else
    if (col.hasClass('detale') == true && state == true) {
        $('.detale_zdjecie').hide()
        //table.responsive.recalc();
    }
   

});


$('#stanyDataTable').on('draw.dt', function (settings) {
    var t = $('#stanyDataTable').DataTable();

t.columns().every(function () {
    var col = this.nodes().to$();
    var state = this.visible();

    if (col.hasClass('detale') == true && state == false) {
        $('.detale_zdjecie').show();
    }
    else
        if (col.hasClass('detale') == true && state == true) {
            $('.detale_zdjecie').hide()
         //   table.responsive.recalc();
        }




});
});


$(document).ready(function () {

    $('#grupyFiltry').children('.panel-body').prepend("<div class='checkbox'>"
        + "<label><input type='checkbox'  id='selectAllGroups' checked='true'>Zaznacz wszystkie</label></div></br>");

    $('#selectAllGroups').click(function () {

        if ($('#selectAllGroups').is(':checked') == true) {
            $('.chkfiltryGrup').prop('checked', true);
        }
        else {
            $('.chkfiltryGrup').prop('checked', false);
        }
    });


    $('#markiFiltry').children('.panel-body').prepend("<div class='checkbox'>"
        + "<label><input type='checkbox'  id='selectAllMarki' checked='true'>Zaznacz wszystkie</label></div></br>");

    $('#selectAllMarki').click(function () {

        if ($('#selectAllMarki').is(':checked') == true) {
            $('.chkfiltryMarek').prop('checked', true);
        }
        else {
            $('.chkfiltryMarek').prop('checked', false);
        }
    });

});
