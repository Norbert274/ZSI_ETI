//custom-adresaci.js


$(".checkboxAdresaci").click(function () {
    var grupaId = $(this).siblings(".paramGrupa").text();
    var wielkoscId = $(this).siblings(".paramWielkosc").text();
    var typId = $(this).siblings(".paramTyp").text();

    if ($(this).children("span").hasClass("fa-circle-o")) {
        
        $(".grupaId-" + grupaId).children(".radioTabela").prop('checked', true);
        $(".wielkoscId-" + wielkoscId).children(".radioTabela").prop('checked', true);
        $(".typId-" + typId).children(".radioTabela").prop('checked', true);
    }
    else {

        $(".grupaId-" + grupaId).children(".radioTabela").prop('checked', false);
        $(".wielkoscId-" + wielkoscId).children(".radioTabela").prop('checked', false);
        $(".typId-" + typId).children(".radioTabela").prop('checked', false);
    };
});




$(".XdodajAdresatow").click(function () {
    $(".radioTabela").each(function () {
        if ($(this).is(':checked')) {
            alert($(this).parent().siblings('.nazwisko').text());
        }
    });
});

function getNames() {
    var nazwiska=[];
    $(".dodajAdresatow").click(function () {
        $(".radioTabela").each(function () {
            if ($(this).is(':checked')) {
                nazwiska.add($(this).parent().siblings('.nazwisko').text());
            }
        });
    });
    return nazwiska;
}


   

$(".textFix").click(function () {
    $(this).parent().children(".checkboxAdresaci").click();
    return false;
});


