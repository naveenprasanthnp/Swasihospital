//----------Login------------------
$("#btnLogin").click(function (event) {
    var email = $("#txtemail").val();
    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    if ($.trim(email) === '') {
        ShowErrorMessage("Please enter username.");
        debugger;
        $("#txtemail").focus();
        return false;
    }
    else if (!emailReg.test(email)) {
        ShowErrorMessage("Enter a valid email.");
        $("#txtemail").focus();
        return false;
    }

    var password = $("#txtuserpassword").val();
    if ($.trim(password) === '') {
        ShowErrorMessage("Please enter password.");
        $("#txtuserpassword").focus();
        return false;
    }

    //var data = $("#loginform").serialize();
    //$.ajax({
    //    type: "POST",
    //    url: "/Home/Login",
    //    data: data,
    //    success: function (result) {
    //        if (result.Status === true) {
    //            debugger;
    //            window.location.href = "/Home/Index";
    //            ShowSuccessMessage("Login Successfully");
    //        }
    //        else {
    //            ShowErrorMessage(result.ErrorMessage);
    //            setTimeout(location.reload(), 2000);
    //        }
    //    }
    //});
});
//-----------------------------------------------------

//------------------ForgotPassword---------------------
$("#btnForgotLoginSubmit").click(function (event) {
    var email = $("#textUserEmail").val();
    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;;
    if ($.trim(email) === '') {
        ShowErrorMessage("Please enter email.");
        $("#textUserEmail").focus();
        return false;
    }
    else if (!emailReg.test(email)) {
        ShowErrorMessage("Enter a valid email.");
        $("#textUserEmail").focus();
        return false;
    }

    var data = $("#forgotpasswordform").serialize();
    $.ajax({
        type: "POST",
        url: "/Home/ForgotPassword",
        data: data,
        success: function (result) {
            if (result.Status === true) {
                ShowSuccessMessage(result.SuccessMessage);
                window.location.href = "/Home/Login";
            }
            else {
                ShowErrorMessage(result.ErrorMessage);
                location.reload();
                return false;
            }
        }
    });
});
//-----------------------------------------------------

//-----------------------------------------------------
$("#btnChangePassword").click(function (event) {
    var oldpassword = $("#txtoldpassword").val();
    if ($.trim(oldpassword) === '') {
        ShowErrorMessage("Please enter current password.");
        $("#txtoldpassword").focus();
        return false;
    }
    var password = $("#txtpassword").val();
    if ($.trim(password) === '') {
        ShowErrorMessage("Please enter password.");
        $("#txtpassword").focus();
        return false;
    }
    var confrmpassword = $("#txtconfrmpassword").val();
    if ($.trim(confrmpassword) === '') {
        ShowErrorMessage("Please enter confirm password.");
        $("#txtconfrmpassword").focus();
        return false;
    }
    if (password !== confrmpassword) {
        ShowErrorMessage("New password and confirm password do not match.");
        $("#txtconfrmpassword").focus();
        return false;
    }

    var data = $("#changepasswordform").serialize();
    $.ajax({
        type: "POST",
        url: "/Home/ChangePassword",
        data: data,
        success: function (result) {
            if (result.Status === true) {
                ShowSuccessMessage(result.SuccessMessage);
                window.location.href = "/Home/Login";
            }
            else {
                ShowErrorMessage(result.ErrorMessage);
                location.reload();
                return false;
            }
        }
    });
});
//-----------------------------------------------------

