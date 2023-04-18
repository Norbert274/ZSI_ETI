
//$("img").hover(function () {
//    $(this).animate({
//        width: "80%",
//        height: "80%"
//    }), $(this).parents('div').first().find("div").removeClass('hidden');
//}
//    , function () {
//        $(this).animate({
//            width: "50%",
//            height: "50%"
//        }), $(".wiadomoscDetal").addClass('hidden');
//    });


$(".rectangle").click(function () {
    $(this).children(".infoText").toggle();
});


$('.rectangle').css('cursor', 'pointer');


$(document).ready(function () {

    var msg = document.getElementById('message').value;
    var mode = document.getElementById('mode').value.toLowerCase();

    if (msg) {
        generateAlert(mode, msg);
    }

});