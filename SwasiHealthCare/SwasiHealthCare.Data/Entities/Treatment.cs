using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SwasiHealthCare.Data.Entities
{
    [Table("Treatment", Schema = "dbo")]
    public class Treatment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TreatmentId { get; set; }
        public string TreatmentName { get; set; }
        public string TreatmentDuration { get; set; }
        public string TreatmentDescription { get; set; }
        public string TreatmentMedicineNeeded { get; set; }
        public decimal? TreatmentCharges { get; set; }
        public bool TreatmentStatus { get; set; }
        public long? TreatmentCreatedBy { get; set; }
        public DateTime TreatmentCreatedDate { get; set; }
        public long? TreatmentModifiedBy { get; set; }
        public DateTime? TreatmentModifiedDate { get; set; }
        public long? HospitalId { get; set; }
    }
}
