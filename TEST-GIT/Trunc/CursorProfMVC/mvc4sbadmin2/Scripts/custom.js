//OGOLNE
//Do pobierania wartosci filtrow
var idLoading = ''; //Do zapamietywania kontrolki na ktorej wyswietli sie spinner
var urlPrefix = ""
//var urlPrefix = "/mobile"

function IsJsonRedirect(xhr) {
    var json;
    try {
        json = $.parseJSON(xhr.responseText);
    } catch (e) {
      return
    }

    if (json != null && json.isRedirect) {
        //window.location.href = json.redirectUrl;
        window.location = json.redirectUrl;
        return false;
    }
};
function ShowJsonAjaxMessage(xhr) {
    try {
        var json = $.parseJSON(xhr.responseText);
        if (typeof json == 'object') {

            var status = json.status;
            var message = json.Message;

            if (status == 0) {
                generateAlert('success', message);
            }
            else if (status == null) {
                generateAlert('error', 'Nie otrzymano informacji od serwera!');
            }
            else if (status == 1) { generateAlert('warning', message); }
            else if (status == -1) { generateAlert('error', message); }
        }

    } catch (e) {
        //generateAlert('error', xhr);
    }
}

//Do pobierania wartosci filtrow
function przygotujWartosciWybrane(klasaCheckBoxow) {
    if (klasaCheckBoxow == null) return "";
    var Checkboxes = $(klasaCheckBoxow);

    if (Checkboxes == null) return "";

    var wybrane = "";
    Checkboxes.each(function () {
        var tempIdArr = "'" + $(this).attr("value") + "'";
        if (wybrane.length == 0) {
            wybrane = tempIdArr;
        }
        else {
            wybrane = wybrane + ", " + tempIdArr;
        }
    });

    if (Checkboxes.length == 0) {
        wybrane = "";
    }

    return wybrane;

};

//Do wyswietlania znikajacych alertow
function generateAlert(type, tekst) {
    var n = noty({
        text: tekst,
        type: type,
        dismissQueue: true,
        timeout: 5000,
        closeWith: ['click', 'backdrop'],
        layout: 'center',
        theme: 'bootstrapTheme',
        maxVisible: 10
    });

};

//Do sprawdzenia - teoretycznie nie pozwoli na podwojny submit...
var tryNumber = 0;

jQuery('input[type=submit]').click(function (event) {
    var self = $(this);

    if (self.closest('form').valid()) {
        if (tryNumber > 0) {
            tryNumber++;
            alert('Operacja jest w toku. Poczekaj na jej ukończenie');
            return false;
        }
        else {
            tryNumber++;
        }
    };
});

//Łatka dla iOS zapobiegajaca edycji pol readonly...

    $('input[readonly]').on('touchstart', function (ev) {
        return false;
    });


//Łatki zaznaczania ilości...
$(".ilosc-select").focus(function () { this.select(); }); // fix for CHROME 
$(".ilosc-select").mouseup(function (e) { e.preventDefault(); });

$('body').on('click', '.ilosc-select', function () {
    $(this).select(); 
    $(this).selectionStart = 0; //Dla IPhone
    $(this).selectionEnd = this.value.length;
   
});


//Initialise any date pickers
//Tworzy datepicker tylko dla urządzeń niedotykowych
if (Modernizr.touch) {

}
else {


    if (navigator.userAgent.toLowerCase().indexOf('firefox') < 0) {

        console.log('datepicker');
        $('input[type=date]').datepicker({ format: 'yyyy-mm-dd', language: 'pl', autoclose: false })
       // $('body').on('click', 'input[type=date]', function () {
           
       //})
    }
   

}

//Zamknięcie datepickera po  kliknięciu w active day
$(document).on('click', '.active.day', function () {
    $('.datepicker').hide();
})

//Do wyświetlania spinnera
function LoadingStart(ctrl)
{
    //idLoading = $(this);
    idLoading = ctrl;
    if (idLoading != '') {
        $(idLoading).addClass('spinner');
    };
}

function LoadingStop()
{
    if (idLoading != '') {
        $(idLoading).removeClass('spinner');
    }
}


function onAjaxComplete(xhr, status) {
    LoadingStop()
    IsJsonRedirect(xhr);
    ShowJsonAjaxMessage(xhr);
};


$(function () {
    $('.input-group-addon.beautiful').each(function () {

        var $widget = $(this),
            $input = $widget.find('input'),
            type = $input.attr('type');
        settings = {
            checkbox: {
                on: { icon: 'fa fa-check-circle-o' },
                off: { icon: 'fa fa-circle-o' }
            },
            radio: {
                on: { icon: 'fa fa-dot-circle-o' },
                off: { icon: 'fa fa-circle-o' }
            }
        };

        $widget.prepend('<span class="' + settings[type].off.icon + '"></span>');

        $widget.on('click', function () {
            $input.prop('checked', !$input.is(':checked'));
            updateDisplay();
        });

        function updateDisplay() {
            var isChecked = $input.is(':checked') ? 'on' : 'off';

            $widget.find('.fa').attr('class', settings[type][isChecked].icon);

            //Just for desplay
            isChecked = $input.is(':checked') ? 'checked' : 'not Checked';
            $widget.closest('.input-group').find('input[type="text"]').val('Input is currently ' + isChecked)
        }

        updateDisplay();
    });
});