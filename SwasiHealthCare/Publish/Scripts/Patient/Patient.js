//-----------------------Patientsave------------------
$("#btnbasicdetailssave").click(function (event) {
    var allfill = true;
    var txtdoctorname = $("#txtdoctorname").val();
    if ($.trim(txtdoctorname) === '') {
        ShowErrorMessage("Please Select Doctor.");
        $("#txtdoctorname").focus();
        return false;
    }
    var txtpatientdate = $("#txtpatientdate").val();
    if ($.trim(txtpatientdate) === '') {
        ShowErrorMessage("Please Select Date.");
        $("#txtpatientdate").focus();
        return false;
    }
    var txtpatientname = $("#txtpatientname").val();
    if ($.trim(txtpatientname) === '') {
        ShowErrorMessage("Please enter patient name.");
        $("#txtpatientname").focus();
        return false;
        allfill = false;
    }
    var txtpatientgender = $("#txtpatientgender").val();
    
    if ($.trim(txtpatientgender) === '--Select Gender--') {
        ShowErrorMessage("Please Select Gender.");
        $("#txtpatientgender").focus();
        return false;
    }
    var txtpatientage = $("#txtpatientage").val();
    if ($.trim(txtpatientage) === '') {
        ShowErrorMessage("Please enter age.");
        $("#txtpatientage").focus();
        return false;
        allfill = false;
    } else if ($.trim(txtpatientage) === '0'){
        ShowErrorMessage("Please enter age valid age.");
        $("#txtpatientage").focus();
        return false;
        allfill = false;
    }
     
    var txtpatientmobilenumber = $("#txtpatientmobilenumber").val();
    if ($.trim(txtpatientmobilenumber) === '') {
        ShowErrorMessage("Please enter mobile number.");
        $("#txtpatientmobilenumber").focus();
        return false;
        allfill = false;
    } else {
        if ($.trim(txtpatientmobilenumber).length !== 10) {
            ShowErrorMessage("Please enter valid mobile number.");
            return false;
        }
    }
    var txtpatientmaritalstatus = $("#txtpatientmaritalstatus").val();
    if ($.trim(txtpatientmaritalstatus) === '--Select Marital Status--') {
        ShowErrorMessage("Please Select Patient Marital Status.");
        $("#txtpatientmaritalstatus").focus();
        return false;
    }

    var txtpatientwhatsappnumber = $("#txtpatientwhatsappnumber").val();
    if (txtpatientwhatsappnumber !== '') {
        if ($.trim(txtpatientwhatsappnumber).length !== 10) {
            ShowErrorMessage("Please enter valid whatsapp number.");
            return false;
        }
    }
 
    var txtpatientprimarycomplaints = $("#txtpatientprimarycomplaints").val();
    if ($.trim(txtpatientprimarycomplaints) === '') {
        ShowErrorMessage("Please enter patient  primary complaints.");
        $("#txtpatientprimarycomplaints").focus();
        return false;
        allfill = false;
    }
    var txtpatientassociatecomplaints = $("#txtpatientassociatecomplaints").val();
    if ($.trim(txtpatientassociatecomplaints) === '') {
        ShowErrorMessage("Please enter patient associate complaints.");
        $("#txtpatientassociatecomplaints").focus();
        return false;
        allfill = false;
    }
    var txtpatienthistoryofpresentillness = $("#txtpatienthistoryofpresentillness").val();
    if ($.trim(txtpatienthistoryofpresentillness) === '') {
        ShowErrorMessage("Please enter patient history of present illness.");
        $("#txtpatienthistoryofpresentillness").focus();
        return false;
        allfill = false;
    }
    var txtpatienthistoryofsurgery = $("#txtpatienthistoryofsurgery").val();
    if ($.trim(txtpatienthistoryofsurgery) === '') {
        ShowErrorMessage("Please enter patient history of surgery.");
        $("#txtpatienthistoryofsurgery").focus();
        return false;
        allfill = false;
    }
    var txtpatientaddress = $("#txtpatientaddress").val();
    if ($.trim(txtpatientaddress) === '') {
        ShowErrorMessage("Please enter patient address.");
        $("#txtpatientaddress").focus();
        return false;
        allfill = false;
    }
    //if (allfill) {
    //    alert(allfill);
    //    debugger;
    //    var patientid = $("#txtpatientid").val();
    //    var patientdate = $("#txtpatientdate").val();
    //    var doctorid = $("#txtdoctorname").val();
    //    var patientregisternumber = $("#PatientRegisterNumber").val();
    //    //var planid = $("#txtplanid").val();
    //    var patientame = $("#txtpatientname").val();
    //    var patientgender = $("#txtpatientgender").val();
    //    var patientage = $("#txtpatientage").val();
    //    var patientaddress = $("#txtpatientaddress").val();
    //    var patientmobilenumber = $("#txtpatientmobilenumber").val();
    //    var patientwhatsappnumber = $("#txtpatientwhatsappnumber").val();
    //    var patienteducation = $("#txtpatienteducation").val();
    //    var patientoccupation = $("#txtpatientoccupation").val();
    //    var patientmaritalstatus = $("#txtpatientmaritalstatus").val();
    //    var patientprimarycomplaints = $("#txtpatientprimarycomplaints").val();
    //    var patientassociatecomplaints = $("#txtpatientassociatecomplaints").val();
    //    var patienthistoryofpresentillness = $("#txtpatienthistoryofpresentillness").val();
    //    var patienthistoryofsurgery = $("#txtpatienthistoryofsurgery").val();
    //    var patientfamilyhistory = $("#txtpatientfamilyhistory").val();
    //    //var patienthospitalid = $("#txthospitalid").val();
    //    var historyoftreatment = $("#txthistoryoftreatment").val();
    //    var obghistory = $("#txtobghistory").val();
    //    var flag = "1";
    //    $.ajax({
    //        type: "POST",
    //        url: "/Patient/NewPatient",
    //        data: {
    //            //OBGHistory: obghistory,
    //            //PatientDate: patientdate,
    //            //HistoryOfTreatment : historyoftreatment,
    //            //Flag: flag,
    //            //PatientRegisterNumber: patientregisternumber,
    //            //PatientIdNumber: patientid,
    //            //PlanId : 0,
    //            //PatientName: patientame,
    //            //PatientGender: patientgender,
    //            //PatientAge: patientage,
    //            //PatientAddress: patientaddress,
    //            //PatientMobileNumber: patientmobilenumber,
    //            //PatientWhatsappNumber: patientwhatsappnumber,
    //            //PatientEducation: patienteducation,
    //            //PatientOccupation: patientoccupation,
    //            //PatientMaritalStatus: patientmaritalstatus,
    //            //PatientPrimaryComplaints: patientprimarycomplaints,
    //            //PatientAssociateComplaints: patientassociatecomplaints,
    //            //PatientHistoryOfPresentIllness: patienthistoryofpresentillness,
    //            //PatientHistoryOfSurgery: patienthistoryofsurgery,
    //            //PatientFamilyHistory: patientfamilyhistory,
    //            ////PatientHospitalId: patienthospitalid,
    //            PatientDoctorId: doctorid
    //        },
    //        dataType: "json",
    //        success: function (result) {
    //            if (result.Status) {
    //                window.location.href = '/Patient/Index/'
    //                //$("#hiddenpatientid").val(result.patientid);
    //                //$("#t_222").trigger("click");
    //                //return true;
    //            }
    //            else {
    //                ShowErrorMessage(result.ErrorMessage);
    //                return false;
    //            }
    //        },
    //        error: function (result) {

    //            ShowErrorMessage("An error occured.");
    //        }
    //    });
    //}

});

