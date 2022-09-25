using SwasiHealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.IBusinessManager
{
    public interface IDesignationManager
    {
        Task<ResponseModel> AddNewDesignation(DesignationModel designationModel);
        Task<DesignationModel> GetEditDesignation(long designationid);
        Task<ResponseModel> DeleteDesignation(long designationid);
        Task<ResponseModel> UpdateDesignationStatus(long designationid, bool designationstatus, long modifiedby);
        Task<ResponseModel> GetAllDesignation();
    }
}
