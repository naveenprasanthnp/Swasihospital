using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class OBAndReceiptModel
    {
        public TreatmentOpeningBalanceModel TreatmentOpeningBalanceModel { get; set; }
        public MedicineOpeningBalanceModel MedicineOpeningBalanceModel { get; set; }
        public TreatmentReceiptModel TreatmentReceiptModel { get; set; }
        public MedicineReceiptModel MedicineReceiptModel { get; set; }
    }
}
