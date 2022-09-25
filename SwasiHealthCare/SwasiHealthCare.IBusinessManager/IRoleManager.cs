using SwasiHealthCare.Data.Entities;
using SwasiHealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.IBusinessManager
{
    public interface IRoleManager
    {
        Task<IEnumerable<Roles>> GetRolesList(string sortby, string sortvalue, long roleid);
        Task<ResponseModel> SaveRoles(RolesModel role);
        Task<ResponseModel> RemoveRoles(long roleid);
        Task<ResponseModel> GetRoleById(long roleid);
        Task<ResponseModel> UpdateRoleStatus(long roleid, bool rolestatus, long modifiedby);
    }
}
