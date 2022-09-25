$("#btnPurchaseMedicineSave").click(function (event) {
    var txtpurchsedate = $("#txtpurchsedate").val();
    if ($.trim(txtpurchsedate) === '') {
        ShowErrorMessage("Please select purchse date");
        $("#txtpurchsedate").focus();
        return false;
    }

    var txtdate = $("#txtmedicine").val();
    if ($.trim(txtdate) === '') {
        ShowErrorMessage("Please select medicine.");
        $("#txtdate").focus();
        return false;
    }

    var txtmedicineexpirydate = $("#txtmedicineexpirydate").val();
    if ($.trim(txtmedicineexpirydate) === '') {
        ShowErrorMessage("Please enter treatment expiry date.");
        $("#txtmedicineexpirydate").focus();
        return false;
    }

    //var txtmedicinedescription = $("#txtmedicinedescription").val();
    //if ($.trim(txtmedicinedescription) === '') {
    //    ShowErrorMessage("Please enter treatment duration.");
    //    $("#txtmedicinedescription").focus();
    //    return false;
    //}

    //var txtmedicinepurchaserate = $("#txtmedicinepurchaserate").val();
    //if ($.trim(txtmedicinepurchaserate) === '') {
    //    ShowErrorMessage("Please enter medicine purchase rate.");
    //    $("#txtmedicinepurchaserate").focus();
    //    return false;
    //}

    //var txtmedicinesalesrate = $("#txtmedicinesalesrate").val();
    //if ($.trim(txtmedicinesalesrate) === '') {
    //    ShowErrorMessage("Please enter medicine sales rate.");
    //    $("#txtmedicinesalesrate").focus();
    //    return false;
    //}

    //var txtmedicinemanufacturer = $("#txtmedicinemanufacturer").val();
    //if ($.trim(txtmedicinemanufacturer) === '') {
    //    ShowErrorMessage("Please enter medicine manufacturer.");
    //    $("#txtmedicinemanufacturer").focus();
    //    return false;
    //}

    //var txtmedicineexpirydate = $("#txtmedicineexpirydate").val();
    //if ($.trim(txtmedicineexpirydate) === '') {
    //    ShowErrorMessage("Please enter treatment charges.");
    //    $("#txtmedicineexpirydate").focus();
    //    return false;
    //}

    var txtmedicinecurrentstock = $("#txtmedicinecurrentstock").val();
    if ($.trim(txtmedicinecurrentstock) === '') {
        ShowErrorMessage("Please enter medicine current stack.");
        $("#txtmedicinecurrentstock").focus();
        return false;
    }

    var txtmedicinepurchasequanity = $("#txtmedicinepurchasequanity").val();
    if ($.trim(txtmedicinepurchasequanity) === '') {
        ShowErrorMessage("Please enter medicine purchase quatity.");
        $("#txtmedicinepurchasequanity").focus();
        return false;
    }

     var txtmedicinemanufacturer = $("#txtmedicinemanufacturer").val();
    if ($.trim(txtmedicinemanufacturer) === '') {
        ShowErrorMessage("Please enter medicine manufacturer.");
        $("#txtmedicinemanufacturer").focus();
        return false;
    }

    var txtpurchasecost = $("#txtpurchasecost").val();
    if ($.trim(txtpurchasecost) === '') {
        ShowErrorMessage("Please enter medicine purchase cost.");
        $("#txtpurchasecost").focus();
        return false;
    }
});

//-----------------------------------------------------

//---------------------RemoveUser-----------------------
function RemovePurchaseMedicine(PurchaseMedicineId) {
    $("#hiddenpurchasemedicineid").val(PurchaseMedicineId);
    $("#confirmmsg").html("Are you sure want to delete this medicine?");
    $("#exampleModalCenterTitle").html("Remove Medicine");
}

function DeletePurchaseMedicine() {
    var purchasemedicineid = $("#hiddenpurchasemedicineid").val();
    debugger;
    $.ajax({
        type: "POST",
        url: "/Medicine/PurchaseDeleteMedicine",
        data: { purchasemedicineid: purchasemedicineid },
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
//-----------------------------------------------------
$("#txtmedicine").change(function () {
    var medicineid = $("#txtmedicine").val();
    if (medicineid != '') {
        $.ajax({
            type: "GET",
            url: "/Medicine/GetMedicineById",
            data: { medicineid: medicineid },
            dataType: "json",
            success: function (result) {
                if (result != null) {
                    $("#txtmedicinecurrentstock").val(result.CurrentStack);
                    $("#hiddencurrentstock").val(result.CurrentStack);
                    $("#txtpurchasecost").val(result.MedicinePurchaseRate);
                    $("#txtmedicinemanufacturer").val(result.MedicineManufacturer);
                    $("#hiddencostamt").val(result.MedicinePurchaseRate);
                    $('#txtmedicinepurchasequanity').removeAttr('readonly', 'readonly')
                }
                else {
                    $("#txtmedicinecurrentstock").val('');
                    $("#txtpurchasecost").val('');
                    $("#txtmedicinemanufacturer").val('');
                    $("#hiddencostamt").val('');
                }
            },
            error: function (result) {
                ShowErrorMessage("An error occured.");
            }
        });
    } else {
        $("#txtmedicinecurrentstock").val('');
        $("#txtpurchasecost").val('');
        $("#txtmedicinemanufacturer").val('');
        $("#hiddencostamt").val('');
    }
});


$("#txtmedicinepurchasequanity").change(function () {
    debugger; 
    var v2 = $('#txtmedicinepurchasequanity').val();
    var v1 = $('#hiddencostamt').val();
    var v3 = v1 * v2;
    var c1 = $('#txtmedicinepurchasequanity').val();
    var c2 = $('#hiddencurrentstock').val();
   
    $('#txtmedicinecurrentstock').val(parseInt(c1) + parseInt(c2));
    $('#txtpurchasecost').val(v3);
});
