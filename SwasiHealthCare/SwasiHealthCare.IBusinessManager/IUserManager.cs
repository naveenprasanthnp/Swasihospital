using SwasiHealthCare.Data.Entities;
using SwasiHealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.IBusinessManager
{
    public interface IUserManager
    {
        Task<ResponseModel> SaveUser(UserModel userModel,long rolid);
        Task<ResponseModel> UpdateProfilePicture(UserModel userModel);
        Task<ResponseModel> CreateSuperAdmin(UserModel userModel);
        Task<IEnumerable<Roles>> GetRoles();
        Task<IEnumerable<Hospital>> GetHospitals();
        Task<UserModel> GetEditUser(long UserId);
        ResponseModel GetLoginUserDetails(long UserId);
        Task<ResponseModel> DeleteUser(long userid);
        Task<ResponseModel> UpdateUserStatus(long userid, bool userstatus, long modiifedby);
        Task<ResponseModel> GetAllUsers();
        Task<LoginResponseModel> UserSignIn(string username, string password, string publicip);
        Task<ResponseModel> ForgetPassword(string username);
        Task<ResponseModel> ChangePassword(string username, string password);
        Task<ResponseModel> AddNewNotes(NotesModel notesmodel);
        Task<ResponseModel> DeleteNotes(long notesid);
    }
}
