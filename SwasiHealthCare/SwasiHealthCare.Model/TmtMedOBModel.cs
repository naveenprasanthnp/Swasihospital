using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class TmtMedOBModel
    {
        public List<TreatmentOpeningBalanceModel> TreatmentOpeningBalanceModels { get; set; }
        public List<MedicineOpeningBalanceModel> MedicineOpeningBalanceModels { get; set; }
        public List<TreatmentReceiptModel> TreatmentReceiptModels { get; set; }
        public List<MedicineReceiptModel> MedicineReceiptModels { get; set; }
    }
}
