using SwasiHealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.IBusinessManager
{
    public interface IMedicineManager
    {
        Task<ResponseModel> AddNewMedicine(MedicineModel medicineModel);
        Task<MedicineModel> GetEditMedicine(long medicineid);
        Task<ResponseModel> DeleteMedicine(long medicineid);
        Task<ResponseModel> UpdateMedicineStatus(long medicineid, bool medicinestatus, long modifiedby);
        Task<ResponseModel> GetAllMedicine(DateTime? FromDate, DateTime? ToDate,long? hospitalid);
        Task<ResponseModel> AddNewPurchaseMedicine(PurchaseMedicineModel medicineModel);
        Task<PurchaseMedicineModel> GetEditPurchaseMedicine(long purchasemedicineid);
        Task<ResponseModel> DeletePurchaseMedicine(long purchasemedicineid);
        Task<ResponseModel> GetAllPurchaseMedicine(DateTime? FromDate, DateTime? ToDate, long? hospitalid);
        Task<ResponseModel> AddNewExpense(ExpenseModel expenseModel);
        Task<ExpenseModel> GetEditExpense(long expenseid);
        Task<ResponseModel> DeleteExpense(long expenseid);
        Task<ResponseModel> UpdateExpenseStatus(long expenseid, bool expensestatus, long modifiedby);
        Task<ResponseModel> GetAllExpense();
        Task<List<MedicineRevenueReportModel>> GetAllMedicineReport(long hospitalid);
        Task<List<TreatmentRevenueReportModel>> GetAllTreatmentReport(long hospitalid);
        Task<List<SalesReport>> GetAllSalesReport(DateTime? FromDate, DateTime? ToDate,long? hospitalid);
    }
}
