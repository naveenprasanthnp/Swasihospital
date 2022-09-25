using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class OPDConsoltationModel
    {
        public string ConsultationIDNumber { get; set; }
        public int PackageCount { get; set; }
        public long IDNumber { get; set; }
        public long? PlanId { get; set; }
        public long OPDConsultationId { get; set; }
        public long? PatientId { get; set; }
        public long? hiddenpatientid { get; set; }
        public long? DoctorId { get; set; }
        public DateTime ConsultationDate { get; set; }
        public DateTime? ConsultationTime { get; set; }
        public string DoctorName { get; set; }
        public string PatientIDNumber { get; set; }
        public string PatientName { get; set; }
        public string PatientGender { get; set; }
        public string PatientAge { get; set; }
        public string MobileNo { get; set; }
        public string PatientVisitType { get; set; }
        public string NatureOfIllness { get; set; }
        public string PatientHeight { get; set; }
        public string PatientWeight { get; set; }
        public string PatientTemp { get; set; }
        public string PatientBP { get; set; }
        public string PatientPulse { get; set; }
        public string PatientSpO2 { get; set; }
        public string PatientLMP { get; set; }
        public decimal TreatmentTotalServicesCharges { get; set; }
        public decimal TreatmentSplDiscount { get; set; }
        public decimal NetCharges { get; set; }
        public string TreatmentPaymentMode { get; set; }
        public string DoctorsNote { get; set; }
        public long? OPDConsoltationCreatedBy { get; set; }
        public DateTime? OPDConsultationCreatedDate { get; set; }
        public long? OPDConsultationModiifedBy { get; set; }
        public DateTime? OPDConsultationModifiedDate { get; set; }
        public OPDServicesModel opdservicesmodel { get; set; }
        public OPDPrescriptionModel opdprescriptionmodel { get; set; }
        public List<OPDServicesModel> opdserviceslist { get; set; }
        public List<OPDPrescriptionModel> opdprescriptionlist { get; set; }
        public string Mode { get; set; }
        public FilterModel filterModel { get; set; }
        public long? TherapistId { get; set; }
        public long? ServiceId { get; set; }
        public string TherapistName { get; set; }
        public long? HospitalId { get; set; }
        public decimal TotalCharges { get; set; }
    }
    public class FilterModel
    {
        public DateTime? FromDate { get; set; }
        public long? PatientId { get; set; }
        public DateTime? ToDate { get; set; }
        public long? HospitalId { get; set; }
        public long? TreatmentId { get; set; }
        public long? MedicineId { get; set; }
        public bool? IsDashboard { get; set; }
        public bool? IsSales { get; set; }
    }
}
