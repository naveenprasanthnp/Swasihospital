using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class RolesModel
    {
        public long? RoleId { get; set; }
        public string RoleName { get; set; }
        public bool RoleStatus { get; set; }
        public long? RoleCreatedBy { get; set; }
        public DateTime? RoleCreatedDate { get; set; }
        public long? RoleModiifedBy { get; set; }
        public DateTime? RoleModifiedDate { get; set; }
        public string Mode { get; set; }
    }
}
