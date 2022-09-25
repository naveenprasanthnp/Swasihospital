using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwasiHealthCare.Data.Entities
{
    [Table("TreatmentReceipt", Schema = "dbo")]
    public class TreatmentReceipt
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TreatmentReceiptId { get; set; }
        public long? ReceiptHospitalId { get; set; }
        public decimal? ReceiptAmount { get; set; }
        public DateTime ReceiptDate { get; set; }
        public DateTime? ReceiptCreatedDate { get; set; }
        public long? ReceiptCreatedBy { get; set; }
    }
}