//----------------------SaveUser--------------------
$("#btnUserSave").click(function (event) {
    debugger;
    var roleid = $("#role").val();
    var hospital = $("#txthospitalid").val();
    if ($.trim(hospital) === '') {
        ShowErrorMessage("Please select hospital.");
        $("#txthospitalid").focus();
        return false;
    }
    if (roleid != 1) {
        var txtsystemid = $("#txtsystemid").val();
        if ($.trim(txtsystemid) === '') {
            ShowErrorMessage("Please select system ip.");
            $("#txtsystemid").focus();
            return false;
        }
    }
    var firstname = $("#txtfirstname").val();
    var regex = /^([\s.]?[a-zA-Z ]+)+$/;
    if ($.trim(firstname) === '') {
        ShowErrorMessage("Please Enter First Name.");
        $("#txtfirstname").focus();
        return false;
    }
    else if (!regex.test(firstname)) {
        ShowErrorMessage("First Name should not be allowed numbers.");
        $("#txtfirstname").focus();
        return false;
    }
    var lastname = $("#txtlastname").val();
    if ($.trim(lastname) === '') {
        ShowErrorMessage("Please Enter Last Name.");
        $("#txtlastname").focus();
        return false;
    }
    else if (!regex.test(lastname)) {
        ShowErrorMessage("Last Name should not be allowed numbers.");
        $("#txtlastname").focus();
        return false;
    }
    var email = $("#txtemail").val();
    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    if ($.trim(email) === '') {
        ShowErrorMessage("Please Enter Email.");
        $("#txtemail").focus();
        return false;
    }
    else if (!emailReg.test(email)) {
        ShowErrorMessage("Enter a valid email address.");
        $("#txtemail").focus();
        return false;
    }
    var gender = $("#txtgender").val();
    if ($.trim(gender) === 'Select Gender') {
        ShowErrorMessage("Please Select Gender.");
        $("#txtgender").focus();
        return false;
    }
    var mobilePhoneInput = $("#txtmobilenumber").val();
    if ($.trim(mobilePhoneInput) === '') {
        ShowErrorMessage("Please Enter Mobile Number.");
        $("#txtmobilePhoneInput").focus();
        return false;
    } else {
        if ($.trim(mobilePhoneInput).length !== 10) {
            ShowErrorMessage("Please enter valid mobile number.");
            return false;
        }
    }
    var dateOfBirthInput = $("#txtdateofbirth").val();
    if ($.trim(dateOfBirthInput) === '') {
        ShowErrorMessage("Please Enter DateOfBirth.");
        $("#txtdateofbirth").focus();
        return false;
    }
    //else if (dateOfBirthInput > dateOfJoinInput) {
    //    ShowErrorMessage("DateOfJoin must be greater than DateofBirth.");
    //    $("#txtdateofjoin").focus();
    //    return false;
    //}
    var dateOfJoinInput = $("#txtdateofjoin").val();
    if ($.trim(dateOfJoinInput) === '') {
        ShowErrorMessage("Please Enter DateOfJoin.");
        $("#txtdateofjoin").focus();
        return false;
    }
    //if (dateOfBirthInput && dateOfJoinInput && dateOfBirthInput > dateOfJoinInput) {
       // ShowErrorMessage("DateofBirth must be less than DateOfJoin.");
       // $("#txtdateofbirth").focus();
       // return false;
   // }
    //if (dateOfJoinInput < dateOfBirthInput) {
    //    debugger;
    //    ShowErrorMessage("DateofBirth must be less than DateOfJoin.");
    //    $("#txtdateofbirth").focus();
    //    return false;
    //}
    var useraddress = $("#txtuseraddress").val();
    if ($.trim(useraddress) === '') {
        ShowErrorMessage("Please Enter Address.");
        $("#txtuseraddress").focus();
        return false;
    }

    if (roleid != 1)
    {
        var roletype = $("#txtroletypes").val();
    if ($.trim(roletype) === '') {

        ShowErrorMessage("Please Select Roles.");
        $("#txtroletypes").focus();
        return false;
    }
        if ($("#txtroletypes").val() != "3" ) {
        var userdesignation = $("#txtdesignation").val();
        if ($.trim(userdesignation) === '') {
            ShowErrorMessage("Select designation.");
            $("#txtdesignation").focus();
            return false;
        }
    }
    if ($("#txtroletypes").val() == "3") {
        var qualification = $("#txtqualification").val();
        if ($.trim(qualification) === '') {
            ShowErrorMessage("Please Enter Qualification.");
            $("#txtqualification").focus();
            return false;
        }
        var specialization = $("#txtspecialization").val();
        if ($.trim(specialization) === '') {
            ShowErrorMessage("Please Enter Specialization.");
            $("#txtspecialization").focus();
            return false;
        }
    }
}
    var password = $("#txtpassword").val();
    if ($.trim(password) === '') {
        ShowErrorMessage("Please Enter Password.");
        $("#txtpassword").focus();
        return false;
    }
    var confrmpassword = $("#txtconfrmpassword").val();
    if ($.trim(confrmpassword) === '') {
        ShowErrorMessage("Please Enter Confirm Password.");
        $("#txtconfrmpassword").focus();
        return false;
    }
    if (password !== confrmpassword) {
        ShowErrorMessage("passwords do not match.");
        $("#txtconfrmpassword").focus();
        return false;
    }
    
});

