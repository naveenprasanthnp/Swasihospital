using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class TeleComPatientModel
    {
        public string PatientIDNumber { get; set; }
        public string PatientName { get; set; }
        public string MobileNo { get; set; }
        public DateTime ConsultationDate { get; set; }
        public string DoctorName { get; set; }
        public string PatientPrimaryComplaints { get; set; }
        public string Treatment { get; set; }
        public string Medicine { get; set; }
        public bool IsClientComingProperly { get; set; }
        public string ReasonforNotComing { get; set; }
        public string OtherReasons { get; set; }
        public DateTime TreatmentDate { get; set; }
        public CountryModel CountryModel { get; set; }
    }
}
