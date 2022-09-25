using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class OPDReportModel
    {
        public FilterModel filterModel { get; set; }
        public long OPDConsultationId { get; set; }
        public long? PatientId { get; set; }
        public long? DoctorId { get; set; }
        public DateTime ConsultationDate { get; set; }
        public string DoctorName { get; set; }
        public string PatientIDNumber { get; set; }
        public string PatientName { get; set; }
        public string ServiceName { get; set; }
        public long? TherapistId { get; set; }
        public string TherapistName { get; set; }
        public decimal MedicineAmount { get; set; }
        public decimal ServiceCharge { get; set; }
        public string ModeofPayment { get; set; }
        public string ConsultationIDNumber { get; set; }
        public long? HospitalId { get; set; }
    }

    public class Dashboard
    {
        public long? DashboardTreatmentCount { get; set; }
        public long? DashboardMedicineCount { get; set; }
    }
}
