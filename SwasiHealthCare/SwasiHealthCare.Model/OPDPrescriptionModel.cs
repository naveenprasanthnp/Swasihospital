using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class OPDPrescriptionModel
    {
        public long OPDPrescriptionId { get; set; }
        public long OPDConsultationId { get; set; }
        public long? PatientId { get; set; }
        public long MedicineId { get; set; }
        public string MedicineName { get; set; }
        public long MedicineQuantity { get; set; }
        public decimal MedicineAmount { get; set; }
        //public string MedicineDosage { get; set; }
        //public string MedicineDuration { get; set; }
        public string MedicineInstructions { get; set; }
        //public string MedicineSpecification { get; set; }
        public long? OPDPrescriptionCreatedBy { get; set; }
        public DateTime? OPDPrescriptionCreatedDate { get; set; }
        public long? OPDPrescriptionModiifedBy { get; set; }
        public DateTime? OPDPrescriptionModifiedDate { get; set; }
        public string Mode { get; set; }
        public long? HospitalId { get; set; }
        public decimal? MedicineRate { get; set; }
    }
}
