$(document).ready(function () {
    //var datepicker = $.fn.datepicker.noConflict(); // return $.fn.datepicker to previously assigned value
    //$.fn.bootstrapDP = datepicker;                 // give $().bootstrapDP the bootstrap-datepicker functionality
});
var eventDates = {};

$('#infobox').draggable();
$(function () {
    var table = $('#listeday').DataTable({
        responsive: true,
        select: true,
        dom: 'lftipB',

        buttons: [
            'copy', 'csv', 'excel', 'print'
        ],
        initComplete: function () {
            var btns = $('.dt-button');
            btns.addClass('btn btn-primary zbtn');
            //btns.removeClass('dt-button');

        }
    });
    $('#listeday tbody').on('click', 'tr', function () {
        $(this).toggleClass('selected');
    });

    $('#button').click(function () {
        alert(table.rows('.selected').data().length + ' row(s) selected');
    });
    $.datepicker.regional['fr'] = {
        clearText: 'Effacer', clearStatus: '',
        closeText: 'Fermer', closeStatus: 'Fermer sans modifier',
        prevText: '&lt;Préc', prevStatus: 'Voir le mois précédent',
        nextText: 'Suiv&gt;', nextStatus: 'Voir le mois suivant',
        currentText: 'Courant', currentStatus: 'Voir le mois courant',
        monthNames: ['Janvier', 'Février', 'Mars', 'Avril', 'Mai', 'Juin',
            'Juillet', 'Août', 'Septembre', 'Octobre', 'Novembre', 'Décembre'],
        monthNamesShort: ['Jan', 'Fév', 'Mar', 'Avr', 'Mai', 'Jun',
            'Jul', 'Aoû', 'Sep', 'Oct', 'Nov', 'Déc'],
        monthStatus: 'Voir un autre mois', yearStatus: 'Voir un autre année',
        weekHeader: 'Sm', weekStatus: '',
        dayNames: ['Dimanche', 'Lundi', 'Mardi', 'Mercredi', 'Jeudi', 'Vendredi', 'Samedi'],
        dayNamesShort: ['Dim', 'Lun', 'Mar', 'Mer', 'Jeu', 'Ven', 'Sam'],
        dayNamesMin: ['Di', 'Lu', 'Ma', 'Me', 'Je', 'Ve', 'Sa'],
        dayStatus: 'Utiliser DD comme premier jour de la semaine', dateStatus: 'Choisir le DD, MM d',
        dateFormat: 'dd/mm/yy', firstDay: 0,
        initStatus: 'Choisir la date', isRTL: false
    };
    $.datepicker.setDefaults($.datepicker.regional['fr']);

    //eventDates[ new Date( '03/03/2020' )] = new Date( '03/03/2020' );
    var json = '@Json.Serialize(Model.Item1.Event.Where(s => s.User.Contains(User.Identity.Name)))';
    console.log(json);
    var list = JSON.parse(json);
    console.log(list);
    for (i = 0; i < list.length; i += 1) {
        var toeventdate = new Date(list[i].date);
        var hourdate = new Date(list[i].heures)
        if (eventDates[toeventdate])
            eventDates[toeventdate] += hourdate.getHours();
        else
            eventDates[toeventdate] = hourdate.getHours();

        console.log("heures = " + eventDates[toeventdate.toString()]);
    }

    console.log(eventDates);
    $('#datepicker').datepicker({
        uiLibrary: 'bootstrap4',
        beforeShowDay: function (date) {
            // exclude weekends
            if (!$.datepicker.noWeekends(date)[0])
                return [false, '', ''];

            if (eventDates[date] && eventDates[date] >= 8) {
                return [true, 'eventvalid', 'nb h total >= 8'];
            }
            return [true, eventDates[date] != undefined ? 'event' : '', 'nb h total < 8'];
        },
        firstDay: 1,
        autoSize: true,
        onSelect: function (dateText) {
            $('#datepicker2').datepicker("setDate", $(this).datepicker("getDate"));
            $('#datepicker3').datepicker("setDate", $(this).datepicker("getDate"));
            is_valid_month();
            //$('#datepicker2').val($(this).datepicker("getDate"));
            //document.getElementById('datepicker2').value = $(this).datepicker("getDate");
            $.ajax({
                type: "GET",
                url: "@Url.Action("getlistbydate")",
                data: { datee: dateText },
                dataType: "json",
                success: function (list) {
                    console.log(list)
                    table.clear().draw();
                    var hourday = 0;
                    //  var option = "";

                    // option = "<a href=\"/Tempo/Edit/" + list[i].id + "\">Edit</a> | <a href=\"/Tempo/Details/" + list[i].id + "\">Details</a> | <a href=\"/Tempo/Delete/" + list[i].id + "\">Delete</a>";

                    for (i = 0; i < list.length; i += 1) {
                        var dt = new Date(list[i].heures);
                        table.row.add([
                            "<button id=\"copier\"  onclick=\"copier('" + list[i].id + "')\" class=\"btn btn-primary zbtn\" style=\"margin: 12px\">Copier</button>",
                            list[i].type,
                            list[i].classe_str,
                            list[i].classe2_str,
                            list[i].nature,
                            dt.getHours(),
                            "<a href=\"/Tempo/Edit/" + list[i].id + "\">Edit</a> | <a href=\"" + "@this.Url.Action("Details","Tempo")" + "/" + list[i].id + "\">Details</a> | <a href=\"" + "@this.Url.Action("Delete","Tempo")" + "/" + list[i].id + "\">Delete</a>",
                        ]).draw();
                        hourday += dt.getHours();
                    }
                    console.log("test = " + hourday);
                    document.getElementById('hourday').innerHTML = "Total des heures du jour :  " + hourday;
                    document.getElementById("iframcreat").src = "@this.Url.Action("Create","Tempo")" + "?datepicker2=" + dateText;

                },
                error: function (req, status, error) {
                    document.getElementById('creat_button').hidden = true;
                    alert("date note valide recharge page or call a administrator");
                }
            });

        }
    });
    $("#datepicker").datepicker("setDate", new Date());
    //$('.ui-widget.ui-widget-content').removeProperty("border");

});
$(function () {
    $("#dialog").dialog({
        width: 'auto', // overcomes width:'auto' and maxWidth bug
        maxWidth: 650,
        height: 'auto',
        autoOpen: false,
        autoResize: true,
        autoReposition: true,
        modal: true,
        resizable: true,
        draggable: true,
        create: function (event, ui) {
            var widget = $(this).dialog("widget");
            $(".ui-dialog   ", widget)
                .removeClass(".ui-dialog-titlebar-close")
                .addClass("close");
        },
        open: function (event, ui) {
            $('#dialog').css('overflow', 'hidden'); //this line does the actual hiding
            //$(this).parent().css('position', 'fixed');
            $("#dialog").prev('.ui-dialog-titlebar').hide();
        }
    });

});
$(function () {
    $("#createevent").attr('class', 'btn btn-primary zbtn');
    $("#quitevent").attr('class', 'btn btn-danger xbtn');
});
$("#createevent").button().on("click", function () {
    $("#dialog").dialog("open");
    $("#dialog").attr('class', 'ui-widget-contentperso');
});
$("#quitevent").button().on("click", function () {
    $("#dialog").dialog("close");
});

