using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class TreatmentModel
    {
        public long? TreatmentId { get; set; }
        public string TreatmentName { get; set; }
        public string TreatmentDuration { get; set; }
        public string TreatmentDescription { get; set; }
        public string TreatmentMedicineNeeded { get; set; }
        public decimal? TreatmentCharges { get; set; }
        public bool TreatmentStatus { get; set; }
        public long? TreatmentCreatedBy { get; set; }
        public DateTime TreatmentCreatedDate { get; set; }
        public long? TreatmentModifiedBy { get; set; }
        public DateTime? TreatmentModifiedDate { get; set; }
        public string Mode { get; set; }
        public long? HospitalId { get; set; }
    }
}