$("#btnpersonaldetailsave").click(function (event) {
    var allfill = true;
    var txtpatientcolor = $("#txtpatientcolor").val();
    if ($.trim(txtpatientcolor) === '') {
        ShowErrorMessage("Please enter patient color.");
        $("#txtpatientcolor").focus();
        return false;
        allfill = false;
    }
    var txtpatienthunger = $("#txtpatienthunger").val();
    if ($.trim(txtpatienthunger) === '') {
        ShowErrorMessage("Please patient hunger.");
        $("#txtpatienthunger").focus();
        return false;
        allfill = false;
    }  

    var txtpatientheight = $("#txtpatientheight").val();
    if ($.trim(txtpatientheight) === '') {
        ShowErrorMessage("Please patient height.");
        $("#txtpatientheight").focus();
        return false;
        allfill = false;
    }

    var txtpatientweight = $("#txtpatientweight").val();
    if ($.trim(txtpatientweight) === '') {
        ShowErrorMessage("Please enter patient sweight.");
        $("#txtpatientweight").focus();
        return false;
        allfill = false;
    }
    var txtpatienttaste = $("#txtpatienttaste").val();
    if ($.trim(txtpatienttaste) === '') {
        ShowErrorMessage("Please enter patient taste.");
        $("#txtpatienttaste").focus();
        return false;
        allfill = false;
    }
    var txtpatientemotion = $("#txtpatientemotion").val();
    if ($.trim(txtpatientemotion) === '') {
        ShowErrorMessage("Please enter patient emotion.");
        $("#txtpatientemotion").focus();
        return false;
        allfill = false;
    }
    var txtpatientmotion = $("#txtpatientmotion").val();
    if ($.trim(txtpatientmotion) === '') {
        ShowErrorMessage("Please enter patient motion.");
        $("#txtpatientmotion").focus();
        return false;
        allfill = false;
    }
    var txtpatienturine = $("#txtpatienturine").val();
    if ($.trim(txtpatienturine) === '') {
        ShowErrorMessage("Please enter patient urine.");
        $("#txtpatienturine").focus();
        return false;
        allfill = false;
    }
    var txtpatientbp = $("#txtpatientbp").val();
    if ($.trim(txtpatientbp) === '') {
        ShowErrorMessage("Please enter patient bp.");
        $("#txtpatientbp").focus();
        return false;
        allfill = false;
    }
    var txtpatientsugar = $("#txtpatientsugar").val();
    if ($.trim(txtpatientsugar) === '') {
        ShowErrorMessage("Please enter patient sugar.");
        $("#txtpatientsugar").focus();
        return false;
        allfill = false;
    }
    if (allfill) {
        debugger;
        var patientid = $("#hiddenpatientid").val();
        var patientcolor = $("#txtpatientcolor").val();
        var patienthunger = $("#txtpatienthunger").val();
        var patientheight = $("#txtpatientheight").val();
        var patientweight = $("#txtpatientweight").val();
        var patienttaste = $("#txtpatienttaste").val();
        var patientmotion = $("#txtpatientmotion").val();
        var patientemotion = $("#txtpatientemotion").val();
        var patienturine = $("#txtpatienturine").val();
        var patientbp = $("#txtpatientbp").val();
        var patientsugar = $("#txtpatientsugar").val();
        var patienttemperature = $("#txtpatienttemperature").val();
        var flag = "1";
        $.ajax({
            type: "POST",
            url: "/Patient/NewPatient",
            data: {
                Flag: flag,
                PatientId: patientid,
                PatientColor: patientcolor,
                PatientHunger: patienthunger,
                PatientHeight: patientheight,
                PatientWeight: patientweight,
                PatientTaste: patienttaste,
                PatientEmotion: patientemotion ,
                PatientMotion: patientmotion,
                PatientUrine: patienturine,
                PatientBP: patientbp,
                PatientSugar: patientsugar,
                PatientTemperature: patienttemperature
            },
            dataType: "json",
            success: function (result) {
                if (result.Status) {
                    debugger;
                    $("#t_333").trigger("click");
                    $("#hiddenpatientid").val(result.patientid);
                    $("#PatientId").val(result.patientid); 
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

$("#btnphyexamsave").click(function (event) {
    debugger;
    var patientid = $("#hiddenpatientid").val();
    var patientpulse = $("#txtpatientpulse").val();
    var patientpalpitation = $("#txtpatientpalpitation").val();
    var patienttongue = $("#txtpatienttongue").val();
    var patientfoot = $("#txtpatientfoot").val();
    var patienteye = $("#txtpatienteye").val();
    var flag = "2";
    $.ajax({
        type: "POST",
        url: "/Patient/NewPatient",
        data: {
            Flag: flag,
            PatientId : patientid,
            PatientPulse: patientpulse,
            PatientPalpitation: patientpalpitation,
            PatientTongue: patienttongue,
            PatientFoot: patientfoot,
            PatientEye: patienteye
        },
        dataType: "json",
        success: function (result) {
            if (result.Status) {
                debugger;
                $("#hiddenpatientid").val(result.patientid);
                $("#PatientId").val(result.patientid); 
                $("#t_444").trigger("click");
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
});

$("#btndiagnosissave").click(function (event) {
    debugger;
    var patientid = $("#hiddenpatientid").val();
    var VatajaDisorder = $("#txtVatajaDisorder").val();
    var PittajaDisorder = $("#txtPittajaDisorder").val();
    var KaphajaDisorder = $("#txtKaphajaDisorder").val();
    var flag = "4";
    $.ajax({
        type: "POST",
        url: "/Patient/NewPatient",
        data: {
            Flag: flag,
            PatientId: patientid,
            KaphajaDisorder: KaphajaDisorder,
            PittajaDisorder: PittajaDisorder,
            VatajaDisorder: VatajaDisorder
        },
        dataType: "json",
        success: function (result) {
            if (result.Status) {
                debugger;
                $("#hiddenpatientid").val(result.patientid);
                $("#PatientId").val(result.patientid);
                $("#t_222").trigger("click");
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
});

//----------------------------SystemStatus----------------
var patientstatus = true;
function ChangePatientStatus(PatientId, patstatus) {
    $("#hiddenpatientid").val(PatientId);
    $("#hiddenstatus").val(patstatus);
    $("#confirmmsg").html("Are you sure want to change the status of this patient?");
    $("#exampleModalCenterTitle").html("Patient Status");
    $("#ActionDelete").toggle();
    patientstatus = patstatus;
}
function PatientStatus() {
    var patientid = $("#hiddenpatientid").val();
    $.ajax({
        type: "POST",
        url: "/Patient/PatientStatus",
        data: { patientid: patientid, status: patientstatus },
        success: function (result) {
            $("#exampleModalCenter").modal("hide");
            if (result.Status) {
                $("#patient_list").load(window.location.href + " #patient_list");
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
function RemovePatient(PatientId) {
    $("#hiddenpatientid").val(PatientId);
    //$("#DeleteUser").modal('show');
    $("#confirmmsg").html("Are you sure want to delete this patient?");
    $("#exampleModalCenterTitle").html("Remove Patient");
    $("#ActionStatus").toggle();
}
function DeletePatient() {
    var patientid = $("#hiddenpatientid").val();
    debugger;
    $.ajax({
        type: "POST",
        url: "/Patient/DeletePatient",
        data: { patientid: patientid },
        success: function (result) {
            $("#exampleModalCenter").modal("hide");
            if (result.Status) {
                $("#patient_list").load(window.location.href + " #patient_list");
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
//-----------------------------------------------------