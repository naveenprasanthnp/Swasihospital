using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwasiHealthCare.Data.Entities
{
    [Table("Medicine", Schema = "dbo")]
    public class Medicine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long MedicineId { get; set; }
        public string MedicineName { get; set; }
        public DateTime Date { get; set; }
        public string MedicineDescription { get; set; }
        public decimal MedicinePurchaseRate { get; set; }
        public decimal MedicineSalesRate { get; set; }
        public string MedicineManufacturer { get; set; }
        public DateTime MedicineExpiryDate { get; set; }
        public long? MedicineCurrentStack { get; set; }
        public long? MedicinePurchaseStack { get; set; }
        public long? MedicineBalanceStack { get; set; }
        public bool MedicineStatus { get; set; }
        public long? MedicineCreatedBy { get; set; }
        public DateTime? MedicineCreatedDate { get; set; }
        public long? MedicineModifiedBy { get; set; }
        public DateTime? MedicineModifiedDate { get; set; }
        public long? HospitalId { get; set; }
    }
}
