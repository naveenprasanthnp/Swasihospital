using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SwasiHealthCare.Data.Entities
{
    [Table("Expense", Schema = "dbo")]
    public class Expense
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ExpenseId { get; set; }
        public string Description { get; set; }
        public string StoreName { get; set; }
        public decimal BillAmount { get; set; }
        public DateTime ExpenseCreatedDate { get; set; }
        public string ApprovedFrom { get; set; }
        public string BillCopyFileName { get; set; }
        public bool ExpenseStatus { get; set; }
        public long? ExpenseCreatedBy { get; set; }
        public DateTime ExpenseDate { get; set; }
        public long? ExpenseModifiedBy { get; set; }
        public DateTime? ExpenseModifiedDate { get; set; }
        public long? HospitalId { get; set; }
        public bool? IsCompanyExpense { get; set; }
    }
}
