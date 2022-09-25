using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class MedicineReceiptModel
    {
        public long MedicineReceiptId { get; set; }
        public long? ReceiptHospitalId { get; set; }
        public decimal? ReceiptAmount { get; set; }
        public DateTime ReceiptDate { get; set; }
        public DateTime? ReceiptCreatedDate { get; set; }
        public long? ReceiptCreatedBy { get; set; }
        public string Mode { get; set; }
    }
}
