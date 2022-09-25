using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class AccessRightsModel
    {
        public long AccessRightsId { get; set; }
        public long? DesignationId { get; set; }
        public long MenuId { get; set; }
        public long UserId { get; set; }
        public bool IsView { get; set; }
        public bool IsCreate { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
