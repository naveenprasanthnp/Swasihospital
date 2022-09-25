using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SwasiHealthCare.Model
{
    public class ExpenseModel
    {
        public long? ExpenseId { get; set; }
        public string Description { get; set; }
        public string StoreName { get; set; }
        public decimal BillAmount { get; set; }
        public string ApprovedFrom { get; set; }
        public HttpPostedFileBase file { get; set; }
        public string BillCopyFileName { get; set; }
        public bool ExpenseStatus { get; set; }
        public long? ExpenseCreatedBy { get; set; }
        public DateTime ExpenseCreatedDate { get; set; }
        public long? ExpenseModifiedBy { get; set; }
        public DateTime? ExpenseModifiedDate { get; set; }
        public string Mode { get; set; }
        public long? HospitalId { get; set; }
        public DateTime ExpenseDate { get; set; }
        public bool IsCompanyExpense { get; set; }
    }
}
