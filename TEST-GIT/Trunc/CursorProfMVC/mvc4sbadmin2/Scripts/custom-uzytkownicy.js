//custom-uzytkownicy.js


//double click toggle the row
$('body').on('dblclick', '#uzytkownicy tbody tr', function (e) {
    var target = $(e.target);
    if (!target.is('.details-control')) {
        $(this).find('.details-control').first().trigger('click')
    }
});




//set pointer on hover
$('#uzytkownicy').css('cursor', 'pointer');





$(document).ready(function () {


    var msg = document.getElementById('message').value;
    var mode = document.getElementById('mode').value.toLowerCase();

    if (msg) {
        generateAlert(mode, msg);
    }


    var table = $('#uzytkownicy').DataTable({
        language: {
            "sProcessing": " ",
            "sLengthMenu": "Pozycji na stronę _MENU_ ",
            "sZeroRecords": "Nie znaleziono pasujących pozycji",
            "sInfoThousands": " ",
            "sInfo": "Pozycje od _START_ do _END_ z _TOTAL_ łącznie",
            "sInfoEmpty": "Pozycji 0 z 0 dostępnych",
            "sInfoFiltered": "(filtrowanie spośród _MAX_ dostępnych pozycji)",
            "sInfoPostFix": "",
            "sSearch": "Szukaj po nazwie:",
            "sUrl": "",
            "oPaginate": {
                "sFirst": '<i class="fa fa-angle-double-left fa-2x"></i>',
                "sPrevious": '<i class="fa fa-angle-left fa-2x"></i>',
                "sNext": '<i class="fa fa-angle-right fa-2x"></i>',
                "sLast": '<i class="fa fa-angle-double-right fa-2x"></i>'
            },
            "sEmptyTable": "Brak danych",
            "sLoadingRecords": " ",
            "oAria": {
                "sSortAscending": ": aktywuj, by posortować kolumnę rosnąco",
                "sSortDescending": ": aktywuj, by posortować kolumnę malejąco"
            }
        },
        "aLengthMenu": [[10, 25, 50, 75], [10, 25, 50, 75]],
        "sPaginationType": "input",
        "stateSave": true,
        "scrollCollapse": true,
        responsive: true,
        bAutoWidth: false,
        processing: true,
            ajax: { url: "http://localhost:5548/api/uzytkownikapi",
            dataSrc: ""},
        aoColumns: [
            {
                className: 'details-control',
                orderable: false,
                mDataProp: null,
                defaultContent:''
            },
            {
                mDataProp: null
               , defaultContent: "<a href='/Uzytkownik/Edit' class='editX btn btn-default btn-sm'> <i class='glyphicon glyphicon-edit'></i> </a>"
            },
            {
                mDataProp: "Uzytkownik_Id"
              , "render": function (mDataProp) {
                  return "<a href='/Uzytkownik/CreateBaseOnOtherUser/" + mDataProp + "'  class=' btn btn-default btn-sm'> <i class='glyphicon glyphicon-file'></i> </a>"
              }
            },
            {
                mDataProp: "Uzytkownik_Id"
              , "render": function (mDataProp) {
                  return "<a href='/Uzytkownik/EditTest/" + mDataProp + "'  class=' btn btn-default btn-sm'> <i class='glyphicon glyphicon-edit'></i> </a>"
              }
            },
            {
                mDataProp: "Nazwa",
                "render": function (mDataProp) {
                    return "<span class='nazwa'>" + mDataProp + "</span>";
                }
            },
            { mDataProp: "NazwaOddzialu" },
            { mDataProp: "Grupa" },
            { mDataProp: "Typ" },
            { mDataProp: "Wielkosc" },
            { mDataProp: "SiecSprzedazy" },
            { mDataProp: "Region" },
            {
                mDataProp: "Uzytkownik_Id",
                "render": function (mDataProp) {
                    // return "<a onclick = 'getConfirm(this.href," + mDataProp + ");' href='/Uzytkownik/Delete/" + mDataProp + "' class='confirm btn btn-default btn-sm'> <i class='glyphicon glyphicon-trash'></i> </a>";
                    return "<span  onclick ='usunUser(" + mDataProp + ");'  class='btnUsun btn btn-default btn-sm'> <i class='glyphicon glyphicon-trash'></i> </span>";
                }
            },
            { mDataProp: "TelefonKomorkowy" },
            { mDataProp: "rola" },
             { mDataProp: "Email" },
            { mDataProp: "Login" },
            { mDataProp: "Limit" },
            {
                mDataProp: "NotifikacjeList",
                render: function (mDataProp) {
                    var list = mDataProp;
                    if (list ==null) {
                        list = "";
                    }
                    list = list.split(',');
                    list.sort(function () { return 0.5 - Math.random() });
                    var html = '<ul>';

                    for (var i = 0; i < list.length; i++) {
                        html += '<li>' + list[i] + '</li>';
                    }

                    html += '</ul>'

                    return html;
                }
            },
            {
                mDataProp: "IsAdmin",
                render: function (mDataProp) {
                    var ret = "";
                    if (mDataProp) {
                        ret = "<i class='glyphicon glyphicon-ok'></i>";
                    }
                    else {
                        ret = "<i class='glyphicon glyphicon-remove'></i>";
                    }
                    return ret;
                }
            },
            {
                mDataProp: "IsSuperUser",
                render: function (mDataProp) {
                    var ret="";
                    if (mDataProp) {
                        ret= "<i class='glyphicon glyphicon-ok'></i>";
                    }
                    else {
                        ret = "<i class='glyphicon glyphicon-remove'></i>";
                    }
                    return ret;
                }
            }
        ]
    });
    

});

//DELETE USER
function usunUser(userId) {

    console.log('jestem');

    $.confirm({
        title: 'Usuń!',
        content: 'Czy napewno chcesz usunąć tego uzytkownika!',
        confirmButton: 'Tak',
        cancelButton: 'Nie',
        text: 'Tak',
        confirm: function () {
            window.location = "Uzytkownik/Delete/" + userId
        },
        cancel: function () {
            return;
        }
    });
};



