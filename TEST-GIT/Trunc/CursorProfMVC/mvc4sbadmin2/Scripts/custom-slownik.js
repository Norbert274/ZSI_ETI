//custom-slownik.js





$(".rectangle").click(function (e) {
    if ($(e.target).is('.Nzarzadzaj')) {
        e.preventDefault();
        return;
    }
    $(this).next(".listaSlownik").toggle();
});

$('.rectangle').css('cursor', 'pointer');


//$("i").click(function () {
//    if ($(this).hasClass('fa-angle-double-down')) {
//        $(this).removeClass('fa-angle-double-down'),
//        $(this).addClass('fa-angle-double-up')
//    }
//    else {
//        $(this).removeClass('fa-angle-double-up'),
//        $(this).addClass('fa-angle-double-down')
//    }
   
//});


$(".usun").click(function () {
    if ($(this).parent().next(".usun").hasClass("hidden")) {
        $(this).parent().next(".usun").removeClass("hidden")
    }
    else {
        $(this).parent().next(".usun").addClass("hidden")
    }
});

$(".nieBtn").click(function() {
    $(this).parent().parent().parent().addClass("hidden");
});

$(".nowyBtn").click(function () {
    if ($(".nowy").hasClass("hidden")) {
        $(".nowy").removeClass("hidden")
    }
    else {
        $(".nowy").addClass("hidden")
    }
});


$(".edytuj").click(function () {
    if ($(this).parent().next().next(".edytuj").hasClass("hidden")) {
        $(this).parent().next().next(".edytuj").removeClass("hidden")
    }
    else {
        $(this).parent().next().next(".edytuj").addClass("hidden")
    }
});


$(document).ready(function () {

    var msg = document.getElementById('message').value;
    var mode = document.getElementById('mode').value.toLowerCase();

    if (msg) {
        generateAlert(mode, msg);
    }
    
});



