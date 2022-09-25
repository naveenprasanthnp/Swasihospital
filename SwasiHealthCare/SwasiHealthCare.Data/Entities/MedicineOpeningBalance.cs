using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwasiHealthCare.Data.Entities
{
    [Table("MedicineOpeningBalance", Schema = "dbo")]
    public class MedicineOpeningBalance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OpeningBalanceId { get; set; }
        public long? HospitalId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime Date { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
    }
}
