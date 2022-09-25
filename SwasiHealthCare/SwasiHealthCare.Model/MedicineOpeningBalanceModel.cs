using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class MedicineOpeningBalanceModel
    {
        public long? OpeningBalanceId { get; set; }
        public long? HospitalId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime Date { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public string Mode { get; set; }
    }
}
