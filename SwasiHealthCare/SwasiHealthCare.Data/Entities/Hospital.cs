using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SwasiHealthCare.Data.Entities
{
    [Table("Hospital", Schema = "dbo")]
    public class Hospital
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long HospitalId { get; set; }
        public string HospitalCode { get; set; }
        public string HospitalName { get; set; }
        public string HospitalAddress { get; set; }
        public string HospitalMobilNumber { get; set; }
        public string LandlineNNumber { get; set; }
        public string HospitalContactPersonName { get; set; }
        public string HospitalContactPersonEmail{ get; set; }
        public string HospitalEmail { get; set; }
        public bool HospitalStatus { get; set; }
        public long? HospitalCreatedBy { get; set; }
        public DateTime? HospitalCreatedDate { get; set; }
        public long? HospitalModifiedBy { get; set; }
        public DateTime? HospitalModifiedDate { get; set; }
        public string PatientIdStartWith { get; set; }
    }
}
