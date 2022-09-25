//-----------------------SaveSystem----------------------
$("#btnSystemSave").click(function (event) {
    var systemname = $("#txtsystemname").val();
    if ($.trim(systemname) === '') {
        ShowErrorMessage("Please enter system name.");
        $("#txtsystemname").focus();
        return false;
    }

    var systemip = $("#txtsystemip").val();
    if ($.trim(systemip) === '') {
        ShowErrorMessage("Please enter system system ip.");
        $("#txtsystemip").focus();
        return false;
    } else {
        var validate = systemip.split('.').filter(n => (n !== '' && n >= 0 && n <= 255)).length === 4
        if (validate == false) {
            ShowErrorMessage("Please enter valid ip address.");
            $("#txtsystemip").focus();
            return false;
        }
        }

    var systemmodel = $("#txtsystemmodel").val();
    if ($.trim(systemmodel) === '') {
        ShowErrorMessage("Please enter system model.");
        $("#txtsystemmodel").focus();
        return false;
    }
});
//------------------------------------------------------------
//----------------------------SystemStatus----------------
var systemstatus = true;
function ChangeSystemStatus(SystemId, sysstatus) {
    $("#hiddensystemid").val(SystemId);
    $("#hiddenstatus").val(sysstatus);
    $("#confirmmsg").html("Are you sure want to change the status of this system?");
    $("#exampleModalCenterTitle").html("System Status"); 
    $("#ActionDelete").toggle();
    systemstatus = sysstatus;
}
function SystemStatus() {
    var systemid = $("#hiddensystemid").val();
    $.ajax({
        type: "POST",
        url: "/Settings/SystemStatus",
        data: { systemid: systemid, status: systemstatus },
        success: function (result) {
            $("#exampleModalCenter").modal("hide");
            if (result.Status) {
                $("#system_list").load(window.location.href + " #system_list");
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
function RemoveSystem(SystemId) {
    $("#hiddensystemid").val(SystemId);
    //$("#DeleteUser").modal('show');
    $("#confirmmsg").html("Are you sure want to delete this system?");
    $("#exampleModalCenterTitle").html("Remove Staff");
    $("#ActionStatus").toggle();
}
function DeleteSystem() {
    var systemid = $("#hiddensystemid").val();
    debugger;
    $.ajax({
        type: "POST",
        url: "/Settings/DeleteSystem",
        data: { systemid: systemid },
        success: function (result) {
            $("#exampleModalCenter").modal("hide");
            if (result.Status) {
                $("#system_list").load(window.location.href + " #system_list");
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