//-----------------------------------------------------

//----------------------------UserStatus----------------
var userstatus = true;
function ChangeUserStatus(UserId, usrstatus) {
    $("#hiddenuserid").val(UserId);
    $("#hiddenstatus").val(usrstatus);
    $("#confirmmsg").html("Are you sure want to change the status of this staff?");
    $("#exampleModalCenterTitle").html("Staff Status");
   
    $("#ActionDelete").toggle();
    userstatus = usrstatus;
}
function UserStatus() {
    var userid = $("#hiddenuserid").val();
    $.ajax({
        type: "POST",
        url: "/User/UserStatus",
        data: { userid: userid, status: userstatus },
        success: function (result) {
            $("#exampleModalCenter").modal("hide");
            if (result.Status) {
                $("#user_list").load(window.location.href + " #user_list");
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
function RemoveUser(UserId) {
   
    $("#hiddenuserid").val(UserId);
    $("#confirmmsg").html("Are you sure want to delete this staff?");
    $("#exampleModalCenterTitle").html("Remove Staff");
    $("#ActionStatus").toggle();
}
function DeleteUser() {
    var usrId = $("#hiddenuserid").val();
    $.ajax({
        type: "POST",
        url: "/User/DeleteUser",
        data: { UserId: usrId },
        success: function (result) {
            $("#exampleModalCenter").modal("hide");
            if (result.Status) {
                $("#user_list").load(window.location.href + " #user_list");
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
$(document).ready(function () {
    $('#check').click(function () {
        if (document.getElementById('check').checked) {
            $('#txtuserpassword').get(0).type = 'text';
        } else {
            $('#txtuserpassword').get(0).type = 'password';
        }
    });
});

var isedit = false;
$("#isedit").click(function (event) {
    var roleid = $("#RoleId").val();
    $("#txtxeditmobile").attr("readonly", false);  
    $("#txtConfirmNewpassword").attr("readonly", false);  
    $("#txtNewPassword").attr("readonly", false);
        $("#btnuserprofilesave").removeAttr("disabled");
    if (roleid == 1) {
        $("#txtdesignationname").show();
        $("#superadmindesign").hide();
    }
    isedit = true;
});

$("#btnuserprofilesave").click(function (event) {
    if (isedit == true) {
        var txtxeditmobile = $("#txtxeditmobile").val();
        if ($.trim(txtxeditmobile) === '') {
            ShowErrorMessage("Please enter mobile number.");
            $("#txtxeditmobile").focus();
            return false;
        } else {
            if ($.trim(txtxeditmobile).length !== 10)
            {
                ShowErrorMessage("Please enter valid mobile number.");
                return false;
            }
        }
        var txtNewPassword = $("#txtNewPassword").val();
        if ($.trim(txtNewPassword) === '') {
            ShowErrorMessage("Please Enter Password.");
            $("#txtNewPassword").focus();
            return false;
        }
        var txtConfirmNewpassword = $("#txtConfirmNewpassword").val();
        if ($.trim(txtConfirmNewpassword) === '') {
            ShowErrorMessage("Please Enter Confirm Password.");
            $("#txtConfirmNewpassword").focus();
            return false;
        }
        if (txtNewPassword !== txtConfirmNewpassword) {
            ShowErrorMessage("passwords do not match.");
            $("#txtconfrmpassword").focus();
            return false;
        }
    }
});