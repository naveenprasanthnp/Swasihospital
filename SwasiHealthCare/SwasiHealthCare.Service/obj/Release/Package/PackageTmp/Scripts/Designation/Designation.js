//----------------------------SystemStatus----------------
var designationstatus = true;
function ChangeDesignationStatus(DesignationId, desigstatus) {
    $("#hiddendesignationid").val(DesignationId);
    $("#hiddenstatus").val(desigstatus);
    $("#confirmmsg").html("Are you sure want to change the status of this designation?");
    $("#exampleModalCenterTitle").html("Designation Status");
    $("#ActionDelete").toggle();
    designationstatus = desigstatus;
}

function DesignationStatus() {
    var designationid = $("#hiddendesignationid").val();
    debugger;
    $.ajax({
        type: "POST",
        url: "/Designation/DesignationStatus",
        data: { designationid: designationid, status: designationstatus },
        success: function (result) {
            $("#exampleModalCenter").modal("hide");
            if (result.Status) {
                $("#designation_list").load(window.location.href + " #designation_list");
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

//---------------------RemoveUser----------------------------
function RemoveDesignation(DesignationId) {
    $("#hiddendesignationid").val(DesignationId);
    alert(DesignationId);
    //$("#DeleteUser").modal('show');
    $("#confirmmsg").html("Are you sure want to delete this designation?");
    $("#exampleModalCenterTitle").html("Remove Staff");
    $("#ActionStatus").toggle();
}
function DeleteDesignation() {
    var designationid = $("#hiddendesignationid").val();
    debugger;
    $.ajax({
        type: "POST",
        url: "/Designation/DeleteDesignation",
        data: { designationid: designationid },
        success: function (result) {
            $("#exampleModalCenter").modal("hide");
            if (result.Status) {
                $("#designation_list").load(window.location.href + " #designation_list");
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
//-----------------------------------------------------------

//----------------------AddDesignation-----------------------
function DesignationAdd() {
    var valdesinationname = $('#DesignationName').val();
    var designationid = $('#hiddendesignationid').val();
    if (valdesinationname === "")
    {
        ShowErrorMessage("Please enter designation name.");
        $("#DesignationName").focus();
        return false;
    }
    else
    {
        $.ajax({
            type: "POST",
            url: "/Designation/NewDesignation",
            data: { DesignationName: valdesinationname, DesignationId: designationid },
            dataType: "json",
            success: function (result) {
                debugger;
                if (result.Status) {
                    $("#designation_list").load(window.location.href + " #designation_list");
                    $("#exampleModalCenterAddDesig").modal("hide");
                    $("#DesignationName").val('');
                    $('#hiddendesignationid').val('');
                    ShowSuccessMessage(result.SuccessMessage);
                    $("#DesignationName").val('');
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

}
//-----------------------------------------------------------
//---------------------EditDesignation-----------------------
function EditExistDesignation(DesignationId, designame) {
    $("#hiddendesignationid").val(DesignationId);
    $("#DesignationName").text(designame);
    $("#confirmmsg").html("Are you sure want to change the status of this designation?");
    $("#exampleModalCenterTitleEdit").html("Designation Status");
    $("#ActionDelete").toggle();
    $("#DesignationName").val(designame);
    $("#hiddendesignationid").val(DesignationId);
}
var DesignationEdit = function () {
    var DesignationId = $("#hiddendesignationid").val();
    var DesignationName = $("#DesignationName").val();
    if (DesignationName === "") {
        ShowErrorMessage("Please Enter Name.");
        $("#DesignationName").focus();
        return false;
    }
    else {
        $.ajax({
            type: "POST",
            url: "/Designation/EditDesignation",
            data: { DesignationId: DesignationId, DesignationName: DesignationName },
            success: function (result) {
                if (result.Status) {
                    location.reload();
                    ShowSuccessMessage(result.SuccessMessage);
                    return true;
                }
                else {
                    ShowErrorMessage(result.ErrorMessage);
                    return false;
                }
            },
            error: function () {
                ShowErrorMessage("An error occured.");
            }
        });
    }
}
//-----------------------------------------------------

