using SwasiHealthCare.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class LoginResponseModel
    {
        public bool Status { get; set; }
        public Users UserProfile { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
        public List<AccessRights> AccessRights { get; set; }
    }
}
