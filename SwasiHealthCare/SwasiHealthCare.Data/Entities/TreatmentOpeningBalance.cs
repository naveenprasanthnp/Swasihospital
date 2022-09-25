using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwasiHealthCare.Data.Entities
{
    [Table("TreatmentOpeningBalance", Schema = "dbo")]
    public class TreatmentOpeningBalance
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
