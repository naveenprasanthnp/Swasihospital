using SwasiHealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.IBusinessManager
{
    public interface IAccessRightManager
    {
        Task<ResponseModel> GetAllMenu();
        Task<ResponseModel> GetAllAccessRights(long? userid);
        Task<ResponseModel> AddAccessRights(List<AccessRightsModel> accessRightsModels,long? hospitalid);
    }
}
