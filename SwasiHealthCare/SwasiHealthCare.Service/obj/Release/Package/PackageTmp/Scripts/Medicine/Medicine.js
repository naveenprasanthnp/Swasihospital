$("#btnMedicineSave").click(function (event) {
    var txtmedicinename = $("#txtmedicinename").val();
    if ($.trim(txtmedicinename) === '') {
        ShowErrorMessage("Please enter treatment name");
        $("#txtmedicinename").focus();
        return false;
    }

    //var txtdate = $("#txtdate").val();
    //if ($.trim(txtdate) === '') {
    //    ShowErrorMessage("Please enter date.");
    //    $("#txtdate").focus();
    //    return false;
    //}

    var txtmedicinedescription = $("#txtmedicinedescription").val();
    if ($.trim(txtmedicinedescription) === '') {
        ShowErrorMessage("Please enter treatment duration.");
        $("#txtmedicinedescription").focus();
        return false;
    }

    var txtmedicinepurchaserate = $("#txtmedicinepurchaserate").val();
    if ($.trim(txtmedicinepurchaserate) === '') {
        ShowErrorMessage("Please enter medicine purchase rate.");
        $("#txtmedicinepurchaserate").focus();
        return false;
    }

    var txtmedicinesalesrate = $("#txtmedicinesalesrate").val();
    if ($.trim(txtmedicinesalesrate) === '') {
        ShowErrorMessage("Please enter medicine sales rate.");
        $("#txtmedicinesalesrate").focus();
        return false;
    }

    var txtmedicinemanufacturer = $("#txtmedicinemanufacturer").val();
    if ($.trim(txtmedicinemanufacturer) === '') {
        ShowErrorMessage("Please enter medicine manufacturer.");
        $("#txtmedicinemanufacturer").focus();
        return false;
    }

    //var txtmedicineexpirydate = $("#txtmedicineexpirydate").val();
    //if ($.trim(txtmedicineexpirydate) === '') {
    //    ShowErrorMessage("Please enter treatment charges.");
    //    $("#txtmedicineexpirydate").focus();
    //    return false;
    //}

    //var txtmedicinecurrentstack = $("#txtmedicinecurrentstack").val();
    //if ($.trim(txtmedicinecurrentstack) === '') {
    //    ShowErrorMessage("Please enter medicine current stack.");
    //    $("#txtmedicinecurrentstack").focus();
    //    return false;
    //}

    //var txtmedicinepurchasestack = $("#txtmedicinepurchasestack").val();
    //if ($.trim(txtmedicinepurchasestack) === '') {
    //    ShowErrorMessage("Please enter medicine purchase stack.");
    //    $("#txtmedicinepurchasestack").focus();
    //    return false;
    //}

    //var txtmedicinebalancestack = $("#txtmedicinebalancestack").val();
    //if ($.trim(txtmedicinebalancestack) === '') {
    //    ShowErrorMessage("Please enter medicine balance stack.");
    //    $("#txtmedicinebalancestack").focus();
    //    return false;
    //}
});

//----------------------------MedicineStatus----------------
var medicinestatus = true;
function ChangeMedicineStatus(MedicineId, medstatus) {
    debugger;
    $("#hiddenmedicineid").val(MedicineId);
    $("#hiddenstatus").val(medstatus);
    $("#confirmmsg").html("Are you sure want to change the status of this medicine?");
    $("#exampleModalCenterTitle").html("Medicine Status");
    //$("#").modal('show');
    $("#ActionDelete").toggle();
    medicinestatus = medstatus;
}
function MedicineStatus() {
    alert("UserStatus");
    var medicineid = $("#hiddenmedicineid").val();
    $.ajax({
        type: "POST",
        url: "/Medicine/MedicineStatus",
        data: { medicineid: medicineid, status: medicinestatus },
        success: function (result) {
            $("#exampleModalCenter").modal("hide");
            if (result.Status) {
                $("#medicine_list").load(window.location.href + " #medicine_list");
                ShowSuccessMessage(result.SuccessMessage);
                $("#ActionDelete").toggle();
            } else {
                ShowErrorMessage("An error occured.");
            }
        },
        error: function () {
            ShowErrorMessage("An error occured.");
        }
    });
}
//-----------------------------------------------------

//---------------------RemoveUser-----------------------
function RemoveMedicine(MedicineId) {
    $("#hiddenmedicineid").val(MedicineId);
    //$("#DeleteUser").modal('show');
    $("#confirmmsg").html("Are you sure want to delete this medicine?");
    $("#exampleModalCenterTitle").html("Remove Staff");
    $("#ActionStatus").toggle();
}
function DeleteMedicine() {
    alert("DeleteUser");
    var medicineid = $("#hiddenmedicineid").val();
    $.ajax({
        type: "POST",
        url: "/Medicine/DeleteMedicine",
        data: { medicineid: medicineid },
        success: function (result) {
            $("#exampleModalCenter").modal("hide");
            if (result.Status) {
                $("#medicine_list").load(window.location.href + " #medicine_list");
                ShowSuccessMessage(result.SuccessMessage);
                $("#ActionStatus").toggle();
            } else {
                ShowErrorMessage("An error occured.");
            }
        },
        error: function () {
            ShowErrorMessage("An error occured.");
        }
    });
}
//----
