using SwasiHealthCare.Data.Entities;
using SwasiHealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.IBusinessManager
{
    public interface IPatientManager
    {
        Task<PatientResponseModel> SavePatient(PatientModel patientModel);
        Task<PatientResponseModel> SavePatientDocumnet(PatientDocument patientDocument);
        Task<OPDConsoltationResponseModel> SaveOPDConsoltation(OPDConsoltationModel opdconsoltationmodel);
        Task<ResponseModel> UpdateOPDTotalAmount(OPDConsoltationModel opdconsoltationmodel);
        Task<PatientModel> GetEditPatient(long PatientId);
        Task<ResponseModel> GetDocumentById(long PatientId);
        Task<ResponseModel> DeletePatient(long Patientid);
        Task<ResponseModel> DeleteOPDService(long opdserviceid);
        Task<ResponseModel> DeleteOPDPrescription(long opdprescriptionid); 
         Task<OPDServicesModel> EditOPDService(long opdserviceid);
        Task<OPDPrescriptionModel> EditOPDPrescription(long opdprescriptionid); 
        Task<ResponseModel> UpdatePatientStatus(long patientid, bool patientstatus, long modifiedby);
        Task<ResponseModel> GetAllPatients();
        Task<ResponseModel> GetAllNotes();
        long? GetPatientsCount(long hospitalid);
        Task<Dashboard> GetOPDServiceCount(long hospitalid);
        long? GetPatientsCountCurrentMonth(long hospitalid);
        long? GetPatientsCountHospital(long hospitalid);
        long? GetOPDCount(long hospitalid,bool? isall);
        Task<decimal?> GetAllOPDServicePrescription(DateTime? FromDate, DateTime? ToDate,long? hospitalid, bool isall);
        Task<ResponseModel> GetAllOPDs(DateTime? FromDate, DateTime? ToDate);
        Task<ResponseModel> GetAllOPDsCommonSearch(DateTime? FromDate, DateTime? ToDate, long hospitalid);
        Task<List<OPDReportModel>> GetOPDReport(DateTime? FromDate, DateTime? ToDate,long? HospitalId, FilterModel FilterModel); 
        Task<ResponseModel> GetAllPatientList(long? PatientId,DateTime? FromDate, DateTime? ToDate,long? HospitalId);
        PatientDocumentModel GetDocument(long documentid);
        Task<ResponseModel> GetAllOpdService();
        Task<ResponseModel> GetAllOpdPrescription();
        Task<ResponseModel> SaveOPDService(OPDServicesModel opdservicemodel);
        Task<ResponseModel> SaveOPDPrescription(OPDPrescriptionModel opdprescriptionmodel);
    }
}
