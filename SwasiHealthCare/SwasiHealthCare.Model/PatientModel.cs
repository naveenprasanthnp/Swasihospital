using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class PatientModel
    {
        //BasicDetails
        public long? PatientId { get; set; }
        public DateTime PatientDate { get; set; }
        public string PatientIdNumber { get; set; }
        public long PlanId { get; set; }
        public string PatientName { get; set; }
        public string PatientGender { get; set; }
        public long PatientAge { get; set; }
        public string PatientAddress { get; set; }
        public string PatientMobileNumber { get; set; }
        public string PatientWhatsappNumber { get; set; }
        public string PatientEducation { get; set; }
        public string PatientOccupation { get; set; }
        public string PatientMaritalStatus { get; set; }
        public string PatientPrimaryComplaints { get; set; }
        public long? PatientDoctorId { get; set; }
        public string PatientDoctorName { get; set; }
        public string PatientAssociateComplaints { get; set; }
        public string PatientHistoryOfPresentIllness { get; set; }
        public string PatientHistoryOfSurgery { get; set; }
        public string PatientFamilyHistory { get; set; }
        //Personal History
        public string PatientColor { get; set; }
        public string PatientHunger { get; set; }
        public string PatientHeight { get; set; }
        public string PatientWeight { get; set; }
        public string PatientTaste { get; set; }
        public string PatientEmotion { get; set; }
        public string PatientMotion { get; set; }
        public string PatientUrine { get; set; }
        public string PatientBP { get; set; }
        public string PatientSugar { get; set; }
        public string PatientTemperature { get; set; }
        //Physical Examination
        public string PatientPulse { get; set; }
        public string PatientPalpitation { get; set; }
        public string PatientTongue { get; set; }
        public string PatientFoot { get; set; }
        public string PatientEye { get; set; }
        //Investigation
        public string PatientBloodReport { get; set; }
        public string PatientUrineReport { get; set; }
        public string PatientSputum { get; set; }
        public string PatientCSF { get; set; }
        public string PatientXRay { get; set; }
        public string PatientCT { get; set; }
        public string PatientMRI { get; set; }
        public string PatientOthers { get; set; }
        public string FilePath { get; set; }
        public bool PatientIsDelete { get; set; }
        //-------------
        public string PatientDiagnosis { get; set; }
        public string PatientTreatment { get; set; }
        public string PatientDiet { get; set; }
        public string PatientPrescription { get; set; }
        public long? PatientCreatedBy { get; set; }
        public DateTime PatientCreatedDate { get; set; }
        public long? PatientModiifedBy { get; set; }
        public DateTime? PatientModifiedDate { get; set; }
        public string Mode { get; set; }
        public bool PatientStatus { get; set; }
        public long? PatientHospitalId { get; set; }
        public string Flag { get; set; }
        public List<PatientDocumentModel> PatientDocumentList { get; set; }
        public long? PatientRegisterNumber { get; set; }
        public FilterModel filterModel { get; set; }
        public string HospitalName { get; set; }
        public string HistoryOfTreatment { get; set; }
        public string OBGHistory { get; set; }
        public string VatajaDisorder { get; set; }
        public string PittajaDisorder { get; set; }
        public string KaphajaDisorder { get; set; }
        public string Notes { get; set; }
    }
}
