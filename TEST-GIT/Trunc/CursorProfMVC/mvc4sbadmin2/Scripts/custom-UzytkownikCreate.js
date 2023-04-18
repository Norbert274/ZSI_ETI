//custom-UzytkownikCreate.js


//Stronnicowanie Start
$("#strona1next").click(function () {

    if ($(".strona1").valid()) {
        $("#strona1").addClass('hidden');
        $("#strona2").removeClass('hidden');
    }
   

});

$("#strona2back").click(function () {

    $("#strona1").removeClass('hidden');
    $("#strona2").addClass('hidden');
   
});

$("#strona2next").click(function () {

    if ($(".strona2").valid()) {
        $("#strona3").removeClass('hidden');
        $("#strona2").addClass('hidden');
    }

});

$("#strona3back").click(function () {

    $("#strona2").removeClass('hidden');
    $("#strona3").addClass('hidden');

});
//Stronnicowanie END

$(document).ready(function () {
    $('#nowyUser').validate();
});

//Ograniczenie do buttonów
$("stop").click(function (event) {
    event.preventDefault();
});

//Validator do ukrytych pól
$.validator.setDefaults({ ignore: null });

//Vallidator po utracie focusu
$('input[data-val=true]').blur(function () {
    $(this).valid();
});

//Wyłącza cofnięcie na przeglądarce
history.pushState(null, null, location.href);
window.onpopstate = function (event) {
    history.go(1);
};

//Multi checkbox zewnętrzna biblioteka 
$(document).ready(function () {
    window.asd = $('.ddlMultiSlectBox').SumoSelect({ csvDispCount: 4 });
});


//Wypełnienie nazwy polami Imie , Nazwisko
$("#Imie, #Nazwisko").keyup(function () {
    update();
});

function update() {
    $("#Nazwa").val($('#Imie').val() + " " + $('#Nazwisko').val());
}