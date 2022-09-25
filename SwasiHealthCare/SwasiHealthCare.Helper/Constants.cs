using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Helper
{
    public class Constants
    {
        public const string UNAUTHORIZED_ACCESS = "Unauthorized Access.";
        public const string AccessDenied = "Access denied";
        public const string AlreadyExist = "User name already exist";
        public const string NoData = "No Data Available";
        public const string PasswordDenied = "Please enter the correct password";
        public const string UserInactive = "Your access denied, please contact your application administrator";
        public const string InvalidPassword = "Password required 5 to 12 characters including numbers, punctuation and special characters.";
        public const string SetNewPasswordFail = "You are not able to set the new password. Please contact your application administrator";
        public const string SetNewPasswordPass = "New Password has been updated successfully";
        public const string PasswordInvalid = "Please enter the correct Password";
        public const string UserNotFound = "User not found";
        public const string ChangePasswordSuccess = "Your new password has been updated successfully";
        public const string UserNameNotFound = "Please enter your email address to login to the application";
        public const string UserPassword = "Your Password has been send to your registered Email Address (or) Mobile Number";
        public const string SuccessMessage = "Success";
        public const string RoleNotfound = "Role is mandatory.";

        /****************Encryption Key******************/
        public const string EncryptKey = "SwasiHealthCare@123";
        /****************Encryption Key******************/

        /****************User******************/
        public const string DelSuccessMessage = "User deleted / removed permanently ";
        public const string FirstNameMandatory = "First Name is mandatory";
        public const string UserDuplicate = "User already exists.";
        public const string UserCreated = "User created successfully.";
        public const string UserUpdated = "User updated successfully.";
        public const string UserStatusChange = "User status updated successfully.";
        public const string UserProfileChange = "Your profile has been updated successfully.";
        public const string ProfileNotFound = "Please upload your profile image.";
        public const string ProfileValid = "Only JPEG / PNG images are allowed.";
        public const string ProfileValidFile = "Please upload valid image.";
        /****************User******************/

        /****************Role******************/
        public const string RoleDuplicate = "Role already exists.";
        public const string RoleCreated = "Role created successfully.";
        public const string RoleUpdated = "Role updated successfully.";
        public const string ReferInUsers = "This role is referred in User(s).";
        public const string DelRoleSuccessMessage = "Role deleted / removed permanently ";
        public const string RoleStatusChange = "Role status updated successfully.";
        public const string RolePrivilegeSuccessMessage = "Role Privilege saved successfully.";
        public const string RolePrivilegeNotfound = "Role Privilege is mandatory.";
        /****************Role******************/
    }
}
