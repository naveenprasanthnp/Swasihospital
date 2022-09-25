$("#btnhospitalsave").click(function (event) {
    var txthospitalcode = $("#txthospitalcode").val();
    if ($.trim(txthospitalcode) === '') {
        ShowErrorMessage("Please enter hospital code.");
        $("#txthospitalcode").focus();
        return false;
    }

    var txthospitalname = $("#txthospitalname").val();
    if ($.trim(txthospitalname) === '') {
        ShowErrorMessage("Please enter hospital name.");
        $("#txthospitalname").focus();
        return false;
    }

    var txthospitalmobilnumber = $("#txthospitalmobilnumber").val();
    if ($.trim(txthospitalmobilnumber) === '') {
        ShowErrorMessage("Please enter mobile number.");
        $("#txthospitalmobilnumber").focus();
        return false;
    }

    //var txtlandlinenumber = $("#txtlandlinenumber").val();
    //if ($.trim(txtlandlinenumber) === '') {
    //    ShowErrorMessage("Please enter system model.");
    //    $("#txtlandlinenumber").focus();
    //    return false;
    //}

    var txthospitalcontactpersonname = $("#txthospitalcontactpersonname").val();
    if ($.trim(txthospitalcontactpersonname) === '') {
        ShowErrorMessage("Please enter contact person name.");
        $("#txthospitalcontactpersonname").focus();
        return false;
    }

    //var txthospitalcontactpersonemail = $("#txthospitalcontactpersonemail").val();
    //if ($.trim(txthospitalcontactpersonemail) === '') {
    //    ShowErrorMessage("Please enter contact persone mail.");
    //    $("#txthospitalcontactpersonemail").focus();
    //    return false;
    //}

    //var txthospitalemail = $("#txthospitalemail").val();
    //if ($.trim(txthospitalemail) === '') {
    //    ShowErrorMessage("Please enter hospital email.");
    //    $("#txthospitalemail").focus();
    //    return false;
    //}
    var txthospitaladdress = $("#txthospitaladdress").val();
    if ($.trim(txthospitaladdress) === '') {
        ShowErrorMessage("Please enter address.");
        $("#txthospitaladdress").focus();
        return false;
    }
    var txtpatientidstartwith = $("#txtpatientidstartwith").val();
    if ($.trim(txtpatientidstartwith) === '') {
        ShowErrorMessage("Please enter patient id start with.");
        $("#txtpatientidstartwith").focus();
        return false;
    }
});

//----------------------------HospitalStatus----------------
var hospitalstatus = true;
function ChangeHospitalStatus(HospitalId, hplstatus) {
    $("#hiddenhospitalid").val(HospitalId);
    $("#hiddenstatus").val(hplstatus);
    $("#confirmmsg").html("Are you sure want to change the status of this hospital?");
    $("#exampleModalCenterTitle").html("Hospital Status");
    //$("#").modal('show');
    $("#ActionDelete").toggle();
    hospitalstatus = hplstatus;
}
function HospitalStatus() {
    var hospitalid = $("#hiddenhospitalid").val();
    $.ajax({
        type: "POST",
        url: "/Hospital/HospitalStatus",
        data: { hospitalid: hospitalid, status: hospitalstatus },
        success: function (result) {
            $("#exampleModalCenter").modal("hide");
            if (result.Status) {
                $("#hospital_list").load(window.location.href + " #hospital_list");
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
function RemoveHospital(HospitalId) {
    $("#hiddenhospitalid").val(HospitalId);
    //$("#DeleteUser").modal('show');
    $("#confirmmsg").html("Are you sure want to delete this hospital?");
    $("#exampleModalCenterTitle").html("Remove Staff");
    $("#ActionStatus").toggle();
}
function DeleteHospital() {
    alert("DeleteUser");
    var hospitalid = $("#hiddenhospitalid").val();
    $.ajax({
        type: "POST",
        url: "/Hospital/DeleteHospital",
        data: { hospitalid: hospitalid },
        success: function (result) {
            $("#exampleModalCenter").modal("hide");
            if (result.Status) {
                $("#hospital_list").load(window.location.href + " #hospital_list");
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
