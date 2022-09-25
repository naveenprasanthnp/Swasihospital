using SwasiHealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SwasiHealthCare.IBusinessManager
{
    public interface IHospitalManager
    {
        Task<ResponseModel> AddHospital(HospitalModel hospitalModel);
        Task<HospitalModel> GetEditHospital(long hospitalid);
        Task<ResponseModel> DeleteHospital(long hospitalid);
        Task<ResponseModel> UpdateHospitalStatus(long hospitalid, bool hospitalstatus, long modifiedby);
        Task<ResponseModel> GetAllHospital();
    }
}
