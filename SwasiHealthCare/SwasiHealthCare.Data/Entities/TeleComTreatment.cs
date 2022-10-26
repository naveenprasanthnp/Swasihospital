using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SwasiHealthCare.Data.Entities
{
    [Table("TeleComTreatment", Schema = "dbo")]
    public class TeleComTreatment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TeleComTreatmentId { get; set; }
        public string TeleComTreatmentName { get; set; }
        public bool TeleComTreatmentStatus { get; set; }
        public long? TeleComTreatmentCreatedBy { get; set; }
        public DateTime? TeleComTreatmentCreatedDate { get; set; }
        public long? TeleComTreatmentModifiedBy { get; set; }
        public DateTime? TeleComTreatmentModifiedDate { get; set; }
        public long? HospitalId { get; set; }
    }
}
