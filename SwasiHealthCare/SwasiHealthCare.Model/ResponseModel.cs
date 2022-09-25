using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class ResponseModel
    {
        public bool Status { get; set; }
        public dynamic Data { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
    }
}
