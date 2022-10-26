using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwasiHealthCare.Data.Entities
{
    [Table("TeleComPatient", Schema = "dbo")]
    public class TeleComPatient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TeleComPatientId { get; set; }
        public string PatientIDNumber { get; set; }
        public string PatientName { get; set; }
        public string MobileNo { get; set; }
        public DateTime ConsultationDate { get; set; }
        public string DoctorName { get; set; }
        public string PatientPrimaryComplaints { get; set; }
        public string Treatment { get; set; }
        public string Medicine { get; set; }
        public string IsClientComingProperly { get; set; }
        public string ReasonforNotComing { get; set; }
        public string OtherReasons { get; set; }
        public DateTime TreatmentDate { get; set; }
        public long? TeleComTreatmentCreatedBy { get; set; }
        public DateTime? TeleComTreatmentCreatedDate { get; set; }
        public long? TeleComTreatmentModifiedBy { get; set; }
        public DateTime? TeleComTreatmentModifiedDate { get; set; }
        public long? HospitalId { get; set; }
    }
}
