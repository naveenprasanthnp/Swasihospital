using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class SubMenuModel
    {
        public long DropdownMenuId { get; set; }
        public long MenuId { get; set; }
        public string MenuName { get; set; }
        public long MenuCreatedBy { get; set; }
        public DateTime MenuCreatedDate { get; set; }
    }
}
