using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwasiHealthCare.Data.Entities
{
    [Table("TeleComMedicine", Schema = "dbo")]
    public class TeleComMedicine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TeleComMedicineId { get; set; }
        public string TeleComMedicineName { get; set; }
        public bool TeleComMedicineStatus { get; set; }
        public long? TeleComMedicineCreatedBy { get; set; }
        public DateTime? TeleComMedicineCreatedDate { get; set; }
        public long? TeleComMedicineModifiedBy { get; set; }
        public DateTime? TeleComMedicineModifiedDate { get; set; }
        public long? HospitalId { get; set; }
    }
}
