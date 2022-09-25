using SwasiHealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.IBusinessManager
{
    public interface ITreatmentManager
    {
        Task<ResponseModel> AddNewTreatment(TreatmentModel treatmentModel);
        Task<TreatmentModel> GetEditTreatment(long treatmentid);
        Task<ResponseModel> DeleteTreatment(long treatmentid);
        Task<ResponseModel> UpdateTreatmentStatus(long treatmentid, bool treatmentstatus, long modifiedby);
        Task<ResponseModel> GetAllTreatment();
        //Task<ResponseModel> GetAllTreatment1();
    }
}
