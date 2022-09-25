using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class DesignationModel
    {
        public long? DesignationId { get; set; }
        public string DesignationName { get; set; }
        public bool DesignationStatus { get; set; }
        public long? DesignationCreatedBy { get; set; }
        public DateTime DesignationCreatedDate { get; set; }
        public long? DesignationModifiedBy { get; set; }
        public DateTime? DesignationModifiedDate { get; set; }
        public string Mode { get; set; }
        public long? HospitalId { get; set; }
    }
}
