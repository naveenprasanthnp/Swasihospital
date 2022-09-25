using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class TreatmentRevenueReportModel
    {
        public DateTime Date { get; set; }
        public string  Particulars { get; set; }
        public string Details { get; set; }
        public decimal? OpeningBalance { get; set; }
        public decimal? Income { get; set; }
        public decimal? Total { get; set; }
        public decimal? Receipt { get; set; }
        public decimal? Expense { get; set; }
        public decimal? CompanyExpense { get; set; }
        public decimal? Profit { get; set; }
        public bool? IsCompanyExpense { get; set; }
        //public decimal? TempAmount { get; set; }
        public long? CompanyId { get; set; }
    }
}
