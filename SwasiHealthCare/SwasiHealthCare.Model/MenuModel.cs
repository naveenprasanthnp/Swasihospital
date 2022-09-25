using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class MenuModel
    {
        public long MenuId { get; set; }
        public string MenuName { get; set; }
        public long MenuCreatedBy { get; set; }
        public DateTime MenuCreatedDate { get; set; }
    }
}
