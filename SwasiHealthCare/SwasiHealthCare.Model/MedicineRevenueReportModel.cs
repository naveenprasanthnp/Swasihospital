using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class MedicineRevenueReportModel
    {
        public DateTime Date { get; set; }
        public string Particulars { get; set; }
        public string Details { get; set; }
        public decimal? OpeningBalance { get; set; }
        public decimal? Income { get; set; }
        public decimal? Total { get; set; }
        public decimal? PurchaseMedicine { get; set; }
        public decimal? Profit { get; set; }
        public decimal? Receipt { get; set; }
        public long? HospitalId { get; set; }
    }
}
