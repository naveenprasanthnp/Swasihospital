using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class SideMenuModel
    {
        public string LoginUserName { get; set; }
        public long RoleId { get; set; }
        public bool IsFirstLogin { get; set; }
        public string ProfilePath { get; set; }
    }
}
