//----------------------OPDConsoltation-------------------
$("#btnopdconsoltationsave").click(function (event) {
    var allfill = true;
    var txtconsoltationdate = $("#txtconsoltationdate").val();
    if ($.trim(txtconsoltationdate) === '') {
        ShowErrorMessage("Please select date.");
        $("#txtconsoltationdate").focus();
        return false;
        allfill = false;
    }
    var txtdoctorname = $("#txtdoctorname").val();
    if ($.trim(txtdoctorname) === '') {
        ShowErrorMessage("Please select doctore name.");
        $("#txtdoctorname").focus();
        return false;
        allfill = false;
    }

    var txtpatientidnumber = $("#txtpatientidnumber").val();
    if ($.trim(txtpatientidnumber) === '') {
        ShowErrorMessage("Please enter mobile number.");
        $("#txtpatientidnumber").focus();
        return false;
        allfill = false;
    }

    var txtpatientname = $("#txtpatientname").val();
    if ($.trim(txtpatientname) === '') {
        ShowErrorMessage("Please enter patient name.");
        $("#txtpatientname").focus();
        return false;
        allfill = false;
    }
    var txtpatientgender = $("#txtpatientgender").val();
    if ($.trim(txtpatientgender) === '') {
        ShowErrorMessage("Please select patient gender.");
        $("#txtpatientgender").focus();
        return false;
        allfill = false;
    }
    var txtpatientage = $("#txtpatientage").val();
    if ($.trim(txtpatientage) === '') {
        ShowErrorMessage("Please enter patient age.");
        $("#txtpatientage").focus();
        return false;
        allfill = false;
    }
    var txtpatientmobilenumber = $("#txtpatientmobilenumber").val();
    if ($.trim(txtpatientmobilenumber) === '') {
        ShowErrorMessage("Please enter patient mobile number.");
        $("#txtpatientmobilenumber").focus();
        return false;
        allfill = false;
    } else {
        if ($.trim(txtpatientmobilenumber).length !== 10) {
            ShowErrorMessage("Please enter valid mobile number.");
            return false;
        }
    }
    var txtpackagetype = $("#txtpackagetype").val();
    if (txtpackagetype == '1') {
        var txtpatientpackage = $("#txtpatientpackage").val();
        if ($.trim(txtpatientpackage) === '--Select Package--') {
            ShowErrorMessage("Please select package.");
            $("#txtpatientpackage").focus();
            return false;
            allfill = false;
        }
    }
    var txtvisittype = $("#txtvisittype").val();
    if ($.trim(txtvisittype) === '--Select Visit Type--') {
        ShowErrorMessage("Please select visit type.");
        $("#txtvisittype").focus();
        return false;
        allfill = false;
    }
    var txttreatmentpaymentmode = $("#txttreatmentpaymentmode").val();
    if ($.trim(txttreatmentpaymentmode) === '') {
        ShowErrorMessage("Please select payment mode.");
        $("#txttreatmentpaymentmode").focus();
        return false;
        allfill = false;
    }
    //var txtpatienttemp = $("#txtpatienttemp").val();
    //if ($.trim(txtpatienttemp) === '') {
    //    ShowErrorMessage("Please enter patient temp.");
    //    $("#txtpatienttemp").focus();
    //    return false;
    //    allfill = false;
    //}
    //var txtpatientbp = $("#txtpatientbp").val();
    //if ($.trim(txtpatientbp) === '') {
    //    ShowErrorMessage("Please enter patient BP.");
    //    $("#txtpatientbp").focus();
    //    return false;
    //    allfill = false;
    //}
    //var txtpatientpulse = $("#txtpatientpulse").val();
    //if ($.trim(txtpatientpulse) === '') {
    //    ShowErrorMessage("Please enter patient pulse.");
    //    $("#txtpatientpulse").focus();
    //    return false;
    //    allfill = false;
    //}
     
    var txttreatmenttotalservicescharges = $("#txttreatmenttotalservicescharges").val();
    if ($.trim(txttreatmenttotalservicescharges) === '') {
        ShowErrorMessage("Please enter service charge.");
        $("#txttreatmenttotalservicescharges").focus();
        return false;
        allfill = false;
    }
    //var txtspldiscount = $("#txtspldiscount").val();
    //if ($.trim(txtspldiscount) === '') {
    //    ShowErrorMessage("Please enter spl discount.");
    //    $("#txtspldiscount").focus();
    //    return false;
    //    allfill = false;
    //}
    //var txtnetcharges = $("#txtnetcharges").val();
    //if ($.trim(txtnetcharges) === '') {
    //    ShowErrorMessage("Please enter net charges.");
    //    $("#txtnetcharges").focus();
    //    return false;
    //    allfill = false;
    //} 
    var txtdoctornotes = $("#txtdoctornotes").val();
    if ($.trim(txtdoctornotes) === '') {
        ShowErrorMessage("Please enter doctor notes.");
        $("#txtdoctornotes").focus();
        return false;
        allfill = false;
    }
    var txtpatientnatureofillness = $("#txtpatientnatureofillness").val();
    if ($.trim(txtpatientnatureofillness) === '') {
        ShowErrorMessage("Please enter nature of illness.");
        $("#txtpatientnatureofillness").focus();
        return false;
        allfill = false;
    }  
    if (allfill) {
        debugger;
        var consultationidnumber = $("#txtconsultationidnumber").val();
        var hiddenpatientid = $("#hiddenpatientid").val();
        var Consoltationdate = $("#txtconsoltationdate").val();
        var doctorname = $("#txtdoctorname").val();
        var doctorid = $("#txtdoctorname").val();
        var patientidnumber = $("#txtpatientidnumber").val();
        var patientname = $("#txtpatientname").val();
        var patientgender = $("#txtpatientgender").val();
        var patientage = $("#txtpatientage").val();
        var patientmobilenumber = $("#txtpatientmobilenumber").val();
        var patientheight = $("#txtpatientheight").val();
        var patientweight = $("#txtpatientweight").val();
        var patienttemp = $("#txtpatienttemp").val();
        var patientbp = $("#txtpatientbp").val();
        var patientpulse = $("#txtpatientpulse").val();
        var patientspo2 = $("#txtpatientspo2").val();
        var patientlmp = $("#txtpatientlmp").val();
        var patientpackage = $("#txtpatientpackage").val();
        var visittype = $("#txtvisittype").val();
        var servicecharge = $("#txtservicecharge").val();
        var spldiscount = $("#txtspldiscount").val();
        var netcharges = $("#txtnetcharges").val();
        var treatmentpaymentmode = $("#txttreatmentpaymentmode").val();
        var doctornotes = $("#txtdoctornotes").val();
        var patientnatureofillness = $("#txtpatientnatureofillness").val();
        var idnumber = $("#IDNumber").val();
        //var planid = $("#txtplanid").val();
        debugger;
        if (hiddenpatientid == null || hiddenpatientid == "") {
            hiddenpatientid = $("#PatientId").val();
        }
        
        
        $.ajax({
            type: "POST",
            url: "/Patient/NewOPD",
            data: {
                OPDConsultationId: $("#hiddenopdconsultationtid").val(),
                IDNumber : idnumber,
                ConsultationIDNumber: consultationidnumber,
                DoctorId: doctorid,
                PatientId: hiddenpatientid,
                ConsultationDate: Consoltationdate, 
                ConsoltationTime: "",
                DoctorName: doctorname,
                PatientIDNumber: patientidnumber,
                PatientName: patientname,
                PatientGender: patientgender,
                PatientAge: patientage,
                PatientMobileNo: patientmobilenumber,
                PatientVisitType: visittype,
                NatureOfIllness: patientnatureofillness,
                PatientHeight: patientheight,
                PatientWeight: patientweight,
                PatientTemp: patienttemp,
                PatientBP: patientbp,
                PatientPulse: patientpulse,
                PatientSpO2: patientspo2,
                PatientLMP: patientlmp,
                TreatmentTotalServicesCharges: servicecharge,
                TreatmentSplDiscount: spldiscount,
                NetCharges: netcharges,
                TreatmentPaymentMode: treatmentpaymentmode,
                DoctorsNote: doctornotes,
                PlanId: patientpackage
            },
            dataType: "json",
            success: function (result) {
                if (result.Status) {
                    debugger;
                    $("#hiddenopdConsoltationtid").val(result.opdconsoltationid);
                    $("#hiddenopdconsultationtid").val(result.opdconsoltationid);
                    $("#OPDConsultationId").val(result.opdconsoltationid); 
                    $("#hiddenpatientid").val(result.patientid);
                    
                    $("#t_22").trigger("click");
                    //$("#t_2").addClass("active");
                    //$("#t_1").removeClass("active");
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
//--------------------------------------------------------

//var planid = "";
//$("#txtplanid").change(function () {
//    planid = $("#txtplanid").val();
//    alert(planid);
    
//});

$("#txtpackagetype").change(function () {
    var packagetype = $("#txtpackagetype").val();
    //alert(packagetype);
    if (packagetype == '1') {
        $("#divpatientpackage").show();
        $("#divadvanceamount").show();
        $("#divservicename").hide();
        $("#divservicecharge").hide();
        var patientidnumber = $("#txtpatientidnumber").val();
        if (patientidnumber != '') {
            $.ajax({
                type: "GET",
                url: "/Patient/NewOPD",
                data: { opdconsutationid: null, patientidnumber: patientidnumber },
                dataType: "json",
                success: function (result) {
                    if (result != null) {
                       
                    }
                    else {
                       
                    }
                },
                error: function (result) {
                    //ShowErrorMessage("An error occured.");
                }
            });
        }

    } else {
        $("#divservicename").show();
        $("#divservicecharge").show();
        $("#divpatientpackage").hide();
        $("#divadvanceamount").hide();
    }
});

$("#txtpatientpackage").change(function () {
    var patientpackageid = $("#txtpatientpackage").val();
    if (patientpackageid != '') {
        //alert(patientpackageid);
        $.ajax({
            type: "GET",
            url: "/Patient/GetPackageDetails",
            data: { patientpackageid: patientpackageid },
            dataType: "json",
            success: function (result) {
                if (result != null) {
                    $("#popuplabel").show();
                    $("#packagebalancecount").show();
                    $("#packagebalanceamt").show();
                    $("#packagetotalprice").show();
                    $("#packagetotalcount").show();
                    $('#packagebalancecount').text('Balance Plan-Treatment Count : ' + result.BalanceServiceCount);
                    $('#packagebalanceamt').text('Balance Plan-Treatment Amount : ' + result.BalanceServiceAmount);
                    $('#packagetotalprice').text('Total Plan-Treatment Count : ' + result.TotalServiceCount);
                    $('#packagetotalcount').text('Total Plan-Treatment Amount : ' + result.TotalServiceAmount);
                    if (result.BalanceServiceCount == 0) {
                        $('#saveopdservice').prop('disabled', true)
                        $('#planavailablecount').hide();
                        $('#planprice').show();
                    } else {

                    }
                }
                else {
                    $('#medicineavailablecount').val('');
                }
            },
            error: function (result) {
                //alert(result.PlanId);
                debugger;
                ShowErrorMessage("An error occured.");
                
            }
        });
    }
    else {
        $('#medicineavailablecount').text('');
    }
});

//-----------------------OPDConsoltation------------------

$("#txtpatientidnumber").change(function () {
    debugger;
    var patientidnumber = $("#txtpatientidnumber").val();
    $.ajax({
        type: "POST",
        url: "/Patient/GetPatientDetailById",
        data: { patientidnumber: patientidnumber },
        dataType: "json",
        success: function (result) {
            if (result.Data != null) {
                $("#txtpatientname").val(result.Data.PatientName);
                $("#txtpatientgender").val(result.Data.PatientGender);
                $("#txtpatientage").val(result.Data.PatientAge);
                $("#txtpatientmobilenumber").val(result.Data.PatientMobileNumber);
                $("#hiddenpatientid").val(result.Data.PatientId);
              
            }
            else {
                $("#txtpatientname").val('');
                $("#txtpatientgender").val('');
                $("#txtpatientage").val('');
                $("#txtpatientmobilenumber").val('');
                $("#hiddenpatientid").val('');
                
            }
        },
        error: function (result) {
            ShowErrorMessage("An error occured.");
        }
    });
});

//------------------------OPDService----------------------

$("#saveopdservice").click(function (event) {
    debugger;
    var allfill = true;
    var pkgtype = $("#txtpackagetype").val();
    //alert(pkgtype);
    var servicename = '';
    var txtpatientpackage = 0;
    if (pkgtype == "1")
    {
        txtpatientpackage = $("#txtpatientpackage").val();
        servicename = $("#txtpatientpackage").val();
        if ($.trim(txtpatientpackage) === '') {
            ShowErrorMessage("Please select package.");
            $("#txtpatientpackage").focus();
            return false;
            allfill = false;
        }
    }
        else {
        servicename = $("#txtservicename").val();
        if ($.trim(txtservicename) === '') {
            ShowErrorMessage("Please enter service name.");
            $("#txtservicename").focus();
            return false;
            allfill = false;
            var txtservicecharge = $("#txtservicecharge").val();
            if ($.trim(txtservicecharge) === '') {
                ShowErrorMessage("Please enter service charge.");
                $("#txtservicecharge").focus();
                return false;
                allfill = false;
            }
        }
    }
    
    //var hiddennewserviceid = $("#hiddennewserviceid").val();
    //alert(hiddennewserviceid);
    //if (hiddennewserviceid == "true")
    //{
      
    //    var txtnewservicename = $("#txtnewservicename").val();
    //    if ($.trim(txtnewservicename) === '') {
    //        ShowErrorMessage("Please enter service name.");
    //        $("#txtnewservicename").focus();
    //        return false;
    //        allfill = false;
    //    }
    //    else
    //    {
    //        txtservicename = txtnewservicename;
    //    }
    //}
    //var txtcount = $("#txtcount").val();
    //if ($.trim(txtcount) === '') {
    //    ShowErrorMessage("Please count.");
    //    $("#txtcount").focus();
    //    return false;
    //    allfill = false;
    //}  

    

    var txttherapistname = $("#txttherapistname").val();
    if ($.trim(txttherapistname) === '') {
        ShowErrorMessage("Please select therapist name.");
        $("#txttherapistname").focus();
        return false;
        allfill = false;
    }
    var txtremarks = $("#txtremarks").val();
    if ($.trim(txtremarks) === '') {
        ShowErrorMessage("Please enter remarks.");
        $("#txtremarks").focus();
        return false;
        allfill = false;
    }
    if (allfill) {
        debugger;
        var opdconsoltationid = $("#hiddenopdconsultationtid").val();
        var patientid = $("#hiddenpatientid").val();
        if (patientid == "") {
            patientid = $("#PatientId").val();
        }
        //var servicename = $("#txtservicename").val();
        //var serviceid = "";
        //if (servicename == "1") {
        //    servicename = $("#txtnewservicename").val();
        //    serviceid = $("#txtnewservicename").val();
        //} else {
        //    servicename = $("#txtservicename").val();
        //    serviceid = 0;
        //}
        var servicecharge = '';
        var packtype = $("#txtpackagetype").val();
        var count = $("#txtcount").val();
        if (packtype =='1') {
            servicecharge = $("#txtadvanceamount").val();
        }
        if (packtype == '2') {
            servicecharge = $("#txtservicecharge").val();
        }
        var remarks = $("#txtremarks").val();
        //alert(servicename);
        var therapistname = $("#txttherapistname").val();
        $.ajax({
            type: "POST",
            url: "/Patient/SaveOPDService",
                data: {
                TreatmentPaymentMode: pkgtype,
                ServiceId: servicename,
                OPDConsoltationId: opdconsoltationid,
                PatientId: patientid,
                ServiceName: servicename,
                //Count: count,
                Count: 0,
                ServiceCharge: servicecharge,
                Remarks: remarks,
                IsNewService: false,
                TherapistName: therapistname,
                PatientPlanSubscriptionId: txtpatientpackage
            },
            dataType: "json",
            success: function (result) {
                debugger;
                if (result.Status) {
                    $("#Services").load(window.location.href + " #Services");
                    //location.reload();
                    $('#exampleModalCenter').modal('hide');
                    totalpatientamount
                    $("#hiddenopdConsoltationtid").val(result.OPDConsoltationId);
                    $("#packagebalancecount").hide();
                    $("#packagebalanceamt").hide();
                    $("#packagetotalprice").hide();
                    $("#packagetotalcount").hide();
                    $("#txtpatientpackage").val('');
                    $("#txtservicename").val('');
                    $("#txtadvanceamount").val('');
                    $("#txttherapistname").val('');
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

//----------Edit Opd-------------------------------
function EditOpdService(OPDServicesId) {
    //alert(OPDServicesId);
    $("#hiddenserviceid").val(OPDServicesId);
    $("#exampleModalCenterTitle").html("Edit Service");
    //alert();
    $.ajax({
        type: "POST",
        url: "/Patient/EditOPDService",
        data: { opdserviceid: OPDServicesId},
        dataType: "json",
        success: function (result) {
            debugger;
            if (result != null) {
                debugger;
                $("#OPDConsultationId").val(result.OPDConsultationId); 
                $("#PatientId").val(result.PatientId); 
                //$("#txtservicename").val(result.ServiceName);
                //$("#txtservicename").val(result.ServiceId); 
                $("#txtcount").val(result.Count);   
                $("#txtservicecharge").val(result.ServiceCharge);  
                $("#txtremarks").val(result.Remarks);
                $("#txtservicename").val(result.ServiceId);
                $("#txttherapistname").val(result.TherapistId);
                //alert(result.ServiceId);
                var Pm = result.PaymentMode;
                if (Pm == "1") {
                    //alert(result.OPDServicesId);
                    $("#txtadvanceamount").val(result.ServiceCharge);
                    $("div.divservicename select").val(result.ServiceId).change();
                }
                else {
                }
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
//--------------------------OPDServices-------------------

function EditOPDPrescription(OPDPrescriptionId) {
    $("#hiddenprescriptionid").val(OPDPrescriptionId);
    $("#exampleModalCenterTitlePrescription").html("Edit Prescription");

    $.ajax({
        type: "POST",
        url: "/Patient/EditOPDPrescription",
        data: { opdprescriptionid: OPDPrescriptionId },
        dataType: "json",
        success: function (result) {
            debugger;
            if (result != null) {
                $("#OPDPrescriptionId").val(result.OPDPrescriptionId);
                $("#txtmedicinename").val(result.MedicineName);
                $("#txtmedicinequantity").val(result.MedicineQuantity);
                $("#txtmedicinedosage").val(result.MedicineDosage);
                $("#txtmedicineduration").val(result.MedicineDuration);
                $("#txtmedicineinstructions").val(result.MedicineInstructions);
                $("#txtmedicinespecification").val(result.MedicineSpecification);
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

//--------------------------OPDPrescription---------------
$("#txtmedicinename1").hide()
$("#txtmedicinename").change(function () {
    var medname = $("#txtmedicinename").val();
    var t = $("#txtmedicinename option:selected").text();
    //alert(t);
    if (t == "Others") {
        //$("#divmedname").toggle();
        $("#txtmedicinename1").show();
    }
    else {
        $("#txtmedicinename1").hide();
    }
});
$("#saveopdprescription").click(function (event) {
    var t = $("#txtmedicinename option:selected").text();
    var allfill = true;
    var medicinename = "";
    var txtmedicinename = $("#txtmedicinename").val();
    if ($.trim(txtmedicinename) === '') {
        ShowErrorMessage("Please select medicine.");
        $("#txtmedicinename").focus();
        return false;
        allfill = false;
    } else {
        medicinename = $("#txtmedicinename").val();
    }
    if (t != "Others") {
        var txtmedicinequantity = $("#txtmedicinequantity").val();
        if ($.trim(txtmedicinequantity) === '') {
            ShowErrorMessage("Please select quantity.");
            $("#txtmedicinequantity").focus();
            return false;
            allfill = false;
        }
    }

    if (t == "Others") {
        var txtmedicinename1 = $("#txtmedicinename1").val();
        if ($.trim(txtmedicinename1) === '') {
            ShowErrorMessage("Please enter medicine name.");
            $("#txtmedicinename1").focus();
            return false;
            allfill = false;
        } else {
            medicinename = $("#txtmedicinename1").val();
        }
    }

    //var txtmedicineduration = $("#txtmedicineduration").val();
    //if ($.trim(txtmedicineduration) === '') {
    //    ShowErrorMessage("Please enter medicine duration.");
    //    $("#txtmedicineduration").focus();
    //    return false;
    //    allfill = false;
    //}
    var txtmedicineinstructions = $("#txtmedicineinstructions").val();
    if ($.trim(txtmedicineinstructions) === '') {
        ShowErrorMessage("Please enter medicine instructions.");
        $("#txtmedicineinstructions").focus();
        return false;
        allfill = false;
    }
    if (t != "Others") {
        var txtmedicineamount = $("#txtmedicineamount").val();
        if ($.trim(txtmedicineamount) === '') {
            ShowErrorMessage("Please enter medicine amount.");
            $("#txtmedicineamount").focus();
            return false;
            allfill = false;
        }
    }
    
    if (allfill) {
        var opdconsultationid = $("#OPDConsultationId").val();
        var patientid = $("#PatientId").val();
        //medicinename = $("#txtmedicinename").val();
        var medicinequantity = $("#txtmedicinequantity").val();
        //var medicinedosage = $("#txtmedicinedosage").val();
        //var medicineduration = $("#txtmedicineduration").val();
        var medicineinstructions = $("#txtmedicineinstructions").val();
        //var medicinespecification = $("#txtmedicinespecification").val();
        var medicineamount = $("#txtmedicineamount").val();
        var medicineid = $("#txtmedicinename").val();
        $.ajax({
            type: "POST",
            url: "/Patient/SaveOPDPrescription",
            data: {
                MedicineId: medicineid,
                OPDConsultationId : opdconsultationid,
                PatientId : patientid,
                MedicineName: medicinename,
                MedicineQuantity: medicinequantity,
                //MedicineDosage: medicinedosage,
                //MedicineDuration: medicineduration,
                MedicineInstructions: medicineinstructions,
                //MedicineSpecification: medicinespecification,
                MedicineAmount : medicineamount
            },
            dataType: "json",
            success: function (result) {
                if (result.Status) {
                    $("#hiddenopdConsoltationtid").val(result.opdconsoltationid);
                    $("#hiddenpatientid").val(result.patientid);
                    $("#Prescription").load(window.location.href + " #Prescription");
                    $('#exampleModalCenterPrescription').modal('hide');
                    $('#medicinenotavailable').hide();
                    $('#medicineavailablecount').hide();
                    $('#txtmedicinequantity').removeAttr('readonly', 'readonly');
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
//--------------------------------------------------------

function RemoveOpdService(OPDServicesId) {
    $("#hiddenserviceid").val(OPDServicesId);
    $("#confirmmsg").html("Are you sure want to delete this service?");
    $("#exampleModalCenterTitleOpdService").html("Remove Service");
}

function DeleteOpdService() {
    debugger;
    var hiddenserviceid = $("#hiddenserviceid").val();
    $.ajax({
        type: "POST",
        url: "/Patient/DeleteOPDService",
        data: { opdserviceid: hiddenserviceid },
        success: function (result) {
            $("#exampleModalCenterSubPopup").modal("hide");
            if (result.Status) {
                $("#Services").load(window.location.href + " #Services");
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

function RemoveOpdPrescription(OPDPrescriptionId) {
    debugger;
    $("#hiddenprescriptionid").val(OPDPrescriptionId);
    $("#confirmmsgpre").html("Are you sure want to delete this prescription?");
    $("#exampleModalCenterTitleOpdPrescription").html("Remove prescription");
}

function DeleteOpdPrescription() {
    debugger;
    var hiddenprescriptionid = $("#hiddenprescriptionid").val();
    $.ajax({
        type: "POST",
        url: "/Patient/DeleteOPDPrescription",
        data: { opdprescriptionid: hiddenprescriptionid },
        success: function (result) {
            $("#exampleModalCenterSubPopupPrescription").modal("hide");
            if (result.Status) {
                $("#Prescription").load(window.location.href + " #Prescription");
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

$("#txtmedicinename").change(function () {
    var medicineid = $("#txtmedicinename").val();
    if (medicineid != '') {
        $.ajax({
            type: "GET",
            url: "/Medicine/GetMedicineById",
            data: { medicineid: medicineid },
            dataType: "json",
            success: function (result) {
                if (result != null) {
                    $('#medicineavailablecount').text('Available Medicine : ' + result.AvailableMedicineCount);
                    $('#medicineprice').text('Medicine Price : ' + result.MedicineSalesRate);
                    $('#mediprice').val(result.MedicineSalesRate);
                    if (result.AvailableMedicineCount == 0) {
                        $('#txtmedicinequantity').attr('readonly', 'readonly');
                        $('#medicinenotavailable').text('Medicine not availabe');
                        $('#medicineavailablecount').hide();
                        $('#medicinenotavailable').show();
                    } else {
                        $('#medicinenotavailable').hide();
                        $('#medicineavailablecount').show();
                        $('#txtmedicinequantity').removeAttr('readonly', 'readonly');
                    }
                }
                else {
                    $('#medicineavailablecount').val('');
                }
            },
            error: function (result) {
                ShowErrorMessage("An error occured.");
            }
        });
    } else {
        $('#medicineavailablecount').text('');
    }
});

$("#txtmedicinequantity").change(function () {
    var v2 = $('#txtmedicinequantity').val();
    var v1 = $('#mediprice').val();
    var v3 = v1 * v2;
    $('#txtmedicineamount').val(v3);
});
//-------------------------------------------
function totalpatientamount() {
    var totalcharge = 0;
    $("#gird_tble TBODY TR").each(function () {
        debugger;
        var row = $(this);
        var amt = row.find("TD").eq(2).html();
        totalcharge = parseInt(totalcharge) + parseInt(amt);
    });

    $("#table_girdpres TBODY TR").each(function () {
        debugger;
        var row = $(this);
        var amt = row.find("TD").eq(3).html();
        totalcharge = parseInt(totalcharge) + parseInt(amt);
    });

    $("#txttreatmenttotalservicescharges").val(totalcharge);
    var opdconsultationid = $("#OPDConsultationId").val();
    var netcharge1 = $("#txtnetcharges").val();
    var spldiscount1 = $("#txtspldiscount").val();
    updatepatientamount(opdconsultationid, totalcharge, netcharge1, spldiscount1);
}
//-------------------------------------------

//---update total charge-----------
function updatepatientamount(opdconsultationid, totalcharge, netcharge, spldiscount) {
    debugger;
    $.ajax({
        type: "POST",
        url: "/Patient/UpdateTotalCharge",
        data: {
            OPDConsultationId: opdconsultationid,
            TreatmentTotalServicesCharges: totalcharge,
            TreatmentSplDiscount: spldiscount,
            NetCharges: netcharge
        },
        dataType: "json",
        success: function (result) {
            debugger;
            if (result.Status) {

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
