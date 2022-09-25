//-----------------------SaveSystem----------------------
$("#btnplansave").click(function (event) {
    debugger;
    var txttreatmentname = $("#txttreatmentname").val();
    if ($.trim(txttreatmentname) === '') {
        ShowErrorMessage("Please enter treatment name.");
        $("#txttreatmentname").focus();
        return false;
    }

    var txtnooftreatment = $("#txtnooftreatment").val();
    if ($.trim(txtnooftreatment) === '') {
        ShowErrorMessage("Please enter no of treatment.");
        $("#txtnooftreatment").focus();
        return false;
    }

    var txtpackagecode = $("#txtpackagecode").val();
    if ($.trim(txtpackagecode) === '') {
        ShowErrorMessage("Please enter package code.");
        $("#txtpackagecode").focus();
        return false;
    }

    var txtoffers = $("#txtoffers").val();
    if ($.trim(txtoffers) === '') {
        ShowErrorMessage("Please enter offers.");
        $("#txtoffers").focus();
        return false;
    }

    var txtamount = $("#txtamount").val();
    if ($.trim(txtamount) === '') {
        ShowErrorMessage("Please enter amount.");
        $("#txtamount").focus();
        return false;
    }

    var txtplanexpirydate = $("#txtplanexpirydate").val();
   
    if ($.trim(txtplanexpirydate) === '') {
        ShowErrorMessage("Please plan expiry date.");
        $("#txtplanexpirydate").focus();
        return false;
    }
});
//------------------------------------------------------------
//----------------------------PlanStatus----------------
var planstatus = true;
function ChangePlanStatus(PlanId, plnstatus) {
    $("#hiddenplanid").val(PlanId);
    $("#hiddenstatus").val(planstatus);
    $("#confirmmsg").html("Are you sure want to change the status of this plan?");
    $("#exampleModalCenterTitle").html("System Status");
    $("#ActionDelete").toggle();
    planstatus = plnstatus;
}
function PlanStatus() {
    var planid = $("#hiddenplanid").val();
    $.ajax({
        type: "POST",
        url: "/Settings/PlanStatus",
        data: { planid: planid, status: planstatus },
        success: function (result) {
            $("#exampleModalCenter").modal("hide");
            if (result.Status) {
                $("#plan_list").load(window.location.href + " #plan_list");
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
function RemovePlan(PlanId) {
    $("#hiddenplanid").val(PlanId);
    //$("#DeleteUser").modal('show');
    $("#confirmmsg").html("Are you sure want to delete this plan?");
    $("#exampleModalCenterTitle").html("Remove Staff");
    $("#ActionStatus").toggle();
}
function DeletePlan() {
    var planid = $("#hiddenplanid").val();
    $.ajax({
        type: "POST",
        url: "/Settings/DeletePlan",
        data: { planid: planid },
        success: function (result) {
            $("#exampleModalCenter").modal("hide");
            if (result.Status) {
                $("#plan_list").load(window.location.href + " #plan_list");
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

//-----------------------SaveSubscription----------------------
$("#btnplansubs").click(function (event) {
    //alert();
    var txtsubscriptiondate = $("#txtsubscriptiondate").val();
    
    if ($.trim(txtsubscriptiondate) === '') {
        ShowErrorMessage("Please select subscription date.");
        $("#txtsubscriptiondate").focus();
        return false;
    }

     
    var txtpatientid = $("#txtpatientid").val();
    if ($.trim(txtpatientid) === '') {
        ShowErrorMessage("Please select patient name.");
        $("#txtpatientid").focus();
        return false;
    }

    var txtplanId = $("#txtplanId").val();
    if ($.trim(txtplanId) === '') {
        ShowErrorMessage("Please select plan.");
        $("#txtplanId").focus();
        return false;
    }
});
//------------------------------------------------------------
//----------------------------PlanStatus----------------
var subsstatus = true;
function ChangeSubscriptionStatus(PatientPlanSubscriptionId, subsstatuss) {
    subsstatus = subsstatuss;
    //alert(subsstatus);
    $("#hiddensubsid").val(PatientPlanSubscriptionId);
    $("#hiddenstatus").val(subsstatuss);
    $("#confirmmsg").html("Are you sure want to change the status of this subscription?");
    $("#exampleModalCenterTitle").html("Subscription Status");
    $("#ActionDelete").toggle();
   
}
function SubscriptionStatus() {
    var patientplansubscriptionid = $("#hiddensubsid").val();
    //alert(subsstatus);
    $.ajax({
        type: "POST",
        url: "/Settings/SubscriptionStatus",
        data: { patientplansubscriptionid: patientplansubscriptionid, status: subsstatus },
        success: function (result) {
            $("#exampleModalCenter").modal("hide");
            if (result.Status) {
                //$("#subs_list").load();
                $("#subs_list").load(window.location.href + " #subs_list");
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
function RemoveSubscription(PatientPlanSubscriptionId) {
    $("#hiddensubsid").val(PatientPlanSubscriptionId);
    $("#confirmmsg").html("Are you sure want to delete this subscription?");
    $("#exampleModalCenterTitle").html("Remove Subscription");
    $("#ActionStatus").toggle();
}
function DeleteSubscription() {
    var patientplansubscriptionid = $("#hiddensubsid").val();
    $.ajax({
        type: "POST",
        url: "/Settings/DeleteSubscription",
        data: { patientplansubscriptionid: patientplansubscriptionid },
        success: function (result) {
            $("#exampleModalCenter").modal("hide");
            if (result.Status) {
                $("#subs_list").load(window.location.href + " #subs_list");
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