using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class HospitalModel
    {
        public long? HospitalId { get; set; }
        public string HospitalCode { get; set; }
        public string HospitalName { get; set; }
        public string HospitalAddress { get; set; }
        public string HospitalMobilNumber { get; set; }
        public string LandlineNumber { get; set; }
        public string HospitalContactPersonName { get; set; }
        public string HospitalContactPersonEmail { get; set; }
        public string HospitalEmail { get; set; }
        public bool HospitalStatus { get; set; }
        public long? HospitalCreatedBy { get; set; }
        public DateTime HospitalCreatedDate { get; set; }
        public long? HospitalModifiedBy { get; set; }
        public DateTime? HospitalModifiedDate { get; set; }
        public string Mode { get; set; }
        public string PatientIdStartWith { get; set; }
    }
}
