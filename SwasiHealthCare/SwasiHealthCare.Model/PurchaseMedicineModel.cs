using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class PurchaseMedicineModel
    {
        public long PurchaseMedicineId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string MedicineName { get; set; }
        public long? MedicineId { get; set; }
        public DateTime? MedicineExpiryDate { get; set; }
        public long? MedicineCurrentStock { get; set; }
        public long? MedicinePurchaseQuanity { get; set; }
        public string MedicineManufacturer { get; set; }
        public decimal PurchaseCost { get; set; }
        //public long? NewStock { get; set; }
        //public decimal PurchaseRate { get; set; }
        public long? PurchaseMedicineCreatedBy { get; set; }
        public DateTime? PurchaseMedicineCreatedDate { get; set; }
        public long? PurchaseMedicineModifiedBy { get; set; }
        public DateTime? PurchaseMedicineModifiedDate { get; set; }
        public long? HospitalId { get; set; }
        public string Mode { get; set; }
        public decimal MedicineRate { get; set; }
    }
}
