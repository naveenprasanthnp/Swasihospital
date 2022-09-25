using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwasiHealthCare.Data.Entities
{
    [Table("PatientPlanSubscription", Schema = "dbo")]
    public class PatientPlanSubscription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PatientPlanSubscriptionId { get; set; }
        public long PatientId { get; set; }
        public long PlanId { get; set; }
        public long TotalServiceCount { get; set; }
        public decimal? TotalServiceAmount { get; set; }
        public bool PatientPlanSubscriptionStatus { get; set; }
        public long? BalanceServiceCount { get; set; }
        public decimal? BalanceServiceAmount { get; set; }
        public DateTime? PatientPlanSubscriptionDate { get; set; }
        public long? PatientPlanSubscriptionCreatedBy { get; set; }
        public DateTime? PatientPlanSubscriptionCreatedDate { get; set; }
        public long? PatientPlanSubscriptionModifiedBy { get; set; }
        public DateTime? PatientPlanSubscriptionModifiedDate { get; set; }
        public long? PatientPlanSubscriptionHospitalId { get; set; }
        public long OPDServicesId { get; set; }
    }
}
