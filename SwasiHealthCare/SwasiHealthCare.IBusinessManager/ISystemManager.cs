using SwasiHealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.IBusinessManager
{
    public interface ISystemManager
    {
        Task<ResponseModel> AddNewSystem(SystemViewModel systemModel);
        Task<SystemViewModel> GetEditSystem(long systemid);
        Task<ResponseModel> DeleteSystem(long systemid);
        Task<ResponseModel> UpdateSystemStatus(long systemid, bool systemstatus, long modifiedby);
        Task<ResponseModel> GetAllSystem();
        Task<ResponseModel> AddNewPlan(PlanModel planmodel);
        Task<PlanModel> GetEditPlan(long planid);
        Task<ResponseModel> DeletePlan(long planid);
        Task<ResponseModel> UpdatePlanStatus(long planid, bool planstatus, long modifiedby);
        Task<ResponseModel> GetAllPlan();
        Task<ResponseModel> GetAllPatientPlanSubscription();
        Task<ResponseModel> GetAllPatientPlanSubscriptionDropdown();
        Task<ResponseModel> GetAllPatientPlanSubscriptionttt();
        Task<ResponseModel> AddPatientPlanSubscription(PatientPlanSubscriptionModel planmodel);
        Task<PatientPlanSubscriptionModel> GetEditPatientPlanSubscription(long patientplansubscriptionid);
        Task<ResponseModel> DeletePatientPlanSubscription(long patientplansubscriptionid);
        Task<ResponseModel> UpdatePatientPlanSubscriptionStatus(long patientplansubscriptionid, bool planstatus, long modifiedby);
        Task<ResponseModel> AddTreatmentOpeningBalance(TreatmentOpeningBalanceModel openingbalancemodel);
        Task<ResponseModel> AddMedicineOpeningBalance(MedicineOpeningBalanceModel openingbalancemodel);
        Task<ResponseModel> AddTreatmentReceipt(TreatmentReceiptModel openingbalancemodel);
        Task<ResponseModel> AddMedicineReceipt(MedicineReceiptModel openingbalancemodel);
        Task<TreatmentOpeningBalanceModel> GetEditTreatmentOpeningBalance(long openingbalance);
        Task<MedicineOpeningBalanceModel> GetEditMedicineOpeningBalance(long openingbalance);
        Task<ResponseModel> DeleteOpeningBalance(long openingbalance,bool ismed);
        Task<ResponseModel> DeleteReceipt(long openingbalance, bool ismed);
        Task<ResponseModel> GetAllTmtOpeningBalance();
        Task<ResponseModel> GetAllMedOpeningBalance();
        Task<ResponseModel> GetAllTmtReceipt();
        Task<ResponseModel> GetAllMedReceipt();
    }
}
