//var input = document.getElementById('input');
//input.onkeyup = function () {
//    alert();
//    debugger;
//    var filter = input.value.toUpperCase();
//    var lis = document.getElementsByTagName('li');
//    for (var i = 0; i < lis.length; i++) {
//        var name = lis[i].getElementsByClassName('name')[0].innerHTML;
//        if (name.toUpperCase().indexOf(filter) == 0)
//            lis[i].style.display = 'list-item';
//        else
//            lis[i].style.display = 'none';
//    }
//}
$("#input").on("keyup", function () {
    //alert();
    var value = $(this).val().toLowerCase();
    $("#table tr").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
});

//$(function () {
//    $('#input').keyup(function () {
//        debugger;
//        var matches = $('ul#myMenu').find('li:contains(' + $(this).val() + ') ');
//        $('li', 'ul#myMenu').not(matches).slideUp();
//        matches.slideDown();
//    });
//});

$("#btnnotesadd").click(function (event) {
    var allfill = true;
    var txtnotes = $("#txtnotes").val();
    if ($.trim(txtnotes) === '') {
        ShowErrorMessage("Please enter notes.");
        $("#txtnotes").focus();
        return false;
        allfill = false;
    }
    if (allfill) {
        debugger;
        var description = $("#txtnotes").val();
        $.ajax({
            type: "POST",
            url: "/Home/Notes",
            data: {
                Description: description
            },
            dataType: "json",
            success: function (result) {
                if (result.Status) {
                    $("#cardnotesbody").load(window.location.href + " #cardnotesbody");
                    $("#txtnotes").val('');
                    return true;
                }
                else {
                    ShowErrorMessage(result.ErrorMessage);
                    return false;
                }
            },
            error: function (result) {

                ShowErrorMessage("An error occured.");
            }
        });
    }
});

function DeleteNotes(notesid) {
    var notesid = notesid;
    $.ajax({
        type: "POST",
        url: "/Home/RemoveNotes",
        data: { NotesId: notesid},
        success: function (result) {
            if (result.Status) {
                $("#cardnotesbody").load(window.location.href + " #cardnotesbody");
                ShowSuccessMessage(result.SuccessMessage);
            } else {
                ShowErrorMessage("An error occured.");
            }
        },
        error: function () {
            ShowErrorMessage("An error occured.");
        }
    });
}