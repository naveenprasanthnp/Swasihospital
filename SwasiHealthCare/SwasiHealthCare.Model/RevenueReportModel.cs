using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class RevenueReportModel
    {
        public FilterModel filterModel { get; set; }
        public List<TreatmentRevenueReportModel> treatmentrevenue { get; set; }
        public List<MedicineRevenueReportModel> medicinerevenue { get; set; }
    }
}
