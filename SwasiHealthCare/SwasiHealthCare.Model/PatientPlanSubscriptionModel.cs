using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class PatientPlanSubscriptionModel
    {
        public long PatientPlanSubscriptionId { get; set; }
        public long PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientIDNumber { get; set; }
        public long PlanId { get; set; }
        public string PlanName { get; set; }
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
        public string Mode { get; set; }
    }
}
