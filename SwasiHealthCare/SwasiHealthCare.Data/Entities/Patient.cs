using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SwasiHealthCare.Data.Entities
{
    [Table("Patient", Schema = "dbo")]
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? PatientId { get; set; }
        public DateTime PatientDate { get; set; }
        public string PatientIDNumber { get; set; }
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
        public string PatientAssociateComplaints { get; set; }
        public string PatientHistoryOfPresentIllness { get; set; }
        public string PatientHistoryOfSurgery { get; set; }
        public string PatientFamilyHistory { get; set; }
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
        public string PatientPulse { get; set; }
        public string PatientPalpitation { get; set; }
        public string PatientTongue { get; set; }
        public string PatientFoot { get; set; }
        public string PatientEye { get; set; }
        public string PatientBloodReport { get; set; }
        public string FilePath { get; set; }
        public string PatientUrineReport { get; set; }
        public string PatientSputum { get; set; }
        public string PatientCSF { get; set; }
        public string PatientXRay { get; set; }
        public string PatientCT { get; set; }
        public string PatientMRI { get; set; }
        public string PatientOthers { get; set; }
        public bool PatientStatus { get; set; }
        public bool PatientIsDelete { get; set; }
        public string PatientDiagnosis { get; set; }
        public string PatientTreatment { get; set; }
        public string PatientDiet { get; set; }
        public string PatientPrescription { get; set; }
        public long? PatientCreatedBy { get; set; }
        public DateTime PatientCreatedDate { get; set; }
        public long? PatientModiifedBy { get; set; }
        public long? PatientHospitalId { get; set; }
        public DateTime? PatientModifiedDate { get; set; }
        public long? PatientRegisterNumber { get; set; }
        public long? PatientDoctorId { get; set; }
        public string PatientDoctorName { get; set; }
        public string HistoryOfTreatment{ get; set; }
        public string OBGHistory { get; set; }
        public string VatajaDisorder { get; set; }
        public string PittajaDisorder { get; set; }
        public string KaphajaDisorder { get; set; }
    }
}
