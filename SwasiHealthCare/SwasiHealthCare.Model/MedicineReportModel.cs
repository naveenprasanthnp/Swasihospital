using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class MedicineReportModel
    {
        public List<PurchaseMedicineModel> medicinelist { get; set; }
        public FilterModel filterModel { get; set; }
        public List<SalesReport> salesreports { get; set; }
    }
    public class SalesReport
    {
        public DateTime Date { get; set; }
        public string PatientName { get; set; }
        public string MobileNo { get; set; }
        public string MedicineName { get; set; }
        public long Quantity { get; set; }
        public decimal Rate { get; set; }
        public string ModeofPayment { get; set; }
        public string OpdNumber { get; set; }
    }
}
