using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class OPDServicesModel
    {
        public long OPDServicesId { get; set; }
        public long OPDConsoltationId { get; set; }
        public long? PatientId { get; set; }
        public long? ServiceId { get; set; }
        public string ServiceName { get; set; }
        public long PackageId { get; set; }
        public long Count { get; set; }
        public decimal ServiceCharge { get; set; }
        public string Remarks { get; set; }
        public bool? IsNewService { get; set; }
        public long? OPDServicesCreatedBy { get; set; }
        public DateTime? OPDServicesCreatedDate { get; set; }
        public long? OPDServicesModiifedBy { get; set; }
        public DateTime? OPDServicesModifiedDate { get; set; }
        public string Mode { get; set; }
        public string PaymentMode { get; set; }
        public long? TherapistId { get; set; }
        public string TherapistName { get; set; }
        public long? HospitalId { get; set; }
        public string TreatmentPaymentMode { get; set; }
        //public decimal ServiceCharge { get; set; }
        public long PatientPlanSubscriptionId { get; set; }
    }
}
