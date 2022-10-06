using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class DashboardModel
    {
        public long totalpatients { get; set; }
        public long totaldoctors { get; set; }
        public long totalstaffs { get; set; }
        public long totaltreatment { get; set; }
        public long? totalopd { get; set; }
        public long? totaldrug { get; set; }
        public decimal? totaltrmtrevenu { get; set; }
        public decimal? totaldrugrevenue { get; set; }
        public decimal? totalpurchasemedicine { get; set; }
        public decimal? totalexpense { get; set; }
        public decimal? totaltrmtrevenuday { get; set; }
        public decimal? totaldrugrevenueday { get; set; }
        public decimal? totalpurchasemedicineday { get; set; }
        public decimal? totalexpenseday { get; set; }
        //public FilterModel filterModel { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
