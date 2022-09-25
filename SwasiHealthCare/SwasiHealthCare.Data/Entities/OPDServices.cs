using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwasiHealthCare.Data.Entities
{
    [Table("OPDServices", Schema = "dbo")]
    public class OPDServices
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OPDServicesId { get; set; }
        public long ServiceId{ get; set; }
        public long OPDConsoltationId { get; set; }
        public long? PatientId { get; set; }
        public string ServiceName { get; set; }
        public long Count { get; set; }
        public decimal ServiceCharge { get; set; }
        public string Remarks { get; set; }
        public long? OPDServicesCreatedBy { get; set; }
        public DateTime? OPDServicesCreatedDate { get; set; }
        public long? OPDServicesModiifedBy { get; set; }
        public DateTime? OPDServicesModifiedDate { get; set; }
        public long? TherapistId { get; set; }
        public string TherapistName { get; set; }
        public long? HospitalId { get; set; }
    }
}
