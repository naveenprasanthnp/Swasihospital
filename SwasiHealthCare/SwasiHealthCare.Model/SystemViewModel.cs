using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class SystemViewModel
    {
        public long? SystemId { get; set; }
        public string SystemName { get; set; }
        public string SystemIp { get; set; }
        public string SystemModel { get; set; }
        public bool SystemStatus { get; set; }
        public long? SystemCreatedBy { get; set; }
        public DateTime SystemCreatedDate { get; set; }
        public long? SystemModifiedBy { get; set; }
        public DateTime? SystemModifiedDate { get; set; }
        public string Mode { get; set; }
        public long? HospitalId { get; set; }
    }
}