$(function () {
    $("#datepicker2").datepicker();
});
$(function () {
    $("#datepicker3").datepicker();
});
function ClosePopup(msg) {
    var date = $('#datepicker2').val();
    //eventDates[ new Date( date )] += 0;
    //location.href = location.href;
    var table = $('#listeday').DataTable();
    $("#dialog").dialog("close");
    $.ajax({
        type: "GET",
        url: "@Url.Action("getlistbydate")",
        data: { datee: date },
        dataType: "json",
        success: function (list) {
            console.log(list)
            table.clear().draw();
            var hourday = 0;
            for (i = 0; i < list.length; i += 1) {
                var dt = new Date(list[i].heures);
                table.row.add([
                    "<button id=\"copier\"  onclick=\"copier('" + list[i].id + "')\" class=\"btn btn-primary zbtn\" style=\"margin: 12px\">Copier</button>",
                    list[i].type,
                    list[i].classe_str,
                    list[i].classe2_str,
                    list[i].nature,
                    dt.getHours(),
                    "<a href=\"/Tempo/Edit/" + list[i].id + "\">Edit</a> | <a href=\"/Tempo/Details/" + list[i].id + "\">Details</a> | <a href=\"/Tempo/Delete/" + list[i].id + "\">Delete</a>",
                ]).draw();
                hourday += dt.getHours();
            }
            console.log("test = " + hourday);
            eventDates[new Date(date)] = hourday;
            document.getElementById('hourday').innerHTML = "Total des heures du jour :  " + hourday;
            document.getElementById("iframcreat").src = "@this.Url.Action("Create","Tempo")" + "?datepicker2=" + dateText;

        },
        error: function (req, status, error) {
            document.getElementById('creat_button').hidden = true;
            alert("date note valide recharge page or call a administrator");
        }
    });
}
function is_valid_month() {
    var date = $('#datepicker3').val();
    $.ajax({
        type: "GET",
        url: "@Url.Action("is_Valid_Month")",
        data: { datepicker3: date },
        dataType: "text",
        success: function (bool) {
            if (bool == "true") {
                document.getElementById('monthValidationButton').hidden = true;
                document.getElementById('creat_button').hidden = true;
                document.getElementById('infovalidation').innerHTML = "Le mois est déjà validé.";
            }
            else {
                document.getElementById('monthValidationButton').hidden = false;
                document.getElementById('creat_button').hidden = false;
                document.getElementById('infovalidation').innerHTML = "Quand vous aurez rentré tous les événements, merci de valider le mois.";
            }
        },
        error: function (req, status, error) {
            alert("eror in month validation refresh page or call a administrator");
        }
    });
}
$("#coller").attr('class', 'btn btn-primary zbtn');
$("#coller").removeProperty("background");

$("#coller").button().on("click", function () {
    alert("coller");
    var dateText = $('#datepicker3').val();
    $.ajax({
        type: "GET",
        url: "@Url.Action("Coller")",
        data: { datecoller: dateText },
        success: function () {
            alert("normalement c bon");
            ClosePopup();
        },
        error: function (req, status, error) {
            alert("date note valide recharge page or call a administrator");
        }
    });
});

function copier(idevent) {
    console.log(idevent);
    $.ajax({
        type: "GET",
        url: "@Url.Action("Copier")",
        data: { eventid: idevent },
        success: function () {
            alert("normalement c bon");
        },
        error: function (req, status, error) {
            alert("date note valide recharge page or call a administrator");
        }
    });
}

function reportWindowSize() {
    $(".ui-dialog").position({
        my: "center", at: "center", of: window
    });
}