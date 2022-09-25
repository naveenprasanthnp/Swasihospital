using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class PlanModel
    {
        public long? PlanId { get; set; }
        public DateTime PlanExpiryDate { get; set; }
        public string PackageCode { get; set; }
        public string TreatmentName { get; set; }
        public string NoofTreatment { get; set; }
        public string Offers { get; set; }
        public decimal? Amount { get; set; }
        public string isfree { get; set; }
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
        public string Mode { get; set; }
    }
}
