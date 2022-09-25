using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwasiHealthCare.Data.Entities
{
    [Table("OPDConsoltation", Schema = "dbo")]
    public class OPDConsoltation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OPDConsultationId { get; set; }
        public long IDNumber { get; set; }
        public long? PatientId { get; set; }
        public long? PlanId { get; set; }
        public long? DoctorId { get; set; }
        public DateTime ConsultationDate { get; set; }
        public DateTime? ConsultationTime { get; set; }
        public string DoctorName { get; set; }
        public string PatientIDNumber { get; set; }
        public string ConsultationIDNumber { get; set; }
        public string PatientName { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string MobileNo { get; set; }
        public string VisitType { get; set; }
        public string NatureOfIllness { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Temp { get; set; }
        public string BP { get; set; }
        public string Pulse { get; set; }
        public string SpO2 { get; set; }
        public string LMP { get; set; }
        public decimal TotalCharges { get; set; }
        public decimal SplDiscount { get; set; }
        public decimal NetCharges { get; set; }
        public string PaymentMode { get; set; }
        public string DoctorsNote { get; set; }
        public long? OPDConsultationCreatedBy { get; set; }
        public DateTime OPDConsultationCreatedDate { get; set; }
        public long? OPDConsultationModiifedBy { get; set; }
        public DateTime? OPDConsultationModifiedDate { get; set; }
        public long? HospitalId { get; set; }
    }
}
