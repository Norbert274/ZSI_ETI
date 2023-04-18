var scroll_y; //Do zapamietywania pozycji strony przy ukrywaniu i pokazywaniu tabeli
var daneZmienione = 0;


$("body").on("click", '#btnKontaktITMailWyslij', function () {
    $(this).attr('disabled', 'disabled');
   
});
  
$("body").on("click", '#btnKontaktOpiekunMailWyslij', function () {
    $(this).attr('disabled', 'disabled');
   
});

$("body").on("click", '#btnKontaktKlientMailWyslij', function () {
    $(this).attr('disabled', 'disabled');

});
