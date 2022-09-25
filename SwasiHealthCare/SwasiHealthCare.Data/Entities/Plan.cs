using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwasiHealthCare.Data.Entities
{
    [Table("Plan", Schema = "dbo")]
    public class Plan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PlanId { get; set; }
        public string PackageCode { get; set; }
        public string TreatmentName { get; set; }
        public string NoofTreatment { get; set; }
        public string Offers { get; set; }
        public decimal? Amount { get; set; }
        public string isfree { get; set; }
        public DateTime PlanExpiryDate { get; set; }
        public long? PlanCreatedBy { get; set; }
        public DateTime PlanCreatedDate { get; set; }
        public long? PlanModifiedBy { get; set; }
        public DateTime? PlanModifiedDate { get; set; }
        //public string PlanName { get; set; }
        //public DateTime PlanExpiryDate { get; set; }
        //public decimal? PlanCharges { get; set; }
        //public long PlanSubusers { get; set; }
        public bool PlanStatus { get; set; }
        //public long? PlanCreatedBy { get; set; }
        //public DateTime PlanCreatedDate { get; set; }
        //public long? PlanModifiedBy { get; set; }
        //public DateTime? PlanModifiedDate { get; set; }
        //public string Mode { get; set; }
    }
}
