//custom-limitUzytkownika.js



$(".checkboxAdresaci").click(function () {
    var labelText = $(this).siblings(".lText").val();
    labelText = labelText.replace(/\s+/g, '');
    // DEBUG  alert(labelText);
    //$(".radioTabela").prop('checked', true);
    if ($(this).children("span").hasClass("fa-circle-o")) {
        $("." + labelText).prop('checked', true);
    }
    else {
        $("." + labelText).prop('checked', false);
    }
 
});

$(".textFix").click(function () {
    $(this).parent().children(".checkboxAdresaci").click();
    return false;
});


$(document).ready(function () {

    var msg = document.getElementById('message').value;
    var mode = document.getElementById('mode').value.toLowerCase();

    if (msg) {
        generateAlert(mode, msg);
    }

});

