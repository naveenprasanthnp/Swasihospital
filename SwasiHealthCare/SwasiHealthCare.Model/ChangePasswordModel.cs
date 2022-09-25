using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class ChangePasswordModel
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public string OldPassword { get; set; }
        public bool UserStatus { get; set; }
    }
}
