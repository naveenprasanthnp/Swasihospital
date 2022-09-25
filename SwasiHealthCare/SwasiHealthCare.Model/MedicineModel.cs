using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class MedicineModel
    {
        public long? MedicineId { get; set; }
        public long? MedicineCurrentStack { get; set; }
        public long? MedicinePurchaseStack { get; set; }
        public long? MedicineBalanceStack { get; set; }
        public DateTime Date { get; set; }
        public string MedicineName { get; set; }
        public string MedicineDescription { get; set; }
        public decimal MedicinePurchaseRate { get; set; }
        public decimal MedicineSalesRate { get; set; }
        public string MedicineManufacturer { get; set; }
        public DateTime MedicineExpiryDate { get; set; }
        //public long? MedicineStack { get; set; }
        //public long? MedicineBalance { get; set; }
        public bool MedicineStatus { get; set; }
        public long? MedicineCreatedBy { get; set; }
        public DateTime MedicineCreatedDate { get; set; }
        public long? MedicineModifiedBy { get; set; }
        public DateTime? MedicineModifiedDate { get; set; }
        public string Mode { get; set; }
        public long? HospitalId { get; set; }
        public long? CurrentStack { get; set; }
        public long? AvailableMedicineCount { get; set; }
    }
}
