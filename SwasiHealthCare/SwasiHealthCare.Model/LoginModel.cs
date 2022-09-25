using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class LoginModel
    {
        public long? UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PublicIP { get; set; }
        public string Otp { get; set; }
    }
}
