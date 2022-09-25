$("#btnexpensesave").click(function (event) {
    var txtexpensedescription = $("#txtexpensedescription").val();
    if ($.trim(txtexpensedescription) === '') {
        ShowErrorMessage("Please enter description.");
        $("#txtexpensedescription").focus();
        return false;
    }

    var txtstorename = $("#txtstorename").val();
    if ($.trim(txtstorename) === '') {
        ShowErrorMessage("Please enter store name.");
        $("#txtstorename").focus();
        return false;
    } var txtexpensedate = $("#txtexpensedate").val();
    if ($.trim(txtexpensedate) === '') {
        ShowErrorMessage("Please select expense date.");
        $("#txtexpensedate").focus();
        return false;
    }
    
    var txtbillamount = $("#txtbillamount").val();
    if ($.trim(txtbillamount) === '') {
        ShowErrorMessage("Please enter bill amount.");
        $("#txtbillamount").focus();
        return false;
    }

    var txtapprovedfrom = $("#txtapprovedfrom").val();
    if ($.trim(txtapprovedfrom) === '') {
        ShowErrorMessage("Please enter approved from.");
        $("#txtapprovedfrom").focus();
        return false;
    }
});

//----------------------------MedicineStatus----------------
var expenstatus = true;
function ChangeExpenseStatus(MedicineId, medstatus) {
    debugger;
    $("#hiddenexpenseid").val(MedicineId);
    $("#hiddenstatus").val(medstatus);
    $("#confirmmsg").html("Are you sure want to change the status of this expense?");
    $("#exampleModalCenterTitle").html("Expense Status");
    $("#ActionDelete").toggle();
    expenstatus = medstatus;
}
function ExpenseStatus() {
    var hiddenexpenseid = $("#hiddenexpenseid").val();
    $.ajax({
        type: "POST",
        url: "/Medicine/ExpenseStatus",
        data: { expenseid: hiddenexpenseid, status: expenstatus },
        success: function (result) {
            $("#exampleModalCenter").modal("hide");
            if (result.Status) {
                $("#expense_list").load(window.location.href + " #expense_list");
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
    $("#hiddenexpenseid").val(MedicineId);
    //$("#DeleteUser").modal('show');
    $("#confirmmsg").html("Are you sure want to delete this expense?");
    $("#exampleModalCenterTitle").html("Remove Expense");
    $("#ActionStatus").toggle();
}
function DeleteMedicine() {
    var expenseid = $("#hiddenexpenseid").val();
    $.ajax({
        type: "POST",
        url: "/Medicine/DeleteExpense",
        data: { expenseid: expenseid },
        success: function (result) {
            $("#exampleModalCenter").modal("hide");
            if (result.Status) {
                $("#expense_list").load(window.location.href + " #expense_list");
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
//----

function RemoveOpeningBalance(OpeningBalanceId) {
    debugger;
    $("#hiddenobid").val(OpeningBalanceId);
    $("#ismed").val(false);
}
function RemoveOpeningBalance1(OpeningBalanceId) {
    $("#hiddenobid").val(OpeningBalanceId);
    $("#ismed").val(true);
}
function DeleteOpeningBalance() {
    var openingbalance = $("#hiddenobid").val();
    var ismed = $("#ismed").val();
    $.ajax({
        type: "POST",
        url: "/Settings/DeleteOpeningBalance",
        data: { openingbalance: openingbalance, ismed: ismed },
        success: function (result) {
            $("#exampleModalCenterob").modal("hide");
            if (result.Status) {
                ShowSuccessMessage(result.SuccessMessage);
                location.reload();
            } else {
                ShowErrorMessage("An error occured.");
            }
        },
        error: function () {
            ShowErrorMessage("An error occured.");
        }
    });
}

function RemoveReceipt(TreatmentReceiptId) {
    debugger;
    $("#hiddenreceiptid").val(TreatmentReceiptId);
    $("#ismed1").val(false);
}
function RemoveReceipt1(MedicineReceiptId) {
    $("#hiddenreceiptid").val(MedicineReceiptId);
    $("#ismed1").val(true);
}
function DeleteReceipt() {
    var openingbalance = $("#hiddenreceiptid").val();
    var ismed = $("#ismed1").val();
    $.ajax({
        type: "POST",
        url: "/Settings/DeleteReceipt",
        data: { openingbalance: openingbalance, ismed: ismed },
        success: function (result) {
            $("#exampleModalCenterob").modal("hide");
            if (result.Status) {
                ShowSuccessMessage(result.SuccessMessage);
                location.reload();
            } else {
                ShowErrorMessage("An error occured.");
            }
        },
        error: function () {
            ShowErrorMessage("An error occured.");
        }
    });
}
