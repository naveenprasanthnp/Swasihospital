using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwasiHealthCare.Data.Entities
{
    [Table("MedicineReceipt", Schema = "dbo")]
    public class MedicineReceipt
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long MedicineReceiptId { get; set; }
        public long? ReceiptHospitalId { get; set; }
        public decimal? ReceiptAmount { get; set; }
        public DateTime ReceiptDate { get; set; }
        public DateTime? ReceiptCreatedDate { get; set; }
        public long? ReceiptCreatedBy { get; set; }
    }
}
