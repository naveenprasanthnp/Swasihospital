$("#btnTreatmentSave").click(function (event) {
    var txttreatmentName = $("#txttreatmentName").val();
    if ($.trim(txttreatmentName) === '') {
        ShowErrorMessage("Please enter treatment name");
        $("#txttreatmentName").focus();
        return false;
    }

    var txttreatmentduration = $("#txttreatmentduration").val();
    if ($.trim(txttreatmentduration) === '') {
        ShowErrorMessage("Please enter treatment duration.");
        $("#txttreatmentduration").focus();
        return false;
    }
    var txttreatmentfee = $("#txttreatmentfee").val();
    if ($.trim(txttreatmentfee) === '') {
        ShowErrorMessage("Please enter treatment fee.");
        $("#txttreatmentfee").focus();
        return false;
    }
    //var txttreatmentdescription = $("#txttreatmentdescription").val();
    //if ($.trim(txttreatmentdescription) === '') {
    //    ShowErrorMessage("Please enter treatment description.");
    //    $("#txttreatmentdescription").focus();
    //    return false;
    //}

    //var txttreatmentmedicineneeded = $("#txttreatmentmedicineneeded").val();
    //if ($.trim(txttreatmentmedicineneeded) === '') {
    //    ShowErrorMessage("Please enter treatment medicine needed.");
    //    $("#txttreatmentmedicineneeded").focus();
    //    return false;
    //}
});

//------------------------------------------------------------
//----------------------------SystemStatus----------------
var treatmentstatus = true;
function ChangeTreatmentStatus(TreatmentId,tmtstatus) {
    $("#hiddentreatmentid").val(TreatmentId);
    $("#hiddenstatus").val(tmtstatus);
    $("#confirmmsg").html("Are you sure want to change the status of this system?");
    $("#exampleModalCenterTitle").html("System Status");
    $("#ActionDelete").toggle();
    treatmentstatus = tmtstatus;
}
function TreatementStatus() {
    var treatmentid= $("#hiddentreatmentid").val();
    $.ajax({
        type: "POST",
        url: "/Treatment/TreatmentStatus",
        data: { treatmentid: treatmentid, status: treatmentstatus },
        success: function (result) {
            $("#exampleModalCenter").modal("hide");
            if (result.Status) {
                $("#treatment_list").load(window.location.href + " #treatment_list");
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
function RemoveTreatment(TreatmentId) {
    $("#hiddentreatmentid").val(TreatmentId);
    //$("#DeleteUser").modal('show');
    $("#confirmmsg").html("Are you sure want to delete this treatment?");
    $("#exampleModalCenterTitle").html("Remove treatment");
    $("#ActionStatus").toggle();
}
function DeleteTreatement() {
    var treatmentid = $("#hiddentreatmentid").val();
    debugger;
    $.ajax({
        type: "POST",
        url: "/Treatment/DeleteTreatment",
        data: { treatmentid: treatmentid },
        success: function (result) {
            $("#exampleModalCenter").modal("hide");
            if (result.Status) {
                $("#treatment_list").load(window.location.href + " #treatment_list");
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
//--